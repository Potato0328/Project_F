using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemButton : MonoBehaviour
{
    public ItemData itemData;
    private StoreController storeController;
    [SerializeField] Button button;
    [SerializeField] Button itemBuyButton;

    private bool isPurchased = false;

    private void Start()
    {
        storeController = FindObjectOfType<StoreController>();
        button = GetComponent<Button>();
        itemBuyButton = transform.Find("Item Buy Button").GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }

        if (itemBuyButton != null)
        {
            itemBuyButton.onClick.AddListener(ItemBuyButtonClick);
        }
    }

    public void OnButtonClick()
    {
        if (storeController != null && itemData != null)
        {
            storeController.ShowExplanation(itemData.itemName, itemData.description, itemData.itemImage);
        }
    }

    public void ItemBuyButtonClick()
    {
        if (isPurchased) // �̹� ������ ���
        {
            Debug.Log($"{itemData.itemName}�� �̹� ���ŵǾ����ϴ�.");
            return; // �������� �ʵ��� ���� ����
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
        ColorBlock colors = itemBuyButton.colors;
        colors.normalColor = Color.gray;
        colors.disabledColor = Color.gray;
        itemBuyButton.colors = colors;
        itemBuyButton.interactable = false;
    }
}
