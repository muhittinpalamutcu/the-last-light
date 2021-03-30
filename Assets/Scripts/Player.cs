using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // We make it public because later on we want to change from inside the unity
    public float speed;

    public float health;

    //It contains all the physic that unity contain 
    private Rigidbody2D rb;

    private Vector2 moveAmount;

    private Animator anim;

    public Weapon currentWeapon;

    public Transform newWeaponInstantiatePlace;

    public GameObject pickupAudio;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    Animator cameraAnim;

    private SceneTransitions sceneTransitions;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cameraAnim = Camera.main.GetComponent<Animator>();
        sceneTransitions = FindObjectOfType<SceneTransitions>();
    }

    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // normalized mean just make game sure to player don't move faster than we expect
        moveAmount = moveInput.normalized * speed;

        //We can get rid of normalized property it makes some weird things when we play the game so be careful!

        // moveAmount = moveInput * speed;

        // I am checking if the player is moving 
        if (moveInput != Vector2.zero)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }


    }


    // this function will get call every single physic action happen
    private void FixedUpdate()
    {

        // fixedDeltaTime for indepented frame on character movement so same speed on old or new pc
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
    }

    public void TakeDamage(int damageAmount)
    {

        health -= damageAmount;
        UpdateHealthUI(Convert.ToInt32(health));
        cameraAnim.SetTrigger("shake");
        if (health <= 0)
        {
            Destroy(gameObject);
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Weapon");
            for (int i = 0; i < objectsWithTag.Length; i++)
            {
                Destroy(objectsWithTag[i]);

            }
            sceneTransitions.LoadScene("Lose");
        }
    }


    public void ChangeWeapon(Weapon weaponToEquip)
    {
        Instantiate(pickupAudio, transform.position, transform.rotation);
        currentWeapon.tag = "Weapon";
        currentWeapon = null;
        currentWeapon = Instantiate(weaponToEquip, newWeaponInstantiatePlace.position, newWeaponInstantiatePlace.rotation, transform);
        currentWeapon.tag = "CurrentWeapon";
        currentWeapon.transform.localScale = new Vector3(1.5F, 1.5F, 1.5F);


        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Weapon");

        for (int i = 0; i < objectsWithTag.Length; i++)
        {
            Destroy(objectsWithTag[i]);


        }
    }

    public void UpdateHealthUI(int currentHealth)  //4 
    {                        //5
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }


    public void Heal(int healAmount)
    {
        if (health + healAmount > 5)
        {
            health = 5;
        }
        else
        {
            health += healAmount;
        }
        UpdateHealthUI(Convert.ToInt32(health));
        Instantiate(pickupAudio, transform.position, transform.rotation);
    }
}
