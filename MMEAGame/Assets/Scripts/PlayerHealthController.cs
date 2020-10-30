using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{

    [SerializeField] private int _currentHealth, _maxHealth;
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
