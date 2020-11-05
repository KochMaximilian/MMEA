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
    public Image fadeScreen;
    public float fadeSpeed;
    private bool shouldFadeToBlack, shouldFadeFromBlack;
    public GameObject levelCompleteText;
    
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateGemCount();
        FadeFromBlack();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.g, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 1f)
            {
                shouldFadeToBlack = false;
            }
        }

        if (shouldFadeFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.g, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 0f)
            {
                shouldFadeFromBlack = false;
            } 
        }
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

    public void FadeToBlack()
    {
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }

    public void FadeFromBlack()
    {
        shouldFadeFromBlack = true;
        shouldFadeToBlack = false;
    }
}
