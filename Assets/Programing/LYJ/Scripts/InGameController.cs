using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameController : MonoBehaviour
{
    private GameObject menuCanvas;
    private GameObject manualCanvas;

    private void Start()
    {
        menuCanvas = GameObject.Find("In Game Menu Canvas");
        manualCanvas = GameObject.Find("In Game Manual Canvas");

        if (menuCanvas != null && manualCanvas != null)
        {
            menuCanvas.SetActive(false);
            manualCanvas.SetActive(false);
        }
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
            Time.timeScale = 0; //���� ����
        }
        else
        {
            Time.timeScale = 1; //���� �簳
        }
    }

    public void ClickReturnGameButton()
    {
        menuCanvas.SetActive(false);
    }

    public void ClickManualButton()
    {
        menuCanvas.SetActive(false);
        manualCanvas.SetActive(true);
    }

    public void ManualExitButton()
    {
        menuCanvas.SetActive(true);
        manualCanvas.SetActive(false);
    }

    public void ClickGiveUpButton()
    {
        SceneManager.LoadScene("GameStart");
    }

    public void ClickLeaveGame()
    {
        //����Ƽ �����Ϳ����� Application.Quit();�� ����� �� ���� �׽�Ʈ �뵵�� �Ʒ� �ڵ� ���
        UnityEditor.EditorApplication.isPlaying = false;

        //�����ؼ� ����� ���� �Ʒ� �ڵ带 ���
        //Application.Quit();  
    }
}
