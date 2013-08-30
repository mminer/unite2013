using UnityEngine;
using System.Collections;

public class KillVolume : MonoBehaviour {
	
	void OnTriggerEnter (Collider other)
	{
		Destroy(other.gameObject);
	}
}
