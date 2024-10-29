using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainController : MonoBehaviour
{
    private GameObject mainCanvas;
    private GameObject bookCanvas;
    private GameObject explanationCanvas;
    private GameObject manualCanvas;

    private Button newGameButton;
    private Button itemBookButton;
    private Button manualButton;
    private Button bookExitButton;
    private Button manualExitButton;
    private Button exitButton;

    private void Start()
    {
        mainCanvas = GameObject.Find("Main Canvas");
        bookCanvas = GameObject.Find("Book Canvas");
        explanationCanvas = GameObject.Find("Explanation Canvas");
        manualCanvas = GameObject.Find("Manual Canvas");

        newGameButton = GameObject.Find("New Game Button").GetComponent<Button>();
        itemBookButton = GameObject.Find("Item Book Button").GetComponent<Button>();
        manualButton = GameObject.Find("Manual Button").GetComponent<Button>();
        bookExitButton = GameObject.Find("Book Exit Button").GetComponent<Button>();
        manualExitButton = GameObject.Find("Manual Exit Button").GetComponent<Button>();
        exitButton = GameObject.Find("Exit Button").GetComponent<Button>();

        InitializeCanvases();
        InitializeButtons();
    }

    private void InitializeCanvases()
    {
        mainCanvas.SetActive(true);
        bookCanvas.SetActive(false);
        explanationCanvas.SetActive(false);
        manualCanvas.SetActive(false);
    }

    private void InitializeButtons()
    {
        if (newGameButton) newGameButton.onClick.AddListener(ChangeStage1Scene);
        if (itemBookButton) itemBookButton.onClick.AddListener(ClickBookButton);
        if (manualButton) manualButton.onClick.AddListener(ClickManualButton);
        if (bookExitButton) bookExitButton.onClick.AddListener(BookExitButton);
        if (manualExitButton) manualExitButton.onClick.AddListener(ManualExitButton);
        if (exitButton) exitButton.onClick.AddListener(ExitButton);
    }

    private void Update()
    {
        if (explanationCanvas != null && explanationCanvas.activeSelf && Input.GetMouseButtonDown(0))
        {
            Explanation();
        }
    }

    /// <summary>
    /// ����â���� ������ ������ �����
    /// </summary>
    /// <param name="itemName"></param>
    /// <param name="description"></param>
    /// <param name="itemImage"></param>
    public void ShowExplanation(string itemName, string description, Sprite itemImage)
    {
        explanationCanvas.SetActive(true);

        TextMeshProUGUI itemNameText = explanationCanvas.transform.Find("Item Name").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI descriptionText = explanationCanvas.transform.Find("Description").GetComponent<TextMeshProUGUI>();
        Image itemImageComponent = explanationCanvas.transform.Find("Description Image").GetComponent<Image>();

        itemNameText.text = itemName;
        descriptionText.text = description;
        itemImageComponent.sprite = itemImage;
    }

    /// <summary>
    /// ����â �ݱ�
    /// </summary>
    public void Explanation()
    {
        explanationCanvas.SetActive(false);
    }

    /// <summary>
    /// ����ȯ - ��������1
    /// </summary>
    public void ChangeStage1Scene()
    {
        SceneManager.LoadScene("Stage1");
    }

    /// <summary>
    /// ���� ��ư Ŭ����
    /// </summary>
    public void ClickBookButton()
    {
        CurrentCanvasState(mainCanvas, bookCanvas);
    }

    /// <summary>
    /// ���� Exit ��ư Ŭ����
    /// </summary>
    public void BookExitButton()
    {
        CurrentCanvasState(bookCanvas, mainCanvas);
    }

    /// <summary>
    /// Manual ��ư Ŭ����
    /// </summary>
    public void ClickManualButton()
    {
        CurrentCanvasState(mainCanvas, manualCanvas);
    }

    /// <summary>
    /// Manual Exit ��ư Ŭ����
    /// </summary>
    public void ManualExitButton()
    {
        CurrentCanvasState(manualCanvas, mainCanvas);
    }

    //���� ĵ������ ��Ȳ (��Ȱ��ȭ? Ȱ��ȭ?)�� ���ϰ� ���� ����
    private void CurrentCanvasState(GameObject toHide, GameObject toShow)
    {
        toHide.SetActive(false);
        toShow.SetActive(true);
    }

    /// <summary>
    /// Exit ��ư Ŭ����
    /// </summary>
    public void ExitButton()
    {
        //����Ƽ �����Ϳ����� Application.Quit();�� ����� �� ���� �׽�Ʈ �뵵�� �Ʒ� �ڵ� ���
        UnityEditor.EditorApplication.isPlaying = false;

        //�����ؼ� ����� ���� �Ʒ� �ڵ带 ���
        //Application.Quit();    
    }
}
