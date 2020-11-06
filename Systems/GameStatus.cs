using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    #region Player Stats/Info
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    public float coinCount = 0f;
    public float attackMultiplier = 1f;
    public float defenseMultiplier = 1f;

    public Vector2 spawnPosition = new Vector2(0f, 0f);
    public bool flipOnStart = false;

    public Inventory playerInventory;
    #endregion
    static GameStatus instance;

    public static GameStatus GetInstance() => instance;

    public GameStatus()
    {
        playerInventory = new Inventory();
    }

    void Start()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDestroy()
    {
        // TODO: Save data to savefile upon exiting -- potentially with PlayerPrefs
        // (e.g. PlayerPrefs.SetInt("currentHealth", health);
    }
}
