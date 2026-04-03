//----- HealthBar.cs START-----
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [Header("Optional UI")]
    [SerializeField] private Slider healthbarSlider;   // player HUD only
    [SerializeField] private Image healthbarFillImage; // enemy + player
    [SerializeField] private TMP_Text healthbarText;

    [Header("Target")]
    [SerializeField] private ActorController targetActor;

    [Header("Enemy Only")]
    [SerializeField] private GameObject[] hideAtFullHealth;

    private void Start()
    {
        if (targetActor != null)
        {
            targetActor.OnHealthChanged += UpdateHealthBar;
            targetActor.InitializeHealth();
        }
    }

    private void OnDestroy()
    {
        if (targetActor != null)
        {
            targetActor.OnHealthChanged -= UpdateHealthBar;
        }
    }

    private void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        float normalized = currentHealth / maxHealth;

        // slider support
        if (healthbarSlider != null)
        {
            healthbarSlider.value = normalized;
        }

        // image fill support
        if (healthbarFillImage != null)
        {
            healthbarFillImage.fillAmount = normalized;
        }

        // text support
        if (healthbarText != null)
        {
            healthbarText.text = $"{currentHealth}/{maxHealth}";
        }

        // optional hide when full HP (enemy bars)
        if (hideAtFullHealth != null && hideAtFullHealth.Length > 0)
        {
            bool shouldShow = currentHealth < maxHealth;

            foreach (GameObject obj in hideAtFullHealth)
            {
                if (obj != null)
                    obj.SetActive(shouldShow);
            }
        }
    }
}
//----- HealthBar.cs END-----