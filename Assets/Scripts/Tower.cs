using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour 
{
	[SerializeField]
	private int attackDamage;


	public int AttackDamage
	{
		get { return this.attackDamage; }
	}
}
