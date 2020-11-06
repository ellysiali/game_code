using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class MainMenu : MonoBehaviour
{
    public GameObject optionsEnterSelectedButton, optionsExitSelectedButton, controlsEnterSelectedButton, controlsExitSelectedButton, levelLoader;
    public void PlayGame()
    {
        levelLoader.GetComponent<LevelLoader>().LoadNextLevel();
        GameStatus.GetInstance().spawnPosition = new Vector2(0f,-0.74f);
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
