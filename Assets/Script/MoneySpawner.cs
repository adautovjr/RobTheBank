using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySpawner : MonoBehaviour
{
    public GameObject money;

    void Start()
    {
        SpawnMoney();
    }
    public void SpawnMoney()
    {
        Instantiate(money, transform.position, transform.rotation);
    }
}
