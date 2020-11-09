using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private SceneLoader sceneLoader;
    public float xPosition, yPosition;
    public int sceneIndex;
    private Vector2 workspace;
    private Player player;

    private void Start()
    {
        sceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
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
            sceneLoader.GetComponent<SceneLoader>().LoadScene(sceneIndex);
        }
    }
}