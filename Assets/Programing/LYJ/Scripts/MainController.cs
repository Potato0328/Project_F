using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    private GameObject mainCanvas;
    private GameObject bookCanvas;
    private GameObject explanationCanvas;
    private GameObject manualCanvas;


    private void Start()
    {
        mainCanvas = GameObject.Find("Main Canvas");
        bookCanvas = GameObject.Find("Book Canvas");
        explanationCanvas = GameObject.Find("Explanation Canvas");
        manualCanvas = GameObject.Find("Manual Canvas");

        if (mainCanvas != null && bookCanvas != null && explanationCanvas != null && manualCanvas != null)
        {
            mainCanvas.SetActive(true);
            bookCanvas.SetActive(false);
            explanationCanvas.SetActive(false);
            manualCanvas.SetActive(false);
        }
    }

    private void Update()
    {
        if (explanationCanvas.activeSelf && Input.GetMouseButtonDown(0))
        {
            Explanation();
        }
    }

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

    public void Explanation()
    {
        explanationCanvas.SetActive(false);
    }

    public void ChangeStage1Scene()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void ClickBookButton()
    {
        mainCanvas.SetActive(false);
        bookCanvas.SetActive(true);
    }

    public void BookExitButton()
    {
        mainCanvas.SetActive(true);
        bookCanvas.SetActive(false);
    }

    public void ClickManualButton()
    {
        mainCanvas.SetActive(false);
        manualCanvas.SetActive(true);
    }

    public void ManualExitButton()
    {
        mainCanvas.SetActive(true);
        manualCanvas.SetActive(false);
    }

    public void ExitButton()
    {
        //����Ƽ �����Ϳ����� Application.Quit();�� ����� �� ���� �׽�Ʈ �뵵�� �Ʒ� �ڵ� ���
        UnityEditor.EditorApplication.isPlaying = false;

        //�����ؼ� ����� ���� �Ʒ� �ڵ带 ���
        //Application.Quit();    
    }
}
