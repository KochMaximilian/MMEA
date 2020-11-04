using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Stompbox : MonoBehaviour
{
    public GameObject deathEffect;
    public GameObject collectible;
    [Range(0, 100f)]public float chaneToDrop;
   
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
        if (other.CompareTag("Enemy"))
        {
            //Debug.Log("HIT ENEMY");
            other.transform.parent.gameObject.SetActive(false);
            Instantiate(deathEffect, other.transform.position, other.transform.rotation);
            PlayerController.instance.Bounce();

            // Max: Let Item drop random at after enemy death
            var dropSelect = Random.Range(0, 100f);

            if (dropSelect <= chaneToDrop)
            {
                Instantiate(collectible, other.transform.position, other.transform.rotation);
            }
            AudioManager.instance.PlaySFX(3);
        }
    }
}
