using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    public void LoadNextLevel()
    {
        StartCoroutine(Load(SceneManager.GetActiveScene().buildIndex + 1));
    }
    public void LoadLevel(int buildIndex)
    {
        StartCoroutine(Load(buildIndex));
    }
    IEnumerator Load(int buildIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(buildIndex);
    }
}
