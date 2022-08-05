using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //This will be used to load saved game. Will call player pref.
    public int levelToBeLoaded;


    public TMP_Text loadingText;
    public TMP_Text progressText;
    public GameObject loadingScreen;
    public Slider loadingSlider;


    //SceneManager.GetActiveScene() may need to be set to playerpref so that it loads saved game.
    public void LoadLevelTransition()
    {
        StartCoroutine(LoadAsynchronously(levelToBeLoaded));
    }
    public void LoadLevelNoTransition()
    {
        SceneManager.LoadScene(levelToBeLoaded);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void LoadNextLevel(int sceneIndex)
    {
        //StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int levelIndex)
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
        //Prevents scene from activating after load.
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            //Checks to see if UI exists.
            if (loadingSlider != null)
            {
                loadingSlider.value = progress;
            }
            if (progressText != null)
            {
                progressText.text = (int)(progress * 100f) + "%";
            }

            if (progress >= 0.9f)
            {
                if (loadingText != null)
                {
                    loadingText.text = "Loading Complete\nPress Space or Click to Continue.";
                }

                if (Input.GetKeyDown("space") || Input.GetMouseButton(0))
                {
                    operation.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }
}
