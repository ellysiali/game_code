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

    [SerializeField] private float respawnTime = 2f;
    private float respawnTimeStart;
    private bool toRespawn;

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

    #region Unity Callback Functions
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
        CheckRespawn();
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
    #endregion

    #region Data/Saving Functions
    public void Save()
    {
        int index = 0;
        Player player = GameObject.FindObjectOfType<Player>();

        currentHealth = MaxHealth;
        currentMagic = MaxMagic;

        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat("coinCount", coinCount);

        PlayerPrefs.SetFloat("spawnPositionX", player.gameObject.transform.position.x);
        PlayerPrefs.SetFloat("spawnPositionY", player.gameObject.transform.position.y);
        PlayerPrefs.SetInt("scene", SceneManager.GetActiveScene().buildIndex);

        foreach (KeyValuePair<string, int> item in playerInventory.items)
        {
            PlayerPrefs.SetString("inventory_" + index + "_name", item.Key);
            PlayerPrefs.SetInt("inventory_" + index + "_amount", item.Value);
            index++;
        }

        index = 0;
        foreach (KeyValuePair<string, int> entry in NPCIndexes)
        {
            PlayerPrefs.SetString("NPC_" + index + "_name", entry.Key);
            PlayerPrefs.SetInt("NPC_" + index + "_index", entry.Value);
            index++;
        }

        PlayerPrefs.Save();
    }
    public void Load()
    {
        SceneLoader sceneLoader = GameObject.FindObjectOfType<SceneLoader>();
        int index = 0;
        currentHealth = MaxHealth;
        currentMagic = MaxMagic;
        coinCount = PlayerPrefs.GetFloat("coinCount", 0);

        attackMultiplier = 1f;
        defenseMultiplier = 1f;
        healthOverTime = 0f;
        magicOverTime = 0f;
        BuffActive = false;
        StopAllCoroutines();

        spawnPosition = new Vector2(PlayerPrefs.GetFloat("spawnPositionX", 0f), PlayerPrefs.GetFloat("spawnPositionY", -0.74f));

        playerInventory.items.Clear();
        while (PlayerPrefs.HasKey("inventory_" + index + "_name"))
        {
            playerInventory.AddItem(PlayerPrefs.GetString("inventory_" + index + "_name"), PlayerPrefs.GetInt("inventory_" + index + "_amount"));
            index++;
        }
        index = 0;
        NPCIndexes.Clear();
        while (PlayerPrefs.HasKey("NPC_" + index + "_name"))
        {
            NPCIndexes.Add(PlayerPrefs.GetString("NPC_" + index + "_name"), PlayerPrefs.GetInt("NPC_" + index + "_index"));
            index++;
        }

        sceneLoader.LoadScene(PlayerPrefs.GetInt("scene", 1));
    }
    public void Respawn()
    {
        respawnTimeStart = Time.time;
        toRespawn = true;
    }
    private void CheckRespawn()
    {
        if (Time.time >= respawnTimeStart + respawnTime && toRespawn)
        {
            GameStatus.GetInstance().Load();
            toRespawn = false;
        }
    }
    #endregion

    #region Consumable Functions
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
    #endregion
}
