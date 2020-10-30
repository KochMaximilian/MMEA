using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           // Debug.Log("Hit!");
           /*  Max: this is ok for a 2D game in bigger games this would cause performance issues
           FindObjectOfType<PlayerHealthController>().DealDamage(); // Max: find an object with the PlayerHealthController and call the DealDamage function inside the PlayerHealthController
           */
           
           // Max: Solution = Singleton, can be used in other scripts too
           PlayerHealthController.instance.DealDamage();
           
        }
    }
}
