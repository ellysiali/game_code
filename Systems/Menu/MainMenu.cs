using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class MainMenu : MonoBehaviour
{
    public GameObject optionsEnterSelectedButton, optionsExitSelectedButton, controlsEnterSelectedButton, controlsExitSelectedButton, levelLoader;
    [SerializeField] private PlayerData playerData;
    public void PlayGame()
    {
        levelLoader.GetComponent<LevelLoader>().LoadNextLevel();
        playerData.currentHealth = playerData.maxHealth;
        playerData.startXPosition = 0f;
        playerData.startYPosition = -0.74f;
        playerData.flipOnStart = false;
        playerData.coinCount = 0f;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ExitGame()
    {
        SceneManager.LoadScene(0);
    }

    #region Button Selection Functions
    public void EnterOptionsMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsEnterSelectedButton);
    }
    public void ExitOptionsMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsExitSelectedButton);
    }
    public void EnterControlsMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(controlsEnterSelectedButton);
    }
    public void ExitControlsMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(controlsExitSelectedButton);
    }
    #endregion
}
