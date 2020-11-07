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

        PlayerPrefs.SetFloat("maxHealth", maxHealth);
        PlayerPrefs.SetFloat("currentHealth", currentHealth);
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
        int index = 0;
        bool finished = false;
        maxHealth = PlayerPrefs.GetFloat("maxHealth", maxHealth);
        currentHealth = PlayerPrefs.GetFloat("currentHealth", currentHealth);
        coinCount = PlayerPrefs.GetFloat("coinCount", coinCount);
        attackMultiplier = PlayerPrefs.GetFloat("attackMultiplier", attackMultiplier);
        defenseMultiplier = PlayerPrefs.GetFloat("defenseMultiplier", defenseMultiplier);

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
    }
}
