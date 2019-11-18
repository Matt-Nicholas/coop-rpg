using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventHandler
{
    public delegate void HealthChangedEventHandler(int playerNumber, int maxHealth, int currentHealth);
    public static event HealthChangedEventHandler HealthChanged = delegate { };

    public static void UpdateHealth(int playerNumber, int maxHealth, int currentHealth)
    {
        HealthChanged(playerNumber, maxHealth, currentHealth);
    }
}
