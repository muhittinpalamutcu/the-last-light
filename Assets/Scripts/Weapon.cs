using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public GameObject projectile;
    public Transform shotPoint;
    public float timeBetweenShots;
    private float shotTime;

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;

        if (Input.GetMouseButton(0))
        {
            if (Time.time >= shotTime)
            {
                Instantiate(projectile, shotPoint.position, transform.rotation);

                // this is how much time the player will need to wait between each shot
                shotTime = Time.time + timeBetweenShots;
            }
        }


    }
}
