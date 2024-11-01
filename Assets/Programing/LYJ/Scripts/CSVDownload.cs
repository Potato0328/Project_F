using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CSVDownload : MonoBehaviour
{
    public static CSVDownload Instance { get; private set; }

    const string urlPath = "https://docs.google.com/spreadsheets/d/1DdyytW9508YQYY1_63fVf_bZNvDM7thHC7nBM7M4X6M/export?format=csv";

    public GameObject[] mainButtons;
    public GameObject[] buttons;

    public List<ItemData> itemDataList { get; private set; }

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

        itemDataList = new List<ItemData>(); //�����۵����� ���� ����Ʈ
        StartCoroutine(DownloadRoutine());
    }

    //CSV ���� �ٿ�ε�
    public IEnumerator DownloadRoutine()
    {
        UnityWebRequest request = UnityWebRequest.Get(urlPath);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string receiveText = request.downloadHandler.text;
            List<ItemData> itemDataList = ParseCSV(receiveText); //CSV �����͸� �Ľ��Ͽ� ������ ������ ����Ʈ�� ��ȯ

            SetItemData(itemDataList);
        }
    }

    //CSV ������ �ؽ�Ʈ �����͸� ItemData ����Ʈ�� ��ȯ
    private List<ItemData> ParseCSV(string csvText)
    {
        List<ItemData> itemDataList = new List<ItemData>();
        StringReader reader = new StringReader(csvText);
        bool headerSkipped = false;

        //CSV������ ��� ������ �а�
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();

            //ù��° �� ����� �ǳʶ�
            if (!headerSkipped)
            {
                headerSkipped = true;
                continue;
            }

            //�� ���� ,�� ����� �迭�� ����
            string[] columns = line.Split(',');
            if (columns.Length >= 13)
            {
                ItemData itemData = new ItemData();
                itemData.itemName = columns[0];
                itemData.description = columns[1];
                itemData.price = float.Parse(columns[3]);
                itemData.itemImage = Resources.Load<Sprite>(columns[2]);
                itemData.elemental = int.Parse(columns[12]);

                itemDataList.Add(itemData); //�ʿ��� ������ ��� ������ ������ ������ ����Ʈ�� ������ �߰�
            }
        }
        return itemDataList;
    }

    //������ �����͸� ��ư�� ����
    public void SetItemData(List<ItemData> itemDataList)
    {
        this.itemDataList = itemDataList;

        HashSet<int> usedIndices = new HashSet<int>(); //�̹� ���� �ε����� ����� HashSet ����

        //������� ��ư�� ������ ������ ������ (���� X)
        for (int i = 0; i < mainButtons.Length; i++)
        {
            if (i >= itemDataList.Count) //����Ʈ�� �������� ������ ����
                break;

            ItemData itemData = itemDataList[i];

            //ItemButton ������Ʈ�� ã�� �����͸� ������
            ItemButton itemButton = mainButtons[i].GetComponent<ItemButton>();
            if (itemButton != null)
            {
                itemButton.itemData = itemData;
            }
        }

        //�������� ��ư�� �����͸� ������
        foreach (GameObject buttonObject in buttons)
        {
            if (usedIndices.Count >= itemDataList.Count) //��� �������� ���Ǿ����� ����
                break;

            int randomIndex;

            do //�ߺ����� ���� �ε����� ã�� (�ݺ��ؼ�)
            {
                randomIndex = Random.Range(0, itemDataList.Count);
            } while (usedIndices.Contains(randomIndex));

            usedIndices.Add(randomIndex);

            ItemData itemData = itemDataList[randomIndex];

            //StoreItemButton ������Ʈ�� ã�� ������ ����
            StoreItemButton storeItemButton = buttonObject.GetComponent<StoreItemButton>();
            if (storeItemButton != null)
            {
                storeItemButton.itemData = itemData;
            }

            //Store Canvas�� ItemPanel ��ü�� ã��
            GameObject itemPanel = GameObject.Find($"Store Canvas/Random Store Item/ItemPanel{usedIndices.Count}");
            if (itemPanel == null) //ItemPanel�� ���� ��� �׳� �������� �Ѿ
            {
                continue; //����ϴ� ������ �տ� itemButton������ itemPanel�� ������� ����
            }

            //ItemPanel ���� ��ư�� ã��
            Button itemNameButton = itemPanel.transform.Find("Item Name Button")?.GetComponent<Button>();
            if (itemNameButton != null)
            {
                TextMeshProUGUI itemNameText = itemNameButton.GetComponentInChildren<TextMeshProUGUI>();
                if (itemNameText != null)
                {
                    itemNameText.text = itemData.itemName;
                }
            }

            //ItemPanel ���� �̹����� ã��
            Image itemImageComponent = itemPanel.transform.Find("Item Image")?.GetComponent<Image>();
            if (itemImageComponent != null)
            {
                itemImageComponent.sprite = itemData.itemImage;
            }

            //ItemPanel ���� ������ ã�Ƽ� ����
            TextMeshProUGUI itemGold = itemPanel.transform.Find("Gold")?.GetComponent<TextMeshProUGUI>();
            if (itemGold != null)
            {
                itemGold.text = itemData.price.ToString();
            }
        }
    }


}
