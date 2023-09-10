using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player; // Reference to the player's Transform component.
    public float smoothSpeed = 5.0f; // The speed at which the camera follows the player.

    private Vector3 offset; // The initial offset between the player and the camera.

    void Start()
    {   
        //Get the player from Game Manager
        player = GameManager.Instance.MainPlayer.transform;
        // Calculate the initial offset between the player and the camera.
        offset = transform.position - player.position;

    }

    void FixedUpdate()
    {
        // Calculate the desired camera position based on the player's Y and Z positions.
        Vector3 targetPosition = player.position + offset;

        // Use Mathf.SmoothDamp to smoothly interpolate between the current camera position and the target position.
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.fixedDeltaTime);
    }
}
