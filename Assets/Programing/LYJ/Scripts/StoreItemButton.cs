using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemButton : MonoBehaviour
{
    public ItemData itemData;
    private InGameController inGameController;
    private StatusWindowController statusWindowController;
    [SerializeField] Button button; //�̸� ������ �ִ� (����â Ȱ��ȭ)
    [SerializeField] Button itemBuyButton;
    [SerializeField] Button potionButton;
    [SerializeField] Button grenadeButton;

    private bool isPurchased = false; //���Ű� �Ǿ����� Ȯ��

    private void OnEnable()
    {
        InitializeButtons();
        ResetButtonStates();
    }

    private void Start()
    {
        inGameController = FindObjectOfType<InGameController>();
        statusWindowController = StatusWindowController.Instance;
    }

    private void InitializeButtons()
    {
        button = GetComponent<Button>();
        itemBuyButton = transform.Find("Item Buy Button")?.GetComponent<Button>();
        potionButton = GameObject.Find("Potion Buy Button").GetComponent<Button>();
        grenadeButton = GameObject.Find("Grenade Buy Button").GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }

        if (itemBuyButton != null)
        {
            itemBuyButton.onClick.AddListener(ItemBuyButtonClick);
        }

        if (potionButton != null)
        {
            potionButton.onClick.RemoveAllListeners(); //��ư ������ �ߺ� ���� (������ ȣ��Ǵ� ���� �ذ�)
            potionButton.onClick.AddListener(() => PotionGrenadeBuyButtonClick(potionButton));
        }

        if (grenadeButton != null)
        {
            grenadeButton.onClick.RemoveAllListeners(); //��ư ������ �ߺ� ���� (������ ȣ��Ǵ� ���� �ذ�)
            grenadeButton.onClick.AddListener(() => PotionGrenadeBuyButtonClick(grenadeButton));
        }
    }

    private void ResetButtonStates()
    {
        isPurchased = false;
        if (itemBuyButton != null)
        {
            itemBuyButton.interactable = true;
        }
        if (potionButton != null)
        {
            potionButton.interactable = true;
        }
        if (grenadeButton != null)
        {
            grenadeButton.interactable = true;
        }
    }


    /// <summary>
    /// ���� ��ũ��Ʈ�� ������ �ִ� ��ư Ŭ���� �����͸� ������
    /// </summary>
    public void OnButtonClick()
    {
        if (inGameController != null && itemBuyButton != null && itemData != null)
        {
            SoundManager.Instance.ButtonClickSound();
            inGameController.ShowExplanation(itemData.itemName, itemData.description, itemData.itemImage, itemData.elemental);
        }
    }

    /// <summary>
    /// ������ ���� ��ư Ŭ���� - ���� ����ź X
    /// </summary>
    public void ItemBuyButtonClick()
    {
        if (isPurchased)
        {
            Debug.Log($"{itemData.itemName}�� �̹� ���ŵǾ����ϴ�.");
            return;
        }

        bool success = GameManager.Instance.PurchaseItem(itemData.price);
        if (success)
        {
            if (statusWindowController != null)
            {
                statusWindowController.AddItemToInventory(itemData.itemImage, itemData.itemName, itemData.description, itemData.elemental);
            }
            SoundManager.Instance.BuyItemSound();
            Debug.Log($"{itemData.itemName}��(��) �����߽��ϴ�.");
            Debug.Log($"���� ��: {GameManager.Instance.curPrice}");
            isPurchased = true;
            UpdateButtonState();
        }
        else
        {
            Debug.Log("���� �����Ͽ� ������ �� �����ϴ�.");
            Debug.Log($"���� ��: {GameManager.Instance.curPrice}");
        }
    }

    private void UpdateButtonState()
    {
        itemBuyButton.interactable = false; //��ư ��Ȱ��ȭ
    }

    /// <summary>
    /// ����, ����ź ���� ��ư Ŭ����
    /// </summary>
    /// <param name="clickedButton"></param>
    public void PotionGrenadeBuyButtonClick(Button clickedButton)
    {
        clickedButton.interactable = false;

        //��ư Ŭ�� �� ���ǰ� ����ź ���ݿ� ���� ���� ���θ� ����
        if (clickedButton == potionButton)
        {
            if (GameManager.Instance.PotionGrenadeItem(1000))
            {
                SoundManager.Instance.BuyItemSound();
                GameManager.Instance.IncrementPotionCount();
                Debug.Log($"���� ���� �Ϸ�");
                Debug.Log($"���� ��: {GameManager.Instance.curPrice}");
            }
            else
            {
                Debug.Log("���� �����Ͽ� ������ ������ �� �����ϴ�.");
                Debug.Log($"���� ��: {GameManager.Instance.curPrice}");
                clickedButton.interactable = true;
            }
        }
        else if (clickedButton == grenadeButton)
        {
            if (GameManager.Instance.PotionGrenadeItem(1000))
            {
                SoundManager.Instance.BuyItemSound();
                GameManager.Instance.IncrementGrenadeCount();
                Debug.Log($"����ź ���� �Ϸ�");
                Debug.Log($"���� ��: {GameManager.Instance.curPrice}");
            }
            else
            {
                Debug.Log("���� �����Ͽ� ����ź�� ������ �� �����ϴ�.");
                Debug.Log($"���� ��: {GameManager.Instance.curPrice}");
                clickedButton.interactable = true;
            }
        }
    }

}
