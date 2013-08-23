using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	public Vector3 rotationSpeed;
	
	void Update () {
		transform.Rotate(rotationSpeed);
	}
}
