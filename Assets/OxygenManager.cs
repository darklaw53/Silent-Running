using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OxygenManager : MonoBehaviour
{
    public float maxOxygen = 100f;  // Maximum oxygen level
    public RectTransform healthBar;  // Health bar UI image
    public RectTransform healthBar2;  // Health bar UI image
    public Image targetImage;  // Target image to modify opacity

    public float currentOxygen;  // Current oxygen level
    private Vector2 initialSize;  // Initial size of the health bar

    void Start()
    {
        currentOxygen = maxOxygen;  // Initialize current oxygen level
        initialSize = healthBar.sizeDelta;  // Store the initial size of the health bar
        UpdateHealthBar();  // Update the health bar initially
        UpdateTargetOpacity();  // Update the target image opacity initially
    }

    void Update()
    {
        // No need to decrease oxygen level since it's not actively decreasing
        UpdateHealthBar();  // Update the health bar
        UpdateTargetOpacity();
    }

    void UpdateHealthBar()
    {
        // Calculate the health bar size based on the oxygen level
        float fillAmount = currentOxygen / maxOxygen;
        float newSizeY = initialSize.y * fillAmount;

        // Set the health bar size
        healthBar.sizeDelta = new Vector2(initialSize.x, newSizeY);
        healthBar2.sizeDelta = new Vector2(initialSize.x, newSizeY);
    }

    void UpdateTargetOpacity()
    {
        // Calculate the opacity based on the oxygen level
        float opacity = currentOxygen / maxOxygen;
        Color targetColor = targetImage.color;
        targetColor.a = opacity;
        targetImage.color = targetColor;
    }
}
