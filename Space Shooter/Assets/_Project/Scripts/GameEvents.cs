using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance;

    public event Action<int> OnPlayerHealthChanged;
    public event Action OnPlayerKilled;
    public event Action OnPlayerShoot;
    public event Action OnEnemyKilled;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    public void InvokePlayerHealthChanged(int currentHealth)
    {
        OnPlayerHealthChanged?.Invoke(currentHealth);
    }

    public void InvokePlayerKilledEvent()
    {
        OnPlayerKilled?.Invoke();
    }

    public void InvokeEnemyKilledEvent()
    {
        OnEnemyKilled?.Invoke();
    }

    public void InvokePlayerShootEvent()
    {
        OnPlayerShoot?.Invoke();
    }
}
