using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform Player; // Reference to the player's Transform component.
    public float SmoothSpeed = 5.0f; // The speed at which the camera follows the player.

    private Vector3 Offset; // The initial offset between the player and the camera.

    void Start()
    {   
        //Get the player from Game Manager
        Player = GameManager.Instance.MainPlayer.transform;
        // Calculate the initial offset between the player and the camera.
        Offset = transform.position - Player.position;

    }

    void Update()
    {
        FollowPlayer();
    }
    public void FollowPlayer()
    {
        // Calculate the desired camera position based on the player's Y and Z positions.
        Vector3 targetPosition = Player.position + Offset;

        // Use Mathf.SmoothDamp to smoothly interpolate between the current camera position and the target position.
        transform.position = Vector3.Lerp(transform.position, targetPosition, SmoothSpeed * Time.fixedDeltaTime);
    }
}
