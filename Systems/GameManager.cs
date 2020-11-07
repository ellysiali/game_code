using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private GameObject player;
    [SerializeField] private float respawnTime;
    [SerializeField] public Image healthBar;
    [SerializeField] public Image yellowHealthBar;
    [SerializeField] public Player playerScript;
    [SerializeField] private TextMeshProUGUI coinText;

    private float respawnTimeStart;
    private bool toRespawn;

    /**************************************************************************
     Function: 	  Start
     Description: Initializes the necessary variables before the first update
     *************************************************************************/    
    void Start()
    {
        playerScript = FindObjectOfType<Player>();
        healthBar.fillAmount = GameStatus.GetInstance().currentHealth / GameStatus.GetInstance().maxHealth;
        yellowHealthBar.fillAmount = GameStatus.GetInstance().currentHealth / GameStatus.GetInstance().maxHealth;
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
    }

    /**************************************************************************
    Function: 	  UpdateHealthBar
    Description:  Updates the health bar UI
    *************************************************************************/
    void UpdateHealthBar()
    {
        if (healthBar.fillAmount < GameStatus.GetInstance().currentHealth / GameStatus.GetInstance().maxHealth)
        {
            if (healthBar.fillAmount + 0.01f > GameStatus.GetInstance().currentHealth / GameStatus.GetInstance().maxHealth)
            {
                healthBar.fillAmount = GameStatus.GetInstance().currentHealth / GameStatus.GetInstance().maxHealth;
            }
            else
            {
                healthBar.fillAmount += 0.01f;
            }
        }
        else
        {
            healthBar.fillAmount = (float)GameStatus.GetInstance().currentHealth / GameStatus.GetInstance().maxHealth;
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
            playerScript.ResetHealth();
            player.transform.position = respawnPoint.position;
            player.SetActive(true);
            toRespawn = false;
        }
    }
}
