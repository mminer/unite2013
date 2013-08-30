using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	
	public bool isGoodGuy = true;
	
	void OnCollisionEnter (Collision other)
	{
		Destroy(gameObject);
	}
}
