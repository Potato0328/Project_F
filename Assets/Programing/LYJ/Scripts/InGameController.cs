using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameController : MonoBehaviour
{
    private GameObject menuCanvas;
    private GameObject manualCanvas;
    private GameObject gameoverCanvas;

    private Button returnGameButton;
    private Button manualButton;
    private Button giveUpButton;
    private Button leaveGameButton;
    private Button manualExitButton;

    private Button restartButton;
    private Button exitButton;

    private void Start()
    {
        menuCanvas = GameObject.Find("In Game Menu Canvas");
        manualCanvas = GameObject.Find("In Game Manual Canvas");
        gameoverCanvas = GameObject.Find("Game Over Canvas");

        returnGameButton = GameObject.Find("Return Game Button").GetComponent<Button>();
        manualButton = GameObject.Find("Manual Button").GetComponent<Button>();
        giveUpButton = GameObject.Find("Give Up Button").GetComponent<Button>();
        leaveGameButton = GameObject.Find("Leave Game Button").GetComponent<Button>();
        manualExitButton = GameObject.Find("Manual Exit Button").GetComponent<Button>();

        restartButton = GameObject.Find("Restart Button").GetComponent<Button>();
        exitButton = GameObject.Find("Exit Button").GetComponent<Button>();

        menuCanvas.SetActive(false);
        manualCanvas.SetActive(false);
        gameoverCanvas.SetActive(false);

        InitializeButtons();
    }

    private void InitializeButtons()
    {
        if (returnGameButton) returnGameButton.onClick.AddListener(ClickReturnGameButton);
        if (manualButton) manualButton.onClick.AddListener(ClickManualButton);
        if (giveUpButton) giveUpButton.onClick.AddListener(ClickGiveUpButton);
        if (leaveGameButton) leaveGameButton.onClick.AddListener(ClickLeaveGame);
        if (manualExitButton) manualExitButton.onClick.AddListener(ManualExitButton);

        if (restartButton) restartButton.onClick.AddListener(ClickRestartButton);
        if (exitButton) exitButton.onClick.AddListener(ClickExitButton);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!manualCanvas.activeSelf)
            {
                bool isActive = menuCanvas.activeSelf;
                menuCanvas.SetActive(!isActive);
            }
        }

        if (menuCanvas.activeSelf || manualCanvas.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    /// <summary>
    /// Return Game ��ư Ŭ����
    /// </summary>
    public void ClickReturnGameButton()
    {
        menuCanvas.SetActive(false);
    }

    /// <summary>
    /// Manual ��ư Ŭ����
    /// </summary>
    public void ClickManualButton()
    {
        menuCanvas.SetActive(false);
        manualCanvas.SetActive(true);
    }

    /// <summary>
    /// Manual Exit ��ư Ŭ����
    /// </summary>
    public void ManualExitButton()
    {
        menuCanvas.SetActive(true);
        manualCanvas.SetActive(false);
    }

    /// <summary>
    /// Give up ��ư Ŭ����
    /// </summary>
    public void ClickGiveUpButton()
    {
        //���� �κ� �̵�
        SceneManager.LoadScene("GameStart");
    }

    /// <summary>
    /// Leave Game ��ư Ŭ����
    /// </summary>
    public void ClickLeaveGame()
    {
        //����Ƽ �����Ϳ����� Application.Quit();�� ����� �� ���� �׽�Ʈ �뵵�� �Ʒ� �ڵ� ���
        UnityEditor.EditorApplication.isPlaying = false;

        //�����ؼ� ����� ���� �Ʒ� �ڵ带 ���
        //Application.Quit();  
    }

    /// <summary>
    /// Restart ��ư Ŭ����
    /// </summary>
    public void ClickRestartButton()
    {
        SceneManager.LoadScene("Stage1");
    }

    /// <summary>
    /// Exit ��ư Ŭ����
    /// </summary>
    public void ClickExitButton()
    {
        //���� �κ� �̵�
        SceneManager.LoadScene("GameStart");
    }
}
