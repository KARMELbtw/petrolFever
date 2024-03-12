using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerOilAi : MonoBehaviour
{   
    public static int chanceToFindOil = 50;
    public static int minTimeToFindOil = 10;
    public static int maxTimeToFindOil = 20;

    private Vector3 minPosition;
    private Vector3 maxPosition;
    private int timeToFindOil;
    private float time;
    private bool foundOil;
    private Vector3 targetPosition = new Vector3(0, 25f, 0);
    private float minSpeed = 1f;
    private float maxSpeed = 3f;
    private float rotationSpeed = 2f;
    private float speed;
    private bool first = true;

    private Vector3 PickRandomPosition()
    {
        float x = Random.Range(minPosition.x, maxPosition.x);
        float z = Random.Range(minPosition.z, maxPosition.z);
        return new Vector3(x, 25f, z);
    }

    private IEnumerator MoveRandomly()
    {
        while (true)
        {
            targetPosition = PickRandomPosition();   
            Vector3 originPosition = transform.position;
            speed = Random.Range(minSpeed, maxSpeed);
            
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f) {
                if (first) {
                    first = false;
                    break;
                }
                RotateTowardsTarget(originPosition);
                MoveTowardsTarget();
                yield return null;
            }

            yield return new WaitForSeconds(Random.Range(1f, 2f));
        
        }
    }

    private void RotateTowardsTarget(Vector3 originPosition)
    {
        Vector3 direction = targetPosition - originPosition;
        Quaternion targetRotation = Quaternion.LookRotation(-direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void MoveTowardsTarget()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * (speed * Time.deltaTime);
    }   

    // Start is called before the first frame update
    void Start()
    {
        GameObject parentObject = transform.parent.gameObject;
        Vector3 chunkPosition = parentObject.transform.position + new Vector3(0, 25f, 0);

        StartCoroutine(MoveRandomly());        

        minPosition = chunkPosition;
        maxPosition = chunkPosition + new Vector3(15f, 0, 15f);

        float x = Random.Range(minPosition.x, maxPosition.x);
        float z = Random.Range(minPosition.z, maxPosition.z);

        foundOil = false;
        if(Random.Range(1, 100) <= chanceToFindOil) {
            foundOil = true;
        }

        timeToFindOil = Random.Range(minTimeToFindOil, maxTimeToFindOil);
    }

    private void Update() {
        time += Time.deltaTime;
        var seconds = (int)(time % 60);
        if(timeToFindOil <= seconds) {
            if (foundOil) {
                ChunkGeneration.RevealRandomOilVein(ChunkGeneration.oilVeins);
                Debug.Log("Wykryto ropę");
                foundOil = false;
            }
            Destroy(gameObject);
        }
    }
}
