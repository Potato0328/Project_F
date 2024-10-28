using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemButton : MonoBehaviour
{
    public ItemData itemData;
    private StoreController storeController;
    private Button button;
    private Button itemBuyButton;
    private Button potionButton;
    private Button grenadeButton;

    private bool isPurchased = false;

    private void Start()
    {
        storeController = FindObjectOfType<StoreController>();
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
            potionButton.onClick.RemoveAllListeners();
            potionButton.onClick.AddListener(() => PotionGrenadeBuyButtonClick(potionButton));
        }

        if (grenadeButton != null)
        {
            grenadeButton.onClick.RemoveAllListeners();
            grenadeButton.onClick.AddListener(() => PotionGrenadeBuyButtonClick(grenadeButton));
        }
    }

    public void OnButtonClick()
    {
        if (storeController != null && itemBuyButton != null && itemData != null)
        {
            storeController.ShowExplanation(itemData.itemName, itemData.description, itemData.itemImage);
        }
    }

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
        itemBuyButton.interactable = false;
    }

    public void PotionGrenadeBuyButtonClick(Button clickedButton)
    {
        clickedButton.interactable = false;

        if (GameManager.Instance.PotionGrenadeItem(1000))
        {
            Debug.Log($"���� �Ϸ�");
            Debug.Log($"���� ��: {GameManager.Instance.curPrice}");
        }
        else
        {
            Debug.Log("���� �����Ͽ� ������ �� �����ϴ�.");
            Debug.Log($"���� ��: {GameManager.Instance.curPrice}");
            clickedButton.interactable = true;
        }
    }
}
