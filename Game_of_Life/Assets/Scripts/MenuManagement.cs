using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagement : MonoBehaviour
{

    public GameObject SettingsUI;
    public void StartSimulate()
    {
        SceneManager.LoadScene("LifeOfGame");
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }

    public void Settings()
    {
        SettingsUI.SetActive(true);
    }

    public void Back()
    {
        SettingsUI.SetActive(false);
    }
}
