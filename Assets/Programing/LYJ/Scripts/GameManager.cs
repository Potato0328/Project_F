using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] public float curPrice = 10000f;
    [SerializeField] TextMeshProUGUI priceText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdatePriceText();
    }

    /// <summary>
    /// ������ ���� (�� ����) - CSV�� �޾ƿ� �����۸� ����
    /// </summary>
    /// <param name="itemPrice"></param>
    /// <returns></returns>
    public bool PurchaseItem(float itemPrice)
    {
        if (curPrice >= itemPrice)
        {
            curPrice -= itemPrice;
            UpdatePriceText();
            return true;
        }
        return false;
    }
    /// <summary>
    /// ������ ���� - ����, ����ź ����
    /// </summary>
    /// <param name="price"></param>
    /// <returns></returns>
    public bool PotionGrenadeItem(float price)
    {
        if (curPrice >= price)
        {
            curPrice -= price;
            UpdatePriceText();
            return true;
        }
        else
        {
            Debug.Log("���� �����Ͽ� ������ �� �����ϴ�.");
            return false;
        }
    }

    private void UpdatePriceText()
    {
        if (priceText != null)
        {
            priceText.text = $"Possession Gold: {curPrice} G";
        }
    }
}
