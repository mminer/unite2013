using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	[HideInInspector] public SecureScore score = new SecureScore();
	[HideInInspector] public int insecureScore;
	
	public float speed = 1f;
	public GameObject projectile;
	public Transform projectileSpawnPoint;
	public bool useSecureScore = true;
	public Health health;
	
	Vector3 translation;
	float timeBetweenShots = 0.4f;
	float lastShotTime;
	
	void Awake ()
	{
		health = GetComponent<Health>();
	}
	

	void Update () 
	{
		GetInput();
		MovePlayer();
	}
	
	void GetInput ()
	{
		translation = Vector3.zero;
		
		// Move left
		if (Input.GetKey("a") || Input.GetKey("left")) {
			translation.x += speed;
		}
		
		// Move right
		if (Input.GetKey("d") || Input.GetKey("right")) {
			translation.x -= speed;
		}
		
		// Fire Weapon
		if (Input.GetKey("space")) {
			if (Time.time - lastShotTime > timeBetweenShots) {
				StartCoroutine(FireProjectile());	
			}
		}
	}
	
	void MovePlayer ()
	{
		transform.Translate(translation);
	}
	
	IEnumerator FireProjectile ()
	{
		lastShotTime = Time.time;
		var spawnPoint = projectileSpawnPoint.position;
		var rotation = projectile.transform.rotation;
		GameObject shot = Instantiate(projectile, spawnPoint, rotation) as GameObject;
		
		shot.collider.isTrigger = true;
		yield return new WaitForSeconds(0.25f);
		
		// Shot might be destroyed
		if (shot != null) {
			shot.collider.isTrigger = false;	
		}
	}
}
