using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The target object to follow 

    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position; // Set the camera's position to the target's position 
        transform.rotation = target.rotation; // Set the camera's rotation to the target's rotation
    }
}
