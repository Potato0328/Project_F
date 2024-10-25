using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    private GameObject mainCanvas;
    private GameObject bookCanvas;

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

    public void Change_Stage1_Scene()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void Click_Book_Button()
    {
        if (mainCanvas != null && bookCanvas != null)
        {
            mainCanvas.SetActive(false);
            bookCanvas.SetActive(true);
        }
    }

    public void Book_Exit_Button()
    {
        if (mainCanvas != null && bookCanvas != null)
        {
            mainCanvas.SetActive(true);
            bookCanvas.SetActive(false);
        }
    }

    public void Exit_Button()
    {
        //����Ƽ �����Ϳ����� Application.Quit();�� ����� �� ���� �׽�Ʈ �뵵�� �Ʒ� �ڵ� ���
        UnityEditor.EditorApplication.isPlaying = false;
        
        //�����ؼ� ����� ���� �Ʒ� �ڵ带 ���
        //Application.Quit();    
    }
}
