using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public Game_of_LIfe gameOfLife;
    public GameObject pauseUI;
    public bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TogglePause();  
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            ClearCells();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("LifeOfGame");
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            pauseUI.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseUI.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void ClearCells()
    {
        gameOfLife.ClearAllCells();
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
