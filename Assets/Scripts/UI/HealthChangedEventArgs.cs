using System;

public class HealthChangedEventArgs : EventArgs
{
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }
    public int PlayerNumber { get; set; }

    public HealthChangedEventArgs(int playerNumber, int maxHealth, int currentHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = currentHealth;
        PlayerNumber = playerNumber;
    }
}
