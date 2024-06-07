using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 80f;
    public float rotationSpeed = 100f; // Adjust this value for desired rotation speed

    private void Update()
    {
        // Movement input
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Rotation input
        float turn = 0f;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            turn = -1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            turn = 1f;
        }

        // Movement calculation
        Vector3 forwardMovement = transform.forward * moveVertical * speed;
        Vector3 sidewaysMovement = transform.right * moveHorizontal * speed;
        Vector3 movement = forwardMovement + sidewaysMovement;

        // Apply movement
        transform.position += movement * Time.deltaTime;

        // Apply rotation
        transform.Rotate(Vector3.up, turn * rotationSpeed * Time.deltaTime);
    }
}
 