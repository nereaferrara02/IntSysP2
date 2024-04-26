using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Duration of the shake effect
    public float shakeDuration = 10f;
    
    // Magnitude of the shake
    public float shakeMagnitude = 0.2f;

    private float timeElapsed = 0f; // Time elapsed since the script started
    public bool isShaking = false; // Flag to check if the camera is currently shaking
    private Vector3 originalPosition; // Original position of the camera before shaking

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        // Increment the time elapsed
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= 20f && !isShaking)
        {
            // Start the shake coroutine
            StartCoroutine(Shake());
        }
    }

    // Coroutine to handle the camera shake effect
    IEnumerator Shake()
    {
        isShaking = true;

        float elapsedTime = 0f; 

        // Continue shaking until the specified duration is reached
        while (elapsedTime < shakeDuration)
        {
            // Generate a random position within a sphere and add it to the original position
            Vector3 randomPosition = originalPosition + Random.insideUnitSphere * shakeMagnitude;

            // Apply the random position to the camera's local position
            transform.localPosition = randomPosition;

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Reset the camera's position to the original position
        transform.localPosition = originalPosition;

        timeElapsed = 0f;
        isShaking = false;
    }
}
