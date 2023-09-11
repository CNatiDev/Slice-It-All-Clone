using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFollow : MonoBehaviour
{
    private Transform Player;
    public float SmoothSpeed = 5.0f; // The speed at which the camera follows the player.
    private void Start()
    {
        //Get the player from Game Manager
        Player = GameManager.Instance.MainPlayer.transform;
    }
    void Update()
    { 
        FollowPlayer();
    }
    public void FollowPlayer()
    {
        // Calculate the desired camera position based on the player's Y and Z positions.
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, Player.position.z);

        // Use Mathf.SmoothDamp to smoothly interpolate between the current camera position and the target position.
        transform.position = Vector3.Lerp(transform.position, targetPosition, SmoothSpeed * Time.fixedDeltaTime);
    }
}
