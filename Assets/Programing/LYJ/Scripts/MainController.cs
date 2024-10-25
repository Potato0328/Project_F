using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    private GameObject mainCanvas;
    private GameObject bookCanvas;
    private GameObject explanationCanvas;

    private void Start()
    {
        mainCanvas = GameObject.Find("Main Canvas");
        bookCanvas = GameObject.Find("Book Canvas");

        if (mainCanvas != null && bookCanvas != null)
        {
            mainCanvas.SetActive(true);
            bookCanvas.SetActive(false);
        }
    }

    public void ChangeStage1Scene()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void ClickBookButton()
    {
        if (mainCanvas != null && bookCanvas != null)
        {
            mainCanvas.SetActive(false);
            bookCanvas.SetActive(true);
        }
    }

    public void BookExitButton()
    {
        if (mainCanvas != null && bookCanvas != null)
        {
            mainCanvas.SetActive(true);
            bookCanvas.SetActive(false);
        }
    }

    public void ExitButton()
    {
        //����Ƽ �����Ϳ����� Application.Quit();�� ����� �� ���� �׽�Ʈ �뵵�� �Ʒ� �ڵ� ���
        UnityEditor.EditorApplication.isPlaying = false;
        
        //�����ؼ� ����� ���� �Ʒ� �ڵ带 ���
        //Application.Quit();    
    }
}
