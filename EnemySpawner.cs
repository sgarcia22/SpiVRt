using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public Transform[] spawnLocations;
    public GameObject[] enemies;
    public static int currentEnemies;
    [SerializeField] private int maxEnemies = 20;
    public Transform spawnLoc;
    // Use this for initialization
    void Start () {
        currentEnemies = 0;
        InvokeRepeating("SpawnWisp", 1f, 1f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void SpawnWisp()
    {
        if (currentEnemies > maxEnemies)
            return;

        int randomEnemy = Random.Range(0, enemies.Length);
        int randomSpawn = Random.Range(0, spawnLocations.Length);
        //Spawn inside of a random circle
        Vector3 spawnPoint = enemies[randomEnemy].transform.position;
        if (Random.Range(0, 1) == 0)
            spawnPoint.x += Random.Range(0f, 4f);
        else
            spawnPoint.x -= Random.Range(0f, 4f);
        if (Random.Range(0, 1) == 0)
            spawnPoint.y += Random.Range(0f, 4f);
        else
            spawnPoint.y -= Random.Range(0f, 4f);
        //Spawn the wisp
        GameObject spawnedEnemy = Instantiate(enemies[randomEnemy], spawnLocations[randomSpawn].transform.position, Quaternion.identity, gameObject.transform);
        spawnedEnemy.transform.SetParent(gameObject.transform, false);
        spawnedEnemy.transform.localScale = new Vector3(3f,3f,3f);
        spawnedEnemy.SetActive(true);
        currentEnemies++;
    }
}
