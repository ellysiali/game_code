using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public Dictionary<string, int> NPCIndexes = new Dictionary<string, int>();
    #endregion
    #region Other Variables
    static GameStatus instance;
    #endregion
    public static GameStatus GetInstance() => instance;
    public GameStatus()
    {
        playerInventory = new Inventory();
    }
    public void Start()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
    }
    public void Update()
    {
    }
    public void Save()
    {
        int index = 0;
        Player player = GameObject.FindObjectOfType<Player>();

        currentHealth = maxHealth;
        PlayerPrefs.SetFloat("maxHealth", maxHealth);
        PlayerPrefs.SetFloat("coinCount", coinCount);
        PlayerPrefs.SetFloat("attackMultiplier", attackMultiplier);
        PlayerPrefs.SetFloat("defenseMultiplier", defenseMultiplier);

        PlayerPrefs.SetFloat("spawnPositionX", player.gameObject.transform.position.x);
        PlayerPrefs.SetFloat("spawnPositionY", player.gameObject.transform.position.y);
        PlayerPrefs.SetInt("scene", SceneManager.GetActiveScene().buildIndex);

        foreach (InventorySlot item in playerInventory.itemList)
        {
            PlayerPrefs.SetInt("inventory_" + index + "_id", item.itemID);
            PlayerPrefs.SetInt("inventory_" + index + "_count", item.amount);
            index++;
        }

        foreach (KeyValuePair<string, int> entry in NPCIndexes)
        {
            PlayerPrefs.SetInt(entry.Key, entry.Value);
        }

        PlayerPrefs.Save();
    }
    public void Load()
    {
        LevelLoader levelLoader = GameObject.FindObjectOfType<LevelLoader>();
        int index = 0;
        bool finished;
        maxHealth = PlayerPrefs.GetFloat("maxHealth", 100);
        currentHealth = maxHealth;
        coinCount = PlayerPrefs.GetFloat("coinCount", 0);
        attackMultiplier = PlayerPrefs.GetFloat("attackMultiplier", 1f);
        defenseMultiplier = PlayerPrefs.GetFloat("defenseMultiplier", 1f);

        spawnPosition = new Vector2(PlayerPrefs.GetFloat("spawnPositionX", 0f), PlayerPrefs.GetFloat("spawnPositionY", -0.74f));
        flipOnStart = false;

        playerInventory.itemList.Clear();
        finished = !PlayerPrefs.HasKey("inventory_" + index + "_id");
        while (!finished)
        {
            playerInventory.AddItem(PlayerPrefs.GetInt("inventory_" + index + "_id"), PlayerPrefs.GetInt("inventory_" + index + "_count"));
            index++;
            finished = !PlayerPrefs.HasKey("inventory_" + index + "_id");
        }
        NPCIndexes.Clear();
        levelLoader.LoadLevel(PlayerPrefs.GetInt("scene", 1));
    }
    public void AddHealth(float value)
    {
        if (currentHealth + value <= maxHealth)
        {
            currentHealth += value;
        }
        else
        {
            currentHealth = maxHealth;
        }
    }
}
