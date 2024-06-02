using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The object to follow
    public Vector3 offset = new Vector3(0, 2, -3); // Offset from the target's position

    private void LateUpdate()
    {
        if (target != null)
        {
            // Set the position of the camera to be offset from the target's position
            transform.position = target.position + offset;

            // Set the rotation of the camera to match the target's rotation
            transform.rotation = target.rotation;

            transform.Rotate(Vector3.up, 90f);
        }
    }
}
