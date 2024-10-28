using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
    [SerializeField] Image hpBar;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] float currentHealth;
    [SerializeField] float maxHealth = 100f;

    private void Start()
    {
        hpBar = GetComponent<Image>();

        if (hpText == null)
        {
            hpText = GetComponentInChildren<TextMeshProUGUI>();
        }

        currentHealth = maxHealth;
    }

    public void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); //�ּ� 0, �ִ� 100
        float healthRatio = currentHealth / maxHealth; //ü�� ���� ���
        hpBar.fillAmount = healthRatio; //Fill Amount ����

        hpText.text = $"{(int)currentHealth} / {maxHealth}";
    }
}
