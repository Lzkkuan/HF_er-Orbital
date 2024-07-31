using UnityEngine;

public class FollowNPC : MonoBehaviour
{
    public GameObject canvas; // Reference to the canvas
    public Vector3 offset; // Offset for the canvas position

    void Update()
    {
        if (canvas != null)
        {
            // Calculate the desired position
            Vector3 desiredPosition = transform.position + offset;
            Debug.Log("canvas: " + desiredPosition);
            // Validate the position
            if (IsValidPosition(desiredPosition))
            {
                canvas.transform.position = desiredPosition;
            }
            else
            {
                Debug.LogWarning("Invalid position calculated for canvas: " + desiredPosition);
            }
        }
    }

    // Method to check if the position is valid (not containing Infinity or NaN)
    private bool IsValidPosition(Vector3 position)
    {
        return !float.IsInfinity(position.x) && !float.IsNaN(position.x) &&
               !float.IsInfinity(position.y) && !float.IsNaN(position.y) &&
               !float.IsInfinity(position.z) && !float.IsNaN(position.z);
    }
}
