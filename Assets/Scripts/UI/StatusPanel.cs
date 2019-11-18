using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusPanel : MonoBehaviour
{
    List<Transform> _playerOneHearts = new List<Transform>();
    List<Slider> _playerOneHeartSlider = new List<Slider>();

    void Awake()
    {
        UIEventHandler.HealthChanged += OnHealthChanged;

        Transform healthPanel = transform.Find("PlayerOne").transform.Find("HealthPanel");
        int healthCount = healthPanel.childCount;
        for (int i = 0; i < healthCount; i++)
        {
            _playerOneHearts.Add(healthPanel.GetChild(i));

            _playerOneHeartSlider.Add(_playerOneHearts[i].GetComponent<Slider>());

            _playerOneHearts[i].gameObject.SetActive(false);
        }
    }

    private void OnHealthChanged(int playerNumber, int maxHealth, int currentHealth)
    {
        _playerOneHeartSlider.ForEach(n => n.SetValueWithoutNotify(0));

        if (playerNumber == 1)
        {
            int index = 0;
            for (int i = 0; i < (maxHealth / 4); i++)
            {
                _playerOneHearts[i].gameObject.SetActive(true);
                if (i < (currentHealth / 4))
                {
                    _playerOneHeartSlider[i].SetValueWithoutNotify(1);
                    index++;
                }
            }

            int remainder = currentHealth % 4;
            if (remainder > 0)
            {
                _playerOneHearts[index].gameObject.SetActive(true);
                float percentage = (float)remainder/ (float)4;
                _playerOneHeartSlider[index].SetValueWithoutNotify(percentage);
            }
        }
    }
}
