using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	public bool isPowerUp = false;
	public PowerUpType speedType;

	public enum PowerUpType
	{
		SPEED5 = 5,
		SPEED10 = 10,
		SPEED15 = 15
	}
}