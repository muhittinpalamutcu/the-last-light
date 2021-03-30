using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;

    [HideInInspector]
    public Transform player;

    public float speed;

    public float timeBetweenAttacks;

    public int damage;

    public int pickupChance;
    public GameObject[] pickups;

    public int healthPickupChance;
    public GameObject healthPickUp;

    public GameObject deathEffect;
    public GameObject deathAudio;


    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    public void TakeDamage(int damageAmount)
    {

        health -= damageAmount;

        if (health <= 0)
        {
            int randomNumber = Random.Range(0, 101);
            if (randomNumber < pickupChance)
            {
                GameObject randomPickUp = pickups[Random.Range(0, pickups.Length)];
                Instantiate(randomPickUp, transform.position, transform.rotation);
            }
            int randHealth = Random.Range(0, 101);
            if (randHealth < healthPickupChance)
            {
                Instantiate(healthPickUp, transform.position, transform.rotation);
            }
            Instantiate(deathAudio, transform.position, transform.rotation);
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }




}
