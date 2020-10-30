using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    public int currentHealth, maxHealth;

    [SerializeField] private float _invincibleLength;
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
            gameObject.SetActive(false); // Max: Player disappears 
         }
         else
         {
            invincibleCounter = _invincibleLength;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, .5f);
            
            PlayerController.instance.KnockBack();
         }
         UIController.instance.UpdateHealthDisplay();
        } 
    }
}
