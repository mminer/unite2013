using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	public Vector3 speed;
	float timer = 0.01f;
	
	void Awake ()
	{
		StartCoroutine(Translate());
	}
	
	IEnumerator Translate()
	{
		yield return new WaitForSeconds(timer);
		transform.Translate(speed);
		StartCoroutine(Translate());
	}
}
