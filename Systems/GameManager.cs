using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private float respawnTime;
    [SerializeField] public Image healthBar;
    [SerializeField] public Image yellowHealthBar;
    [SerializeField] public Player playerScript;
    [SerializeField] private Text coinText;

    private CinemachineVirtualCamera CVC;
    private float respawnTimeStart;
    public float coinCount { get; private set; }
    private bool toRespawn;

    /**************************************************************************
     Function: 	  Start
     Description: Initializes the necessary variables before the first update
     *************************************************************************/    
    void Start()
    {
        playerScript = FindObjectOfType<Player>();
        coinCount = 0;
        //CVC = GameObject.Find("Player Camera").GetComponent<CinemachineVirtualCamera>();
    }

    /**************************************************************************
    Function: 	  Update
    Description: Once per frame, implements the following code
    *************************************************************************/
    void Update()
    {
        coinText.text = "" + coinCount;
        UpdateHealthBar();
        CheckRespawn();
    }

    /**************************************************************************
    Function: 	  UpdateHealthBar
    Description:  Updates the health bar UI
    *************************************************************************/
    void UpdateHealthBar()
    {
        if (healthBar.fillAmount < playerScript.currentHealth / playerData.maxHealth)
        {
            if (healthBar.fillAmount + 0.01f > (float)playerScript.currentHealth / playerData.maxHealth)
            {
                healthBar.fillAmount = (float)playerScript.currentHealth / playerData.maxHealth;
            }
            else
            {
                healthBar.fillAmount += 0.01f;
            }
        }
        else
        {
            healthBar.fillAmount = (float)playerScript.currentHealth / playerData.maxHealth;
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
            playerScript.ResetStats();
            player.transform.position = respawnPoint.position;
            player.SetActive(true);
            //var playerTemp = Instantiate(player, respawnPoint);
            //CVC.m_Follow = playerTemp.transform;
            //playerScript = playerTemp.GetComponent<Player>();
            toRespawn = false;
            yellowHealthBar.fillAmount = 1f;
        }
    }

    public void AddCoins(float numberOfCoins)
    {
        coinCount += numberOfCoins;
    }

    public void RemoveCoins(float numberOfCoins)
    {
        coinCount -= numberOfCoins;
    }
}
