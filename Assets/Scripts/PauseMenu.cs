using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using InControl;

public class PauseMenu : MonoBehaviour
{
    private bool GameIsPaused = false; 

    [SerializeField] GameObject PauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape)) || (InputManager.ActiveDevice.Action3.WasPressed))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Retry()
    {
        SceneManager.LoadScene("CellularAutomata");
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
        Debug.Log("Loading Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting game");
    }
}
