using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager instance;

    void Awake()
    {
        instance = this;
        //This needs to be in awake because other scripts use PlayerManager.player in Start().
        player = GameObject.FindGameObjectWithTag("Player");
    }
    #endregion

    public GameObject player;
    public GameObject victoryScreen;
    public GameObject loseScreen;
    public AudioClip victoryTheme;
    public AudioClip loseTheme;
    public static bool gameIsPaused = false; //Bool for when game is paused.
    public GameObject menuUI, targetUI; //TargetUI = interface that display name of target.
    public TMP_Text targetText;     //TargetIndicator UI text. Will display focus in UI.
    bool gameHasEnded = false;
    bool continueGame = false;



    public List<GameObject> enemiesList = new List<GameObject>();

    private void Start()
    {
        Resume();
        enemiesList.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
    }
    private void Update()
    {
        WinCondition();
        OpenMenu();
    }
    public void WinCondition()
    {
        if (enemiesList.Count <= 0 || !enemiesList.Contains(GameObject.Find("Boss")) && !continueGame)
        {
            continueGame = true;
            victoryScreen.SetActive(true);
            AudioManager.Instance.ChangeTrack(victoryTheme);
        }
    }
    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            Pause();
            loseScreen.SetActive(true);
            gameHasEnded = true;
            AudioManager.Instance.ChangeTrack(loseTheme);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Pressing Esc opens and closes menu however if return to game button is pressed, it stops working unless
    /// you click on somewhere. Then it works again. Make sure to add pause game function when menuUI is set to active.
    /// </summary>
    public void OpenMenu()
    {
        if (gameHasEnded == false && Input.GetKeyDown(KeyCode.Escape))
        {
            menuUI.SetActive(!menuUI.activeSelf);
            menuUI.transform.GetChild(0).gameObject.SetActive(true);
            menuUI.transform.GetChild(1).gameObject.SetActive(false);
            if (menuUI.activeSelf)
            {
                Pause();
            } else
            {
                Resume();
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;    //Sets time passing to regular.
        gameIsPaused = false;
    }

    void Pause()
    {
        Time.timeScale = 0f;    //Freezes time passing.
        gameIsPaused = true;
    }
}
