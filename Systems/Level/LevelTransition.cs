using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private LevelLoader levelLoader;
    public float xPosition, yPosition;
    public int sceneIndex;
    private Vector2 workspace;
    private Player player;

    private void Start()
    {
        levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
        player = FindObjectOfType<Player>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            workspace.Set(xPosition, yPosition);
            GameStatus.GetInstance().spawnPosition = workspace;
            if (player.facingDirection == -1)
            {
                GameStatus.GetInstance().flipOnStart = true;
            }
            else
            {
                GameStatus.GetInstance().flipOnStart = false;
            }
            levelLoader.GetComponent<LevelLoader>().LoadLevel(sceneIndex);
        }
    }
}