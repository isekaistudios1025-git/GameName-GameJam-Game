using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quitButton;
    void Start()
    {
        pauseMenuPanel.SetActive(false);
     
        resumeButton.onClick.AddListener(OnResumeButtonClicked);
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    private void OnDestroy()
    {
        resumeButton.onClick.RemoveListener(OnResumeButtonClicked);
        mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
        quitButton.onClick.RemoveListener(OnQuitButtonClicked);
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (pauseMenuPanel.activeSelf)
            {
                OnResumeButtonClicked();
            }
            else
            {
                OnPauseButtonClicked();
            }
        }
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

    private void OnResumeButtonClicked()
    {
       SetPauseState(false);
       pauseMenuPanel.SetActive(false);
    }

    private void OnPauseButtonClicked()
    {
        pauseMenuPanel.SetActive(true);
        SetPauseState(true);
    }

    //Helper method to set the time scale and toggle the pause menu visibility

    private void SetPauseState(bool isPaused)
    {
        Time.timeScale = isPaused ? 0f : 1f;
    }

    
}
