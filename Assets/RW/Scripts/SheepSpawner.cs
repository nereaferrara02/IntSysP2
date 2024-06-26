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
    public float timeBetweenSpawnsWhileShaking;


    private List<GameObject> sheepList = new List<GameObject>();


    public CameraShake cameraShake;




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

            if(cameraShake.isShaking){
                //while the camera shaking the sheeps are spawn faster
                yield return new WaitForSeconds(timeBetweenSpawnsWhileShaking); 
            }else{
                yield return new WaitForSeconds(timeBetweenSpawns); 
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








