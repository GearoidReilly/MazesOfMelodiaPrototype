﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    private int health;
    [SerializeField] protected int maxHealth = 100;         //Max health of enemy
    [SerializeField] protected int damage;                  //Enemy Damage
    [SerializeField] protected float speed;                 //Enemy Movement Speed
    [SerializeField] protected float timeBetweenAttacks;    //Time between enemy attacks
    [SerializeField] private AudioClip damageSound; //Damage sound on player
    [SerializeField] private AudioClip deathSound;  //Death Sound on player

    protected PlayerStats playerTarget;                       //Player to target
    protected Animator anim;                                //Reference to animator
    private Collider col;

    // Start is called before the first frame update
    public virtual void Start()
    {
        //Set health to maxHealth
        health = maxHealth;
        //Find the player in scene
        playerTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        //Get reference to the animator component
        anim = GetComponent<Animator>();
        //Get reference to collider
        col = GetComponent<Collider>();
        //Increase the number of enemies in the scene by one
        GameManager.Instance.AdjustEnemyCountInScene(1);
    }

    public void TakeDamage(int damageAmount){
        //Player takes damage based on damage amount
        health -= damageAmount;
        //Play damage sound
        AudioManager.Instance.PlaySFX(damageSound);

        //Check if the player has lost all of their health
        if(health <= 0){
            //Player is dead
            Die();
        }
    }

    //Enemy has lost all of their health
    private void Die(){
        //Play the death animation
        anim.SetTrigger("Dying");
        //Decrease the number of enemies in the scene by one
        GameManager.Instance.AdjustEnemyCountInScene(-1);
        //Play death sound
        AudioManager.Instance.PlaySFX(deathSound);
        //Disable collider
        col.enabled = false;
    }

    public void RemoveEnemy(){
        //Destroy the gameobject
        Destroy(gameObject);
    }
}
