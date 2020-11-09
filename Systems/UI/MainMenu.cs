using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class MainMenu : MonoBehaviour
{
    public GameObject optionsEnterSelectedButton, optionsExitSelectedButton, controlsEnterSelectedButton, 
        controlsExitSelectedButton, sceneLoader;
    public void PlayGame()
    {
        // PlayerPrefs.DeleteAll();
        GameStatus.GetInstance().Load();
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
