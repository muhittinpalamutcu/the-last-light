using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;

    public float speed;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;


    private void Start()
    {
        transform.position = playerTransform.position;

    }

    private void Update()
    {
        // Lerp is a function to provide us move with smoothly base on a speed
        if (playerTransform != null)
        {

            //if our minX=-5 maxX=10 and player pos x =15 our clamp wouldn't be able to exceed 15 or go below -5 so make player pos x =10 
            float clampedX = Mathf.Clamp(playerTransform.position.x, minX, maxX);

            float clampedY = Mathf.Clamp(playerTransform.position.y, minY, maxY);

            transform.position = Vector2.Lerp(transform.position, new Vector2(clampedX, clampedY), speed);
        }

    }



}
