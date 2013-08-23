using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public int val = 3;
	public bool isGoodGuy = true;
	public GameObject explosion;
	
	int startingHealth = 0;
	
	
	void Awake ()
	{
		startingHealth = val;
	}
	
	void OnCollisionEnter (Collision other)
	{		
		if (other.gameObject.tag == "projectile") {
			GotHit(other.gameObject.GetComponent<Projectile>());
		}
	}
	
	void GotHit (Projectile projectile)
	{		
		if (projectile == null) {
			return;
		}
		
		// Only count hits from the opposing team
		if (isGoodGuy == projectile.isGoodGuy) {
			return;
		}
		
		val--;
		
		if (val <= 0) {
			if (!isGoodGuy) {
				var points = 100 * startingHealth;
				GameManager.IncreaseScore(points);
			}
			
			if (explosion != null) {
				GameObject ex = Instantiate(explosion, gameObject.transform.position, Quaternion.identity) as GameObject;
				ex.BroadcastMessage("Explode");		
			}
		
			Destroy(gameObject);
		}
	}
}
