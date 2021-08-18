using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VehicleSpawner : MonoBehaviour
{
    public GameObject[] cars;
    public GameObject[] lanes;
    private ArrayList busyLanes;

    void Start()
    {
        busyLanes = new ArrayList();
        StartCoroutine(WaitCutscene(1.5f));
    }

    void Update()
    {
    }

    void SpawnCar()
    {
        UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
        int car = UnityEngine.Random.Range(0, cars.Length - 1);
        int lane = UnityEngine.Random.Range(0, lanes.Length);
        while (busyLanes.Contains(lane))
        {
            lane = UnityEngine.Random.Range(0, lanes.Length - 1);
        }
        busyLanes.Add(lane);
        if (lane < lanes.Length) { 
            Instantiate(cars[0], new Vector3(lanes[lane].transform.position.x, lanes[lane].transform.position.y, lanes[lane].transform.position.z), lanes[lane].transform.rotation);
        }
        Invoke("SpawnCar", UnityEngine.Random.Range(0.1f, 1f));
        StartCoroutine(LaneDebuser(lane));
    }

    IEnumerator LaneDebuser(int lane)
    {
        yield return new WaitForSeconds(1);
        busyLanes.Remove(lane);
    }

    IEnumerator WaitCutscene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SpawnCar();
    }
}
