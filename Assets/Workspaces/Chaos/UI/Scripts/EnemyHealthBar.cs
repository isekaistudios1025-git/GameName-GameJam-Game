using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private GameObject[] healthbarContainer;
    [SerializeField] private Image healthbarFillImage;
    [SerializeField] private TextMeshPro healthbarText;
    [SerializeField] private ActorController targetActor;



    private void Start()
    {
        foreach (var container in healthbarContainer)
        {
            if (container != null)
            {
                container.SetActive(false);
                Debug.Log($"Health bar container {container.name} set to inactive.");
            }
        }
        if (targetActor != null)
        {
            targetActor.OnHealthChanged += UpdateHeathBar;
            targetActor.InitializeHealth();
        }
    }

    private void UpdateHeathBar(float currentHealth, float maxHealth)
    {
        if (currentHealth == maxHealth)
        {
            foreach (var container in healthbarContainer)
            {
                if (container != null)
                {
                    container.SetActive(false);
                    Debug.Log($"Health bar container {container.name} set to inactive.");
                }
            }
        }
        else
        {
            foreach (var container in healthbarContainer)
            {
                if (container != null)
                {
                    container.SetActive(true);
                    Debug.Log($"Health bar container {container.name} set to active.");
                }
            }
        }

        if (healthbarFillImage != null)
            healthbarFillImage.fillAmount = currentHealth / maxHealth;
        if (healthbarText != null)
            healthbarText.text = $"{currentHealth}/{maxHealth}";
    }

}
