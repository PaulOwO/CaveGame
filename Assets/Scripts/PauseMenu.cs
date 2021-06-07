using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using InControl;

public class PauseMenu : MonoBehaviour
{
    private bool _gameIsPaused = false; 

    [SerializeField] GameObject PauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape)) || (InputManager.ActiveDevice.Action3.WasPressed))
        {
            if (_gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        _gameIsPaused = false;
    }

    private void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("CellularAutomata");
    }

    private void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        _gameIsPaused = true;
    }

    private void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
        Debug.Log("Loading Menu");
    }

    private void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting game");
    }
}
