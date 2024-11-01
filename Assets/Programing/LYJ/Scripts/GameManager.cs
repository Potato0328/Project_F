using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] public float curPrice = 10000f;
    [SerializeField] TextMeshProUGUI priceText;

    [SerializeField] Image hpBar;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] public float currentHealth;
    [SerializeField] public float maxHealth = 100f;

    [SerializeField] private TextMeshProUGUI potionCountText;
    [SerializeField] private TextMeshProUGUI grenadeCountText;
    private int potionCount = 0;
    private int grenadeCount = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //���� �ε�� �� ȣ��Ǵ� �ʱ�ȭ �޼ҵ�
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetGameState();
    }

    private void ResetGameState()
    {
        //�ʿ��� ���� �ʱ�ȭ (�Ʒ� ������ ���� �ʿ信 ���� ����)
        potionCount = 0;
        grenadeCount = 0;
        curPrice = 10000f; //�ʱ� ��� ������ ����
        currentHealth = maxHealth; //ü���� �ִ�ġ�� ����

        UpdateUI();
        UpdatePriceText();
        UpdateHealthUI();
    }

    private void Start()
    {
        InitializeHealth();
        UpdatePriceText();
        UpdateUI();
    }

    private void InitializeHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    private void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (UIManager.Instance.IsUIActive("Status Window Canvas"))
            {
                UIManager.Instance.HideUI("Status Window Canvas");
            }
            else
            {
                UIManager.Instance.ShowUI("Status Window Canvas");
            }
            Time.timeScale = UIManager.Instance.IsUIActive("Status Window Canvas") ? 0 : 1;
        }
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

    private void UpdateHealthUI()
    {
        float healthRatio = currentHealth / maxHealth;
        if (hpBar != null)
        {
            hpBar.fillAmount = healthRatio;
        }

        if (hpText != null)
        {
            hpText.text = $"{(int)currentHealth} / {maxHealth}";
        }
    }

    // ������ ���� ��� (�÷��̾�.cs�� �ٿ� ����� �� ����� ���)
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Potion"))
        {
            potionCount++;
            UpdateUI();
            Destroy(collider.gameObject);
        }
        else if (collider.CompareTag("Grenade"))
        {
            grenadeCount++;
            UpdateUI();
            Destroy(collider.gameObject);
        }
    }

    private void UpdateUI()
    {
        if (potionCountText != null)
            potionCountText.text = $"{potionCount}";

        if (grenadeCountText != null)
            grenadeCountText.text = $"{grenadeCount}";
    }

    public void IncrementPotionCount()
    {
        potionCount++;
        UpdateUI();
    }

    public void IncrementGrenadeCount()
    {
        grenadeCount++;
        UpdateUI();
    }

    /// <summary>
    /// ���ظ� �޴� �޼���
    /// </summary>
    /// <param name="damageAmount"></param>
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHealthUI();
        if (currentHealth <= 0)
        {
            //������� �� ���ó��
            Debug.Log("�÷��̾ ����߽��ϴ�.");
        }
    }

    /// <summary>
    /// ü���� ȸ���ϴ� �޼���
    /// </summary>
    /// <param name="percentage">ȸ���� ���� (0~1) 0.7 = 70% </param>
    public void Heal(float percentage)
    {
        currentHealth = maxHealth * percentage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
        Debug.Log($"ü���� {percentage * 100}%�� ȸ���Ǿ����ϴ�.");
    }
}
