using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] public Transform target; // Reference to the target transform
    [SerializeField] public float speed = 5f; // Speed at which the camera moves towards the target

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            // Move the transform towards the target position
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
}
