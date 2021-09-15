using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
	private Inventory inventory;
	private ThirdPersonMovement player;
	public GameObject thumbnail;
	public bool isPowerUpSpeed = false;
	public bool isPowerUpLoadedSpeed = false;
	public float powerUpSpeed = 0f;
	public float powerUpLoadedSpeed = 0f;
	public float powerUpDuration = 0f;
	public string name;
	public enum PowerUpValues
	{
		SPEED1 = 5,
		SPEED2 = 10,
		SPEED3 = 15
	}

	void Start()
	{
		inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonMovement>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			for (int i = 0; i < inventory.slots.Length; i++)
			{
				//print(inventory.GetItemOnIndexName(inventory.slots[0].GetComponent<Slot>().transform));
				if (inventory.slots[i].GetComponent<Slot>().isFull == true && this.name == inventory.GetItemOnIndexName(inventory.slots[i].GetComponent<Slot>().transform))
				{
					Destroy(gameObject);
					inventory.slots[i].GetComponent<Slot>().quantity++;
					inventory.slots[i].GetComponent<Slot>().UpdateUIQuantity();
					//inventory.GetItemOnInventory(inventory.slots[i].GetComponent<Slot>().transform).quantity++;
					break;
				}
				if (inventory.slots[i].GetComponent<Slot>().isFull == false)
				{
					//inventory.isFull[i] = true;
					inventory.slots[i].GetComponent<Slot>().isFull = true;
					Instantiate(this, inventory.slots[i].transform, false);
					Instantiate(thumbnail, inventory.slots[i].transform, false);
					Destroy(gameObject);
					inventory.slots[i].GetComponent<Slot>().quantity++;
					inventory.slots[i].GetComponent<Slot>().UpdateUIQuantity();
					//print(quantity);
					break;
				}
			}
		}
	}

	public void UseItem(int index)
	{
		Slot slot = inventory.slots[index].GetComponent<Slot>();
		if (isPowerUpSpeed)
		{
			ToggleBuffSpeed();
			StartCoroutine(WaitBuffSpeed(powerUpDuration));
			UpdateIndexes(slot);
		}
		if (isPowerUpLoadedSpeed)
		{
			ToggleBuffLoadedSpeed();
			StartCoroutine(WaitBuffLoadedSpeed(powerUpDuration));
			UpdateIndexes(slot);
		}
	}

	private void UpdateIndexes(Slot slot)
	{
		if (slot.quantity > 1)
		{
			slot.quantity--;
			slot.UpdateUIQuantity();
		}
		else if (slot.quantity == 1)
		{
			slot.quantity--;
			slot.UpdateUIQuantity();
			slot.DropItem();
			slot.isFull = false;
		}
	}

	IEnumerator WaitBuffSpeed(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		ToggleBuffSpeed();
	}

	IEnumerator WaitBuffLoadedSpeed(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		ToggleBuffLoadedSpeed();
	}

	private void ToggleBuffLoadedSpeed()
	{
		player.loadedBuff = !player.loadedBuff;
		if (player.loadedBuff)
		{
			player.speed = player.getSPEED();
		}
	}

	private void ToggleBuffSpeed()
	{
		player.speedBuff = !player.speedBuff;
		if (player.speedBuff)
		{
			player.speed = player.speed * powerUpSpeed;
		}
		else
		{
			player.speed = player.getSPEED();
		}
	}

	///<summary>Retorna se um powerup é válido, mas não faz sentido, até pq eu que vou setar.
	///<param><paramref name="boolean"/> recebe um boleano kekw.</param>
	///<param><paramref name="value"/> um valor kekw.</param>
	///<returns><c>true</c> ou <c>false</c></returns>
	///</summary>
	private bool IsValidPowerUp(bool boolean, float value) => boolean && value > 0;
}