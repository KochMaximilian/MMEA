﻿using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    public int currentHealth, maxHealth;

    [SerializeField] private float _invincibleLength;
    [SerializeField] private GameObject deathEffect;
    private float invincibleCounter;
    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        instance = this; // Max: is the exact component (current version of PlayerHealthController)
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime; // Max: amount of time it takes to get from one frame to the next

            if (invincibleCounter <= 0)
            {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
            }
        }
    }

    public void DealDamage()
    {
        if (invincibleCounter <= 0)
        {
            currentHealth--;

         if (currentHealth <= 0)
         {
            currentHealth = 0;
            //  gameObject.SetActive(false); // Max: Player disappears 
            Instantiate(deathEffect, transform.position, transform.rotation);
            LevelManager.instance.RespawnPlayer();
         }
         else
         {
            invincibleCounter = _invincibleLength;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, .7f);
            
            PlayerController.instance.KnockBack();
            AudioManager.instance.PlaySFX(9);
         }
         UIController.instance.UpdateHealthDisplay();
        } 
    }

    public void HealPlayer()
    {
        // Max: full heal
        // currentHealth = maxHealth; 
        currentHealth++;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UIController.instance.UpdateHealthDisplay();
    }
}
