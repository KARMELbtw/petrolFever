using System.Collections;
using UnityEngine;

public class DeerAI : MonoBehaviour
{
    public float minSpeed = 1f;
    public float maxSpeed = 3f;
    public float rotationSpeed = 2f;
    public Vector3 minPosition = new Vector3(-10, 0, -10);
    public Vector3 maxPosition = new Vector3(10, 0, 10);
    public int chanceToFindOil = 50;

    private int minTimeToFindOil = 10;
    private int maxTimeToFindOil = 20;
    private int timeToFindOil;
    private float time;
    private Vector3 targetPosition;
    private float speed;
    private float previousSec;

    private void Start()
    {
        StartCoroutine(MoveRandomly());
        if(SceneChanger.IsLookingAtChunk) {
            bool foundOil = false;

            if(Random.Range(1, 100) <= chanceToFindOil) {
                foundOil = true;
            }

            timeToFindOil = Random.Range(minTimeToFindOil, maxTimeToFindOil);
        }
    }

    private void Update() {
        time += Time.deltaTime;
        var seconds = (int)(time % 60);

        if(seconds != previousSec)
        Debug.Log(seconds);

        previousSec = seconds;

        if(timeToFindOil <= seconds) {

        }
    }

    private Vector3 PickRandomPosition()
    {
        float x = Random.Range(minPosition.x, maxPosition.x);
        float z = Random.Range(minPosition.z, maxPosition.z);
        return new Vector3(x, transform.position.y, z);
    }

    private void RotateTowardsTarget()
    {
        Vector3 direction = targetPosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        targetRotation = Quaternion.Inverse(targetRotation);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private IEnumerator MoveRandomly()
    {
        while (true)
        {
            targetPosition = PickRandomPosition();
            speed = Random.Range(minSpeed, maxSpeed);

            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                RotateTowardsTarget();
                MoveTowardsTarget();
                yield return null;
            }

            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }
}