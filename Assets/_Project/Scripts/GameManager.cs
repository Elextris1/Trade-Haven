using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    [SerializeField] private GameObject pauseMenu;

    public void Start()
    {
        #region Singleton
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        #endregion
        Application.targetFrameRate = 60;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void PauseGame ()
    {
        InputManager.input.Player.Disable();
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void UnPauseGame ()
    {
        InputManager.input.Player.Enable();
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void QuitGame() 
    {
        Application.Quit();
    }
}
