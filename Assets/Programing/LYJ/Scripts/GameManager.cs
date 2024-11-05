using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] public float curPrice = 0f;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] TextMeshProUGUI goldText;

    [SerializeField] TextMeshProUGUI gainPriceText;

    [SerializeField] Image hpBar;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] public float currentHealth;
    [SerializeField] public float maxHealth = 100f;

    [SerializeField] private TextMeshProUGUI potionCountText;
    [SerializeField] private TextMeshProUGUI grenadeCountText;
    private int potionCount = 0;
    private int grenadeCount = 0;
    private const int maxItemCount = 3;

    [SerializeField] private TextMeshProUGUI stageNumberText;
    [SerializeField] private TextMeshProUGUI waveNumberText;

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
        curPrice = 0; //�ʱ� ��� ��
        curPrice = 20000;
        currentHealth = maxHealth;

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

        if (Input.GetKeyDown(KeyCode.E))
        {
            UsePotion();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            UseGrenade();
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
        Debug.Log("���� �����Ͽ� ������ �� �����ϴ�.");
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
        if (priceText != null && goldText != null)
        {
            priceText.text = $"���� ���: {curPrice} G";
            goldText.text = $"���� ���: {curPrice} G";
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
    //private void OnTriggerEnter(Collider collider)
    //{
    //    if (collider.CompareTag("Potion"))
    //    {
    //        potionCount++;
    //        UpdateUI();
    //        Destroy(collider.gameObject);
    //    }
    //    else if (collider.CompareTag("Grenade"))
    //    {
    //        grenadeCount++;
    //        UpdateUI();
    //        Destroy(collider.gameObject);
    //    }
    //}

    private void UpdateUI()
    {
        if (potionCountText != null)
            potionCountText.text = $"{potionCount}";

        if (grenadeCountText != null)
            grenadeCountText.text = $"{grenadeCount}";
    }

    public void IncrementPotionCount()
    {
        if (potionCount <= maxItemCount)
        {
            potionCount++;
            UpdateUI();
        }
    }

    public void IncrementGrenadeCount()
    {
        if (grenadeCount <= maxItemCount)
        {
            grenadeCount++;
            UpdateUI();
        }
    }

    public int GetPotionCount() => potionCount;
    public int GetGrenadeCount() => grenadeCount;

    /// <summary>
    /// ���ظ� �޴� �޼���
    /// </summary>
    /// <param name="damageAmount"></param>
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHealthUI();
        if (currentHealth <= 100)
        {
            UIManager.Instance.ShowUI("Game Over Canvas");
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

    private void UpdateGainedGoldText(float amount)
    {
        if (gainPriceText != null)
        {
            gainPriceText.text = $"{amount} G";
        }
    }

    /// <summary>
    /// ��带 �߰�
    /// </summary>
    /// <param name="amount">��� ��</param>
    public void AddGold(float amount)
    {
        curPrice += amount;
        UpdatePriceText();
        UpdateGainedGoldText(amount);
    }

    public void UsePotion()
    {
        if (potionCount > 0)
        {
            if (currentHealth < maxHealth)
            {
                float healthToRestore = maxHealth * 0.5f;
                currentHealth += healthToRestore;
                currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
                potionCount--;
                UpdateUI();
                UpdateHealthUI();
                Debug.Log($"ü���� ȸ���Ǿ����ϴ�. ���� ü��: {currentHealth}");
            }
            else
            {
                Debug.Log("ü���� �̹� �ִ��Դϴ�. ������ ����� �� �����ϴ�.");
            }
        }
        else
        {
            Debug.Log("������ �����ϴ�.");
        }
    }

    public void DecrementPotionCount()
    {
        if (potionCount > 0)
        {
            potionCount--;
            UpdateUI();
        }
    }

    public void UseGrenade()
    {
        if (grenadeCount > 0)
        {
            grenadeCount--;
            UpdateUI();
            // ����ź ��� �ڵ� �߰�
            Debug.Log("����ź�� ����߽��ϴ�.");
        }
        else
        {
            Debug.Log("����ź�� �����ϴ�.");
        }
    }
}
