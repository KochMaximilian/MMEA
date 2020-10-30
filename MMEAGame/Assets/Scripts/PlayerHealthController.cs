using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    [SerializeField] private int _currentHealth, _maxHealth;


    private void Awake()
    {
        instance = this; // Max: is the exact component (current version of PlayerHealthController)
    }

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DealDamage()
    {
        _currentHealth--;

        if (_currentHealth <= 0)
        {
            gameObject.SetActive(false); // Max: Player disappears 
        }
    }
}
