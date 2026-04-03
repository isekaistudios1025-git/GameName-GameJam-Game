//----- LivesDisplay.cs START-----


using TMPro;
using UnityEngine;

public class LivesDisplay : MonoBehaviour
{
    [SerializeField] private PlayerState playerState;
    [SerializeField] private TextMeshProUGUI livesText;

    private void Start()
    {
        if (playerState != null)
        {
            playerState.OnLivesChanged += UpdateLives;
            UpdateLives(playerState.CurrentLives);
        }
    }

    private void OnDestroy()
    {
        if (playerState != null)
        {
            playerState.OnLivesChanged -= UpdateLives;
        }
    }

    private void UpdateLives(int lives)
    {
        livesText.text = lives.ToString();
    }
}

//----- LivesDisplay.cs END-----