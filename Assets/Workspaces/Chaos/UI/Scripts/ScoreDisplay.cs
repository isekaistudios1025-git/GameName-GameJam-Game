//----- ScoreDisplay.cs START-----

using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private PlayerState playerState;
    [SerializeField] private TextMeshProUGUI scoreText;

    private int displayedScore;
    private int targetScore;

    public float countSpeed = 200f;
    public float popScale = 1.3f;
    public float popDuration = 0.15f;

    private Vector3 originalScale;

    private void Start()
    {
        originalScale = scoreText.transform.localScale;

        if (playerState != null)
        {
            playerState.OnScoreChanged += OnScoreChanged;
            OnScoreChanged(playerState.CurrentScore);
        }
    }

    private void OnDestroy()
    {
        if (playerState != null)
        {
            playerState.OnScoreChanged -= OnScoreChanged;
        }
    }

    private void OnScoreChanged(int newScore)
    {
        targetScore = newScore;
        StopAllCoroutines();
        StartCoroutine(AnimateScore());
        StartCoroutine(PopEffect());
    }

    private IEnumerator AnimateScore()
    {
        while (displayedScore < targetScore)
        {
            displayedScore += Mathf.CeilToInt(countSpeed * Time.deltaTime);
            displayedScore = Mathf.Min(displayedScore, targetScore);
            scoreText.text = displayedScore.ToString("D8");
            yield return null;
        }
    }

    private IEnumerator PopEffect()
    {
        float time = 0f;

        while (time < popDuration)
        {
            time += Time.deltaTime;
            scoreText.transform.localScale =
                Vector3.Lerp(originalScale, originalScale * popScale, time / popDuration);
            yield return null;
        }

        time = 0f;

        while (time < popDuration)
        {
            time += Time.deltaTime;
            scoreText.transform.localScale =
                Vector3.Lerp(originalScale * popScale, originalScale, time / popDuration);
            yield return null;
        }

        scoreText.transform.localScale = originalScale;
    }
}

//----- ScoreDisplay.cs END-----