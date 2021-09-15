using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public bool[] isFull;
	public GameObject[] slots;
	public GameObject slot;

	void Update()
	{
		if (Input.GetButtonDown("Use_inventory_slot_1"))
		{
			SearchNUse(slots[0].GetComponent<Slot>().transform, 0);
		}
		if (Input.GetButtonDown("Use_inventory_slot_2"))
		{
			SearchNUse(slots[1].GetComponent<Slot>().transform, 1);
		}
	}

	private void SearchNUse(Transform slot, int index)
	{
		foreach (Transform child in slot)
		{
			if (child.CompareTag("Powerup"))
			{
				child.gameObject.GetComponent<Pickup>().UseItem(index);
			}
		}
	}

	public string GetItemOnIndexName(Transform slot)
	{
		foreach (Transform child in slot)
		{
			if (child.CompareTag("Powerup"))
			{
				return child.gameObject.GetComponent<Pickup>().name;
			}
		}
		return null;
	}

	public Pickup GetItemOnInventory(Transform slot)
	{
		foreach (Transform child in slot)
		{
			if (child.CompareTag("Powerup"))
			{
				return child.gameObject.GetComponent<Pickup>();
			}
		}
		return null;
	}
}