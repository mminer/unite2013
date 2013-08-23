using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {
	
	public float timeUntilDestruction = 5f;
	
	void Start () 
	{
		StartCoroutine(DoDestroy());		
	}
	
	IEnumerator DoDestroy ()
	{
		yield return new WaitForSeconds(timeUntilDestruction);
		Destroy(gameObject);
	}
}
