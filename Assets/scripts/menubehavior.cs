using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehavior : MonoBehaviour
{
    public void StartGame1()
    {
        SceneManager.LoadScene(1);
    }
    public void StartGame2()
    {
        SceneManager.LoadScene(2);
    }
    public void StartGame3()
    {
        SceneManager.LoadScene(3);
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    public void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}

