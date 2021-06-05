using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void PlayGame()
    {
        SceneManager.LoadScene("CellularAutomata");
    }

    private void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
