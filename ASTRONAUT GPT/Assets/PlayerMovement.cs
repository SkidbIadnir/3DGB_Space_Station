using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;  // Adjust the speed as per your needs

    private Rigidbody playerRigidbody;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
{
    float moveHorizontal = Input.GetAxis("Horizontal");
    float moveVertical = Input.GetAxis("Vertical");

    // Rotate the player when Q or D is pressed
    if (Input.GetKey(KeyCode.Q))
    {
        transform.Rotate(Vector3.up, -speed * Time.deltaTime);
    }
    else if (Input.GetKey(KeyCode.D))
    {
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }

    Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
    playerRigidbody.velocity = movement * speed;
}

}
