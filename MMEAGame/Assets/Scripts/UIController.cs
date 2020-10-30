using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine.UI;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [SerializeField] private Image _health1, _health2, _health3; // Max: UI uses Image Components instead of sprites
    [SerializeField] private Sprite _healthFull, _healthEmpty, _healthHalf;
    public TextMeshProUGUI gemText;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateGemCount();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthDisplay()
    {
        switch (PlayerHealthController.instance.currentHealth)
        {
            case 6:
                _health1.sprite = _healthFull;
                _health2.sprite = _healthFull;
                _health3.sprite = _healthFull;
                break;
            
            case 5:
                _health1.sprite = _healthFull;
                _health2.sprite = _healthFull;
                _health3.sprite = _healthHalf;
                break;
            
            case 4:
                _health1.sprite = _healthFull;
                _health2.sprite = _healthFull;
                _health3.sprite = _healthEmpty;
                break;
            
            case 3:
                _health1.sprite = _healthFull;
                _health2.sprite = _healthHalf;
                _health3.sprite = _healthEmpty;
                break;
            
            case 2:
                _health1.sprite = _healthFull;
                _health2.sprite = _healthEmpty;
                _health3.sprite = _healthEmpty;
                break;
            
            case 1:
                _health1.sprite = _healthHalf;
                _health2.sprite = _healthEmpty;
                _health3.sprite = _healthEmpty;
                break;
            
            case 0:
                _health1.sprite = _healthEmpty;
                _health2.sprite = _healthEmpty;
                _health3.sprite = _healthEmpty;
                break;
            
            default:
                _health1.sprite = _healthEmpty;
                _health2.sprite = _healthEmpty;
                _health3.sprite = _healthEmpty;
                break;
        }
    }

    public void UpdateGemCount()
    {
        gemText.text = LevelManager.instance.gemsCollected.ToString();
    }
}
