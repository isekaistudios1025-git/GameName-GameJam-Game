//----- PlayerState.cs START-----

using System;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public event Action<int> OnScoreChanged;
    public event Action<int> OnLivesChanged;

    [Header("Starting Values")]
    [SerializeField] private int startingLives = 3;
    [SerializeField] private int startingScore = 0;

    private int currentLives;
    private int currentScore;

    public int CurrentLives => currentLives;
    public int CurrentScore => currentScore;

    private void Awake()
    {
        currentLives = startingLives;
        currentScore = startingScore;
    }

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        OnLivesChanged?.Invoke(currentLives);
        OnScoreChanged?.Invoke(currentScore);
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        OnScoreChanged?.Invoke(currentScore);
    }

    public void LoseLife(int amount = 1)
    {
        currentLives = Mathf.Max(0, currentLives - amount);
        OnLivesChanged?.Invoke(currentLives);
    }

    public void GainLife(int amount = 1)
    {
        currentLives += amount;
        OnLivesChanged?.Invoke(currentLives);
    }
}
// ----- PlayerState.cs END -----