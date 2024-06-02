using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 80f;

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 forwardMovement = transform.forward * moveVertical * speed;
        Vector3 sidewaysMovement = transform.right * moveHorizontal * speed;

        Vector3 movement = forwardMovement + sidewaysMovement;

        transform.position += movement * Time.deltaTime;
    }

}
