using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeathBar : MonoBehaviour
{
    [SerializeField] private Slider heathbarSlider;
    [SerializeField] private TextMeshProUGUI heathbarText;

   

    private void UpdateHeathBar(float currentHealth, float maxHealth)
    {
        heathbarSlider.value = currentHealth / maxHealth;

        heathbarText.text = $"{currentHealth}/{maxHealth}";
    }

}
