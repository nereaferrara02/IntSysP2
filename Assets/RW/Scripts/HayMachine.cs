using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HayMachine : MonoBehaviour
{
    public float movementSpeed;
    public float horizontalBoundary = 22;

    public GameObject hayBalePrefab;
    public Transform haySpawnpoint;
    public float shootInterval;
    private float shootTimer;

    private bool Multishooting;

    //To change the color of the machines
    public Transform modelParent;
    public GameObject blueModelPrefab;
    public GameObject yellowModelPrefab;
    public GameObject redModelPrefab;


    public int MaxSheepSaved;
    private void Start()
    {
        LoadModel();

    }

    private void LoadModel()
    {
        Destroy(modelParent.GetChild(0).gameObject); 

        switch (GameSettings.hayMachineColor)
        {
            case HayMachineColor.Blue:
                Instantiate(blueModelPrefab, modelParent);
                break;

            case HayMachineColor.Yellow:
                Instantiate(yellowModelPrefab, modelParent);
                break;

            case HayMachineColor.Red:
                Instantiate(redModelPrefab, modelParent);
                break;
        }
    }


    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
        UpdateShooting();

    }

    private void UpdateMovement()
    {

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        // print(horizontalInput); //para ver si funcionan el teclado <- ->

        //Moving to the left
        if(horizontalInput < 0 && transform.position.x > - horizontalBoundary){
            transform.Translate(transform.right * -movementSpeed * Time.deltaTime);
        }

        //Moving to the right
        else if (horizontalInput > 0 && transform.position.x < horizontalBoundary)
        {
            transform.Translate(transform.right * movementSpeed * Time.deltaTime);
        }

 
    }

    void ShootHay()
    {
      
        Instantiate(hayBalePrefab, haySpawnpoint.position, Quaternion.identity);
        SoundManager.Instance.PlayShootClip();

    }


    private void UpdateShooting()
    {
        shootTimer -= Time.deltaTime;
        if(shootTimer <= 0 && Input.GetKey(KeyCode.Space))
        {

            //every time we save 3 sheeps we increment the shootInterval
            if(GameStateManager.Instance.sheepSaved % MaxSheepSaved == 0)
            {
                shootInterval = shootInterval * 0.9f;
            }


            shootTimer = shootInterval;
            ShootHay();

        }

    }


}
