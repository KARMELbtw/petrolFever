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

        // Move to the target position
        float movementDuration = 1f;
        float movementElapsedTime = 0f;
        Vector3 startPosition = transform.localPosition;

        while (movementElapsedTime < movementDuration)
        {
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, movementElapsedTime / movementDuration);
            movementElapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure reaching the exact target position
        transform.localPosition = targetPosition;
    }

    public void Move(float x, float z)
    {
        Vector3 targetPosition = new Vector3(x, 15.24f, z);
        StartCoroutine(RotateAndMove(targetPosition));
    }

    // Start is called before the first frame update
    void Start()
    {
        randomX = Random.Range(-3f, -3f);
        randomZ = Random.Range(-2.795f, 3.3f);
        nextMove = Random.Range(2500, 5000);
        this.transform.localPosition = new Vector3(randomX,-0.5f,randomZ);
    }

    // Update is called once per frame
    void Update()
    {
        if(i >= nextMove) {
            randomX = Random.Range(-3f, -3f);
            randomZ = Random.Range(-2.795f, 3.3f);
            nextMove = Random.Range(2500, 5000);
            Move(randomX, randomZ);
            i = 0;
        }
        i++;
    }
}