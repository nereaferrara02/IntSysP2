using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHeart : MonoBehaviour
{
    public int RotationSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
        transform.Rotate(0, RotationSpeed*Time.deltaTime, 0);
    }
}
