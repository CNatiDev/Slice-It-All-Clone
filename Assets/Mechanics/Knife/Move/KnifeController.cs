using UnityEngine;

public class KnifeController : MonoBehaviour
{
    // Knife jump parameters
    public float jumpForce = 5f;
    public float rotationSpeed = 360f; // Degrees per second
    private Rigidbody rb;
    public float maxRotationX = 50f; // Maximum rotation on the X-axis
    public bool IsJumping = false;
    // Update is called once per frame
    void Update()
    {
        // Check for user input (e.g., tapping the screen)
        if (Input.GetButtonDown("Fire1"))
        {
            Jump();

        }
        if (IsJumping) {
            RotateKnife();
        }
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Jump()
    {
        // Add an upward force to the knife
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        IsJumping = true;
    }
    void RotateKnife()
    {
        // Get the current rotation of the object.
        Vector3 currentRotation = transform.rotation.eulerAngles;

        // Rotate the object on the X-axis.
        currentRotation.x += rotationSpeed * Time.deltaTime;

        // Apply the new rotation to the object.
        transform.rotation = Quaternion.Euler(currentRotation.x,transform.eulerAngles.y,transform.eulerAngles.z);
    }
    private void OnCollisionEnter(Collision collision)
    {
        IsJumping = false;
    }
}
