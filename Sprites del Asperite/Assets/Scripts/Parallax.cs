using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Vector2 startPos, length; // Store initial X and Y position
    public GameObject cam;
    public Vector2 parallaxEffect; // Use Vector2 for independent X and Y effects

    void Start()
    {
        startPos = transform.position; // Store initial position
        length = GetComponent<SpriteRenderer>().bounds.size; // Get the size of the sprite (background)
    }

    void FixedUpdate()
    {
        // Calculate the distance moved by the camera
        float distanceX = (cam.transform.position.x - startPos.x) * parallaxEffect.x;
        float distanceY = (cam.transform.position.y - startPos.y) * parallaxEffect.y;

        // Update the new position based on the camera movement
        transform.position = new Vector3(startPos.x + distanceX, startPos.y + distanceY, transform.position.z);

        // Handle infinite loop for X-axis with smoother transition
        if (cam.transform.position.x - startPos.x > length.x)
        {
            startPos.x += length.x;
        }
        else if (cam.transform.position.x - startPos.x < -length.x)
        {
            startPos.x -= length.x;
        }

        // Handle infinite loop for Y-axis with smoother transition
        if (cam.transform.position.y - startPos.y > length.y)
        {
            startPos.y += length.y;
        }
        else if (cam.transform.position.y - startPos.y < -length.y)
        {
            startPos.y -= length.y;
        }

        // Smooth interpolation to create a more fluid transition between tiles
        transform.position = Vector3.Lerp(transform.position, new Vector3(startPos.x + distanceX, startPos.y + distanceY, transform.position.z), Time.deltaTime * 5);
    }
}

