using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
   
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
    
    public void Retry()
    {
        SceneManager.LoadScene("CellularAutomata");
    }
}
