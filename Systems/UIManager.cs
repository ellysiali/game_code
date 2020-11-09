using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Image yellowHealthBar;
    [SerializeField] private Image potion;
    [SerializeField] private TextMeshProUGUI coinText;

    /**************************************************************************
     Function: 	  Start
     Description: Initializes the necessary variables before the first update
     *************************************************************************/    
    void Start()
    {
        healthBar.fillAmount = GameStatus.GetInstance().currentHealth / GameStatus.GetInstance().MaxHealth;
        yellowHealthBar.fillAmount = GameStatus.GetInstance().currentHealth / GameStatus.GetInstance().MaxHealth;
        try
        {
            potion.sprite = GameStatus.GetInstance().potionSprite;
        }
        catch (NullReferenceException)
        { }
    }

    /**************************************************************************
    Function: 	  Update
    Description: Once per frame, implements the following code
    *************************************************************************/
    void Update()
    {
        coinText.text = "" + GameStatus.GetInstance().coinCount;
        UpdateHealthBar();
        potion.gameObject.SetActive(GameStatus.GetInstance().BuffActive);
    }

    /**************************************************************************
    Function: 	  UpdateHealthBar
    Description:  Updates the health bar UI
    *************************************************************************/
    void UpdateHealthBar()
    {
        if (healthBar.fillAmount < GameStatus.GetInstance().currentHealth / GameStatus.GetInstance().MaxHealth)
        {
            if (healthBar.fillAmount + 0.01f > GameStatus.GetInstance().currentHealth / GameStatus.GetInstance().MaxHealth)
            {
                healthBar.fillAmount = GameStatus.GetInstance().currentHealth / GameStatus.GetInstance().MaxHealth;
            }
            else
            {
                healthBar.fillAmount += 0.01f;
            }
        }
        else
        {
            healthBar.fillAmount = (float)GameStatus.GetInstance().currentHealth / GameStatus.GetInstance().MaxHealth;
        }
        if (yellowHealthBar.fillAmount > healthBar.fillAmount)
        {
            yellowHealthBar.fillAmount -= 0.005f;
        }
    }

    public void UpdateConsumableSprite(Sprite sprite)
    {
            potion.sprite = sprite;
    }
}
