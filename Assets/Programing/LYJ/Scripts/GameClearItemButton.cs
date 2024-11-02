using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameClearItemButton : MonoBehaviour
{
    public ItemData itemData;
    [SerializeField] InGameController inGameController;
    [SerializeField] Button button; //���� ��ư (����â Ȱ��ȭ)
    [SerializeField] Button itemBuyButton;

    private bool isPurchased = false;

    private static List<GameClearItemButton> allButtons = new List<GameClearItemButton>();

    private void Awake()
    {
        allButtons.Add(this);
    }

    private void OnDestroy()
    {
        allButtons.Remove(this);
    }

    private void Start()
    {
        inGameController = FindObjectOfType<InGameController>();
        button = GetComponent<Button>();
        itemBuyButton = transform.Find("Item Buy Button")?.GetComponent<Button>();

        if (button != null )
        {
            button.onClick.AddListener(OnButtonClick);
        }

        if (itemBuyButton != null)
        {
            itemBuyButton.onClick.AddListener(ItemBuyButtonClick);
        }

        SetRandomItemData();
    }

    private void SetRandomItemData()
    {
        if (CSVDownload.Instance != null && CSVDownload.Instance.itemDataList.Count > 0)
        {
            int randomIndex = Random.Range(0, CSVDownload.Instance.itemDataList.Count);
            itemData = CSVDownload.Instance.itemDataList[randomIndex];

            // ������ �����Ϳ� �°� UI ������Ʈ
            button.GetComponentInChildren<TextMeshProUGUI>().text = itemData.itemName;
            itemBuyButton.GetComponent<Image>().sprite = itemData.itemImage;
        }
    }

    /// <summary>
    /// ��ư Ŭ���� ����â Ȱ��ȭ
    /// </summary>
    public void OnButtonClick()
    {
        if (inGameController != null && itemBuyButton != null && itemData != null)
        {
            inGameController.ShowExplanation(itemData.itemName, itemData.description, itemData.itemImage);
        }
    }

    /// <summary>
    /// ��ư Ŭ���� ������ ���� �� �κ��丮 ����
    /// </summary>
    public void ItemBuyButtonClick()
    {
        if (!isPurchased && itemData != null)
        {
            StatusWindowController.Instance.AddItemToInventory(itemData.itemImage, itemData.itemName, itemData.description, itemData.elemental);
            isPurchased = true;

            if (inGameController != null)
            {
                inGameController.ActivateContinueButton();
            }

            Debug.Log($"{itemData.itemName}��(��) �κ��丮�� �߰��Ǿ����ϴ�.");

            DisableOtherButtons();
        }
    }

    //���õ��� ���� ��ư ��Ȱ��ȭ
    private void DisableOtherButtons()
    {
        foreach (var button in allButtons)
        {
            if (button != this && button.itemBuyButton != null)
            {
                button.itemBuyButton.interactable = false;
            }
        }
    }
}
