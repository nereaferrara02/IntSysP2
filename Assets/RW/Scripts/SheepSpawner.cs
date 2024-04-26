using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SheepSpawner : MonoBehaviour
{

    public bool canSpawn = true; 
    public GameObject sheepPrefab; 
    public List<Transform> sheepSpawnPositions = new List<Transform>();
    public float timeBetweenSpawns;
    private List<GameObject> sheepList = new List<GameObject>();

    public float spawnAcceleration;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private void SpawnSheep()
    {
        Vector3 randomPosition = sheepSpawnPositions[Random.Range(0,sheepSpawnPositions.Count)].position;
        GameObject sheep = Instantiate(sheepPrefab, randomPosition,
        sheepPrefab.transform.rotation);
        sheepList.Add(sheep);
        sheep.GetComponent<Sheep>().SetSpawner(this); 
    }


    private IEnumerator SpawnRoutine()
    {
        while (canSpawn)
        {
            SpawnSheep();
            yield return new WaitForSeconds(timeBetweenSpawns);

            //timeBetweenSpawns is decreased by spawnAcceleration
            //after each spawn to make the spawns happen faster over time.
            timeBetweenSpawns -= spawnAcceleration;
            if(timeBetweenSpawns < 0.1f)
            {
                timeBetweenSpawns = 0.1f;
            }
        }
    }

    public void RemoveSheepFromList(GameObject sheep)
    {
        sheepList.Remove(sheep);
    }

    public void DestroyAllSheep()
    {
        foreach (GameObject sheep in sheepList)
        {
            Destroy(sheep);
        }

        sheepList.Clear();
    }



}








