using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

public class AnimatedScore : MonoBehaviour
{
    public static AnimatedScore Instance { get; private set; }

    public TextMeshProUGUI scoreText;

    private int currentScore = 0;
    private int displayedScore = 0;

    public float countSpeed = 200f;
    public float popScale = 1.3f;
    public float popDuration = 0.15f;

    private Vector3 originalScale;

    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        originalScale = scoreText.transform.localScale;
        UpdateText();
    }
 
    public void AddScore(int amount)
    {
        currentScore += amount;
        StopAllCoroutines();
        StartCoroutine(AnimateScore());
        StartCoroutine(PopEffect());
    }

    IEnumerator AnimateScore()
    {
        while (displayedScore < currentScore)
        {
            displayedScore += Mathf.CeilToInt(countSpeed * Time.deltaTime);
            displayedScore = Mathf.Min(displayedScore, currentScore);
            UpdateText();
            yield return null;
        }
    }

    IEnumerator PopEffect()
    {
        float time = 0;

        // Scale up
        while (time < popDuration)
        {
            time += Time.deltaTime;
            float t = time / popDuration;
            scoreText.transform.localScale = Vector3.Lerp(originalScale, originalScale * popScale, t);
            yield return null;
        }

        time = 0;

        // Scale back down
        while (time < popDuration)
        {
            time += Time.deltaTime;
            float t = time / popDuration;
            scoreText.transform.localScale = Vector3.Lerp(originalScale * popScale, originalScale, t);
            yield return null;
        }

        scoreText.transform.localScale = originalScale;
    }

    void UpdateText()
    {
        scoreText.text = displayedScore.ToString("D8");
    }
}