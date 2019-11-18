using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour {

    const int MaxHealth = 80;
    //public CharacterStats CharacterStats;
    public int EquipmentID = 1;

    public int PlayerNumber = 1;
    public int HealthUnlocked = 12;
    public int CurrentHealth;
    public PlayerController playerController;
    


    private void Awake()
    {
        SetHealthUnlocked(HealthUnlocked);
        playerController = GetComponent<PlayerController>();
    }


    private void Start()
    {
        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        UIEventHandler.UpdateHealth(PlayerNumber, HealthUnlocked, CurrentHealth);
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        if(CurrentHealth < 0) CurrentHealth = 0;
        if(CurrentHealth <= 0)
        {
            Die();
        }
        UIEventHandler.UpdateHealth(PlayerNumber, HealthUnlocked, CurrentHealth);
    }

    //void OnGUI()
    //{
    //    if (GUI.Button(new Rect(10, 500, 80, 30), "Do Damage"))
    //    {
    //        TakeDamage(Random.Range(1, 10));
    //    }
    //}

    public void Eat(int value)
    {
        CurrentHealth += (CurrentHealth <= HealthUnlocked) ? value : HealthUnlocked;
        UIEventHandler.UpdateHealth(PlayerNumber, HealthUnlocked, CurrentHealth);
    }

    private void SetHealthUnlocked(int value)
    {
        
        HealthUnlocked = HealthUnlocked > MaxHealth ? MaxHealth : value;
        while (HealthUnlocked % 4 != 0) HealthUnlocked++;
        CurrentHealth = HealthUnlocked;
    }


    public void AddHeart()
    {
        SetHealthUnlocked(++HealthUnlocked);
        UIEventHandler.UpdateHealth(PlayerNumber, HealthUnlocked, CurrentHealth);
    }

    private void Die()
    {
        CurrentHealth = HealthUnlocked;
        //UIEventHandler.HealthChanged(MaxHealth, CurrentHealth);
    }
}
