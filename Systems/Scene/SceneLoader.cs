using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    public void LoadNextScene()
    {
        StartCoroutine(Load(SceneManager.GetActiveScene().buildIndex + 1));
    }
    public void LoadScene(int buildIndex)
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
