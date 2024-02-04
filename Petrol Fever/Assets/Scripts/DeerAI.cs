using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerAI : MonoBehaviour
{
    private float randomX;
    private float randomZ;
    private int i = 1000;
    private int nextMove;
    private Quaternion rotation;
    
    IEnumerator RotateAndMove(Vector3 targetPosition)
    {
        // Calculate rotation to the target
        Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);

        // Smoothly rotate to face the target
        float rotationDuration = 0.25f;
        float rotationElapsedTime = 0f;
        Quaternion startRotation = transform.rotation;

        while (rotationElapsedTime < rotationDuration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, rotationElapsedTime / rotationDuration);
            rotationElapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure reaching the exact target rotation
        transform.rotation = targetRotation;
        transform.Rotate(0, -270, 0);


        // move to the target position
        float movementDuration = 1f;
        float movementElapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (movementElapsedTime < movementDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, movementElapsedTime / movementDuration);
            movementElapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure reaching the exact target position
        transform.position = targetPosition;
    }

    public void Move(float x, float z)
    {
        Vector3 targetPosition = new Vector3(x, 15.5f, z);
        StartCoroutine(RotateAndMove(targetPosition));
    }

    // Start is called before the first frame update
    void Start()
    {
        randomX = Random.Range(-54f, -43f);
        randomZ = Random.Range(-51f, -42f);
        nextMove = Random.Range(2500, 5000);
        this.transform.position = new Vector3(randomX,15.5f,randomZ);
    }

    // Update is called once per frame
    void Update()
    {
        if(i >= nextMove) {
            randomX = Random.Range(-54f, -45f);
            randomZ = Random.Range(-51f, -42f);
            nextMove = Random.Range(2500, 5000);
            Move(randomX, randomZ);
            i = 0;
        }
        i++;
    }
}