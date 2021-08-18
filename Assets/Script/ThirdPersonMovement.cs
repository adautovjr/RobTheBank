using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Cinemachine;
using UnityEngine.SceneManagement;

public class ThirdPersonMovement : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera customCamera;
    public CharacterController controller;
    public GameObject body;
    public Transform cam;
    public Animator animator;
    public float speed = 10f;
    public float MIN_SPEED = 6f;
    public float MAX_SPEED = 10f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public MoneySpawner moneySpawner;
    public Text uiMoneyCounter;
    public GameObject backpack;
    private bool hasMoneyBag = false;
    private bool dead = false;
    private int score;
    public List<Collider> RagdollParts = new List<Collider>();

    const int MIN_DINERO = 5000;
    const int MAX_DINERO = 10000;

    private void Awake()
    {
        customCamera = this.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        SetRagdollParts();
    }

    void Start()
    {
        score = 0;
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f && !dead)
        {
            animator.SetBool("Moving", true);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward).normalized;
            controller.Move(moveDir * speed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MoneyBag") && !hasMoneyBag)
        {
            other.gameObject.GetComponent<MoneyBag>().toggleCollectable();
            ToggleMoneyBag();
            other.gameObject.transform.parent = backpack.transform;
            other.gameObject.transform.position = backpack.transform.position;
            other.gameObject.transform.rotation = backpack.transform.rotation;
        }
        else if (other.gameObject.CompareTag("Dropoff") && hasMoneyBag)
        {
            UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
            score += UnityEngine.Random.Range(MIN_DINERO, MAX_DINERO); ;
            UpdateScore();
            ToggleMoneyBag();
            DestroyMoneyBags();
            moneySpawner.SpawnMoney();
        }
    }

    private void ToggleMoneyBag()
    {
        hasMoneyBag = !hasMoneyBag;
        if(hasMoneyBag) {
            speed = MIN_SPEED;
        } else {
            speed = MAX_SPEED;
        }
    }

    private void UpdateScore()
    {
        uiMoneyCounter.text = "$" + score.ToString();
    }

    private void SetRagdollParts() {
        Collider[] colliders = gameObject.GetComponentsInChildren<Collider>();

        foreach (Collider c in colliders) { 
            if (c.gameObject != gameObject) {
                c.isTrigger = true;
                RagdollParts.Add(c);
            }
        }
    }

    public void ToggleRagdoll(){
        dead = true;
        animator.enabled = false;
        animator.avatar = null;

        foreach (Collider c in RagdollParts.ToArray()) {
            c.isTrigger = false;
            c.attachedRigidbody.velocity = Vector3.zero;
        }
        controller.enabled = false;

        StartCoroutine(WaitToResetWorld(6));
    }

    public void ToggleCollision(){

    }

    private void DestroyMoneyBags() {
        int childs = backpack.transform.childCount;
        Destroy(backpack.transform.GetChild(0).gameObject);
    }

    IEnumerator WaitToResetWorld(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}