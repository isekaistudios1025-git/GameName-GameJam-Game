using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private GameObject gameOverMenuPanel;
    [SerializeField] private Button replayButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quitButton;
    void Start()
    {
        gameOverMenuPanel.SetActive(false);

        replayButton.onClick.AddListener(OnReplayButtonClicked);
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    private void OnDestroy()
    {
        replayButton.onClick.RemoveListener(OnReplayButtonClicked);
        mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
        quitButton.onClick.RemoveListener(OnQuitButtonClicked);
    }

    private void OnReplayButtonClicked()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnQuitButtonClicked()
    {
        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
    }

    private void OnMainMenuButtonClicked()
    {
        SetPauseState(false);
        SceneManager.LoadScene(0);
    }

     

    //Helper method to set the time scale and toggle the pause menu visibility

    private void SetPauseState(bool isPaused)
    {
        Time.timeScale = isPaused ? 0f : 1f;
    }

    
}
