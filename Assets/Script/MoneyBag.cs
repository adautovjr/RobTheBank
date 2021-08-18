using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBag : MonoBehaviour
{
    const int PLAYER = 3;
    bool isCollectable = true;
    void Update()
    {
        if (isCollectable) {
            transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);
        }
    }

    public void toggleCollectable() {
        isCollectable = !isCollectable;
    }
}