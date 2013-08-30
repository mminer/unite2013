using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public GameObject projectile;
	public Transform projectileSpawnPoint;
	float minTimeBetweenShots = 0.66f;

	
	void Awake ()
	{
		StartCoroutine(FireProjectile());
	}
	
	IEnumerator FireProjectile ()
	{
		var waitTime = Random.Range(minTimeBetweenShots, 3f);
		yield return new WaitForSeconds(waitTime);
		
		var spawnPoint = projectileSpawnPoint.position;
		var rotation = projectile.transform.rotation;
		GameObject shot = Instantiate(projectile, spawnPoint, rotation) as GameObject;
		StartCoroutine(FireProjectile());

		shot.collider.isTrigger = true;
		yield return new WaitForSeconds(0.25f);
		
		// Shot might be destroyed
		if (shot != null) {
			shot.collider.isTrigger = false;	
		}
	}
}
