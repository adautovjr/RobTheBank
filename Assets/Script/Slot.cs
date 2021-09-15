using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
	private Inventory inventory;

	public int i = 0;
	public int quantity = 0;
	public Text slotQuantity;
	public bool isFull = false;

	private void Start()
	{
		inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
		UpdateUIQuantity();
	}

	private void Update()
	{
		if (transform.childCount <= 0)
		{
			inventory.isFull[i] = false;
		}
	}
	public void UseItem()
	{
		Debug.Log(inventory.slots[i].GetComponent<Slot>());
		//if (inventory.isFull[i] && inventory.isPowerUpSpeed) inventory.slots[i].ToggleBuffSpeed();
		foreach (Transform child in transform)
		{
			GameObject.Destroy(child.gameObject);
		}
	}

	public void DropItem()
	{
		foreach (Transform child in transform)
		{
			if (!child.CompareTag("inventory_quantity"))
			{
				GameObject.Destroy(child.gameObject);
			}
		}
	}

	public void UpdateUIQuantity()
	{
		slotQuantity.text = quantity == 0 ? "" : "" + quantity.ToString();
	}
}