using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthbarSlider;
    [SerializeField] private Image healthbarFillImage;
    [SerializeField] private TextMeshProUGUI healthbarText;
    [SerializeField] private TextMeshProUGUI playerLivesText;
    [SerializeField] private ActorController targetActor;

   

    private void Start()
    {
        if (targetActor != null)
        {
            targetActor.OnHealthChanged += UpdateHeathBar;
            targetActor.InitializeHealth();
        }

      

    }

    private void OnDestroy()
    {
        if (targetActor != null)
        {
            targetActor.OnHealthChanged -= UpdateHeathBar;
        }

    }
    private void UpdateHeathBar(float currentHealth, float maxHealth)
    {
        if (healthbarSlider != null)
        {
            healthbarSlider.value = currentHealth / maxHealth;
        }

        healthbarText.text = $"{currentHealth}/{maxHealth}";
    }

    private void UpdatePlayerLives(int lives)
    {
        playerLivesText.text = $"{lives}";
    }


   
}
