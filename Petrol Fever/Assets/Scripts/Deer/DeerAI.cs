using System.Collections;
using UnityEngine;

public class DeerAI : MonoBehaviour
{
    public float minSpeed = 1f;
    public float maxSpeed = 3f;
    public float rotationSpeed = 2f;
    public Vector3 minPosition = new Vector3(-10, 0, -10);
    public Vector3 maxPosition = new Vector3(10, 0, 10);
    private Vector3 targetPosition;
    private float speed;
    
    private void Start()
    {
        StartCoroutine(MoveRandomly());
    }
    
    private Vector3 PickRandomPosition()
    {
        float x = Random.Range(minPosition.x, maxPosition.x);
        float z = Random.Range(minPosition.z, maxPosition.z);
        return new Vector3(x, transform.position.y, z);
    }

    private void RotateTowardsTarget(Vector3 originPosition)
    {
        Vector3 direction = targetPosition - originPosition;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void MoveTowardsTarget()
    {
    Vector3 direction = (targetPosition - transform.position).normalized;
    transform.position += direction * (speed * Time.deltaTime);
    }   

    private IEnumerator MoveRandomly()
    {
        while (true)
        {
            targetPosition = PickRandomPosition();
            Vector3 originPosition = transform.position;
            speed = Random.Range(minSpeed, maxSpeed);
            
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f) {
                RotateTowardsTarget(originPosition);
                MoveTowardsTarget();
                yield return null;
            }

            yield return new WaitForSeconds(Random.Range(1f, 6f));
        }
    }
}