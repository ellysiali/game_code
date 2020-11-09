using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private GameObject player;
    [SerializeField] private float respawnTime;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image yellowHealthBar;
    [SerializeField] private Image potion;
    [SerializeField] private TextMeshProUGUI coinText;

    private float respawnTimeStart;
    private bool toRespawn;

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
        CheckRespawn();
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

    /**************************************************************************
    Function: 	  Respawn
    Description:  Initializes the parameters needed to respawn the player
    *************************************************************************/
    public void Respawn()
    {
        respawnTimeStart = Time.time;
        toRespawn = true;
    }

    /**************************************************************************
    Function: 	  CheckRespawn
    Description:  Checks and initiates respawn
    *************************************************************************/

    private void CheckRespawn()
    {
        if (Time.time >= respawnTimeStart + respawnTime && toRespawn)
        {
            GameStatus.GetInstance().Load();
            toRespawn = false;
        }
    }

    public void UpdatePotion(Sprite sprite)
    {
            potion.sprite = sprite;
    }
}
