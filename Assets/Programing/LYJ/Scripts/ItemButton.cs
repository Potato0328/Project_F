using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public ItemData itemData;
    private GameController gameController;
    private Button button;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    /// <summary>
    /// ItemButton.cs �� ������ �ִ� ��ư�� ������ ��
    /// ������ �̸�, ����, �̹����� ������
    /// </summary>
    public void OnButtonClick()
    {
        if (gameController != null && itemData != null)
        {
            gameController.ShowExplanation(itemData.itemName, itemData.description, itemData.itemImage);
        }
    }
}
