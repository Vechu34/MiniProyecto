using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float spriteWidth, startPosX, startPosY;
    public GameObject cam;
    public Vector2 parallaxEffect; // X and Y independent movement

    void Start()
    {
        startPosX = transform.position.x;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void LateUpdate()
    {
        float distanceX = (cam.transform.position.x * parallaxEffect.x);
        float distanceY = (cam.transform.position.y * parallaxEffect.y);

        transform.position = new Vector3(startPosX + distanceX, startPosY + distanceY, transform.position.z);

        // Smooth Infinite Transition on X
        float camRelativeX = cam.transform.position.x * (1 - parallaxEffect.x);
        if (camRelativeX > startPosX + spriteWidth)
        {
            startPosX += spriteWidth;
        }
        else if (camRelativeX < startPosX - spriteWidth)
        {
            startPosX -= spriteWidth;
        }
    }
}

