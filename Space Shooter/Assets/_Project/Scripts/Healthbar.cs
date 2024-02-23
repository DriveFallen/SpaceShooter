using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    private Image _slider;

    private void Awake()
    {
        _slider = GetComponent<Image>();
    }

    private void OnEnable()
    {
        GameEvents.Instance.OnPlayerHealthChanged += UpdateHealthbar;
    }

    private void OnDisable()
    {
        GameEvents.Instance.OnPlayerHealthChanged -= UpdateHealthbar;
    }

    private void UpdateHealthbar(int currentHealth)
    {
        _slider.fillAmount = (float)currentHealth/100;
    }
}
