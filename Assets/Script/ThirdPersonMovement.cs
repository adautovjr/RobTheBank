using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThirdPersonMovement : MonoBehaviour
{

    public CharacterController controller;
    public Transform cam;
    public Animator animator;
    public float speed = 10f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public MoneySpawner moneySpawner;
    public Text uiMoneyCounter;
    private bool hasMoneyBag = false;
    private int score;

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

        if (direction.magnitude >= 0.1f)
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
            ToggleMoneyBag();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Dropoff") && hasMoneyBag)
        {
            score++;
            UpdateScore();
            ToggleMoneyBag();
            moneySpawner.SpawnMoney();
        }
    }

    private void ToggleMoneyBag()
    {
        if (!hasMoneyBag)
        {
            hasMoneyBag = true;
        }
        else
        {
            hasMoneyBag = false;
        }
    }

    private void UpdateScore()
    {
        uiMoneyCounter.text = "Total subtraído não tão legalmente: $ " + score.ToString();
    }

    public void ToggleRagdoll(){
    }

    public void ToggleCollision(){

    }
}