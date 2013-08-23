using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public GameObject[] asteroids;
	public GameObject[] enemies;
	public int maxEnemies = 8;
	public float minEnemySpawnTime = 1;
	public float minAsteroidSpawnTime = 8f;
	public float difficultyIncreaseInterval = 5f;
	
	public static Player player;
	
	static float randomSpawnInterval;
	static GameObject[] spawnPoints;
	static GameObject[] asteroidSpawnPoints;
	
	static GameManager instance;
	GameManager () {}
	
	void Awake ()
	{
		instance = this;
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		spawnPoints = GameObject.FindGameObjectsWithTag("Spawn Point");
		asteroidSpawnPoints = GameObject.FindGameObjectsWithTag("Asteroid Spawn Point");
	}
	
	void Start ()
	{
		DoIncreaseDifficulty();
		DoSpawnEnemy();
		DoSpawnAsteroid();
	}
	
	void DoIncreaseDifficulty ()
	{
		StartCoroutine(IncreaseDifficulty());
	}
	
	void DoSpawnEnemy ()
	{
		StartCoroutine(SpawnEnemy());
	}
	
	void DoSpawnAsteroid ()
	{
		StartCoroutine(SpawnAsteroid());
	}
	
	public static void IncreaseScore (int amount)
	{
		player.score += amount;
		player.insecureScore += amount;
	}
	
	#region Difficulty Ramping
	
	static IEnumerator IncreaseDifficulty ()
	{
		yield return new WaitForSeconds(instance.difficultyIncreaseInterval);
		instance.maxEnemies++;
		instance.minEnemySpawnTime /= 1.1f;
		instance.minAsteroidSpawnTime /= 1.1f;
		instance.DoIncreaseDifficulty();
	}
	
	#endregion
	
	#region Enemy Management
	
	static IEnumerator SpawnEnemy ()
	{
		// Wait between spawns
		randomSpawnInterval = Random.Range(0.5f, 3);
		yield return new WaitForSeconds(instance.minEnemySpawnTime + randomSpawnInterval);
		
		// Spawn new enemy
		var spawnPoint = GetSpawnPoint();
		var enemy = GetRandomEnemy();
		Instantiate(enemy, spawnPoint, enemy.transform.rotation);
		instance.DoSpawnEnemy();
	}
	
	static Vector3 GetSpawnPoint ()
	{
		int indexes = spawnPoints.Length - 1;
		int randomIndex = Random.Range(0, indexes);
		return spawnPoints[randomIndex].transform.position;
	}
	
	static GameObject GetRandomEnemy ()
	{
		int randomIndex = Random.Range(0, instance.enemies.Length);
		return instance.enemies[randomIndex];
	}
	
	#endregion
	
	#region Asteroid Management
	
	static IEnumerator SpawnAsteroid ()
	{
		// Wait between spawns
		var waitTime = Random.Range(instance.minAsteroidSpawnTime, instance.minAsteroidSpawnTime * 2);
		yield return new WaitForSeconds(waitTime);
		
		// Spawn new asteroid
		var spawnPoint = GetAsteroidSpawnPoint();
		var asteroid = GetRandomAsteroid();
		Instantiate(asteroid, spawnPoint, asteroid.transform.rotation);
		instance.DoSpawnAsteroid();
	}
	
	static Vector3 GetAsteroidSpawnPoint ()
	{
		int indexes = asteroidSpawnPoints.Length - 1;
		int randomIndex = Random.Range(0, indexes);
		return asteroidSpawnPoints[randomIndex].transform.position;
	}
	
	static GameObject GetRandomAsteroid ()
	{
		int randomIndex = Random.Range(0, instance.asteroids.Length);
		return instance.asteroids[randomIndex];
	}
	
	#endregion
}
