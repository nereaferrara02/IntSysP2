using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{

    public float runSpeed;
    public float gotHayDestroyDelay;
    private bool hitByHay;
    public float dropDestroyDelay;
    private Collider myCollider;
    private Rigidbody myRigidBody;
    private SheepSpawner sheepSpawner;

    private bool isdropped = false;


    //Heart Part
    public float heartOffset;
    public GameObject heartPrefab;



    // Rotation Part
    private bool isRotating = false;
    private float rotationSpeed;


    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider>();
        myRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
       
        if (isRotating)
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
    }

    
    private void HitByHay()
    {
        sheepSpawner.RemoveSheepFromList(gameObject); 
        hitByHay = true;
        runSpeed = 0;

        rotationSpeed = 360 / gotHayDestroyDelay; // Calculate rotation speed
        isRotating = true; // Start rotating

        Destroy(gameObject, gotHayDestroyDelay);
        Instantiate(heartPrefab, transform.position + new Vector3(0, heartOffset, 0), Quaternion.identity);
        TweenScale tweenScale = gameObject.AddComponent<TweenScale>(); ;
        tweenScale.targetScale = 0; 
        tweenScale.timeToReachTarget = gotHayDestroyDelay;

        GameStateManager.Instance.SavedSheep();
        SoundManager.Instance.PlaySheepHitClip();

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Hay") && !hitByHay)
        {
            Destroy(other.gameObject);
            HitByHay();
        }else if (other.CompareTag("DropSheep")&& !isdropped)
        {
            Drop();
        }
    }

    private void Drop()
    {
        isdropped = true;
        sheepSpawner.RemoveSheepFromList(gameObject);

        GameStateManager.Instance.DroppedSheep();


        //Make the sheep's rigidbody non-kinematic affected by gravity
        myRigidBody.isKinematic = false;

        //Disable the trigger so sheep become a solid object
        myCollider.isTrigger = false;

        //Destroy the sheep after the delay specified in dropDestroyDelay
        Destroy(gameObject, dropDestroyDelay);
        SoundManager.Instance.PlaySheepDroppedClip();

    }

    public void SetSpawner(SheepSpawner spawner)
    {
        sheepSpawner = spawner;
    }

}


