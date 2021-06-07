using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
   
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
    
    private void Retry()
    {
        SceneManager.LoadScene("CellularAutomata");
    }
}
