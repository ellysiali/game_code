using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStatus : MonoBehaviour
{
    #region Player Stats/Info
    public float MaxHealth = 100f;
    public float MaxMagic = 100f;
    public float currentHealth = 100f;
    public float currentMagic = 100f;
    public float coinCount = 0f;

    public float attackMultiplier = 1f;
    public float defenseMultiplier = 1f;
    public float healthOverTime = 0f;
    public float magicOverTime = 0f;

    public bool flipOnStart = false;
    public Vector2 spawnPosition = new Vector2(0f, 0f);

    public Inventory playerInventory;
    public Dictionary<string, int> NPCIndexes = new Dictionary<string, int>();
    #endregion
    #region Other Variables
    static GameStatus instance;

    public bool BuffActive { get; private set; }
    private float lastBuffTime = 0f;
    private float buffDuration = 0f;
    public Sprite potionSprite;
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
        if (BuffActive && Time.time > lastBuffTime + buffDuration)
        {
            BuffActive = false;
            attackMultiplier = 1f;
            defenseMultiplier = 1f;
            healthOverTime = 0f;
            magicOverTime = 0f;
            StopAllCoroutines();
        }
    }
    public void Save()
    {
        int index = 0;
        Player player = GameObject.FindObjectOfType<Player>();

        currentHealth = MaxHealth;
        currentMagic = MaxMagic;
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
        currentHealth = MaxHealth;
        currentMagic = MaxMagic;
        coinCount = PlayerPrefs.GetFloat("coinCount", 0);
        attackMultiplier = PlayerPrefs.GetFloat("attackMultiplier", 1f);
        defenseMultiplier = PlayerPrefs.GetFloat("defenseMultiplier", 1f);
        BuffActive = false;

        spawnPosition = new Vector2(PlayerPrefs.GetFloat("spawnPositionX", 0f), PlayerPrefs.GetFloat("spawnPositionY", -0.74f));

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
        if (currentHealth + value <= MaxHealth)
        {
            currentHealth += value;
        }
        else
        {
            currentHealth = MaxHealth;
        }
    }
    public void AddMagic(float value)
    {
        if (currentMagic + value <= MaxMagic)
        {
            currentMagic += value;
        }
        else
        {
            currentMagic = MaxMagic;
        }
    }
    public void AddBuff(float attack, float defense, float HoT, float MoT, float duration)
    {
        BuffActive = true;
        attackMultiplier = 1f + attack;
        defenseMultiplier = 1f - defense;
        healthOverTime = HoT;
        magicOverTime = MoT;
        buffDuration = duration;
        lastBuffTime = Time.time;

        if (HoT > 0f)
        {
            StartCoroutine(HealOverTime());
        }
        if (MoT > 0f)
        {
            StartCoroutine(MagicOverTime());
        }
    }
    private IEnumerator HealOverTime()
    {
        while (healthOverTime > 0f)
        { 
            AddHealth(healthOverTime);
            yield return new WaitForSeconds(1f);
        }
    }
    private IEnumerator MagicOverTime()
    {
        while (magicOverTime > 0f)
        {
            AddMagic(healthOverTime);
            yield return new WaitForSeconds(1f);
        }
    }
}
