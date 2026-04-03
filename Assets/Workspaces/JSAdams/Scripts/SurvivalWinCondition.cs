using UnityEngine;

public class SurvivalWinCondition : MonoBehaviour
{
    [SerializeField] private float survivalTime = 15f;
    [SerializeField] private GameOverMenu winMenuPlaceholder;

    private float timer;
    private bool hasWon;

    private void Update()
    {
        if (hasWon) return;

        timer += Time.deltaTime;

        if (timer >= survivalTime)
        {
            TriggerWin();
        }
    }

    private void TriggerWin()
    {
        hasWon = true;

        Debug.Log("YOU WIN");

        winMenuPlaceholder.Show();
    }
}