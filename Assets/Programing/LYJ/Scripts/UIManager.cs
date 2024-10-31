using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private Dictionary<string, GameObject> uiCanvases = new Dictionary<string, GameObject>();

    public bool IsAnimationCompleted { get; private set; } = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (Transform child in transform)
        {
            if (child.gameObject.CompareTag("UICanvas"))
            {
                uiCanvases[child.name] = child.gameObject;
                child.gameObject.SetActive(false); // ��� UI ��Ȱ��ȭ
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        HideUI("Main Canvas");
        HideUI("In Game Menu Canvas");
        HideUI("Player HP Canvas");
        HideUI("Player Item Canvas");

        if (scene.name == "Stage1")
        {
            ShowUI("Start Item Canvas");
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// Ư�� UI�� Ȱ��ȭ��ų �� ���
    /// </summary>
    /// <param name="uiName"></param>
    public void ShowUI(string uiName)
    {
        if (uiCanvases.ContainsKey(uiName))
        {
            GameObject canvas = uiCanvases[uiName];
            canvas.SetActive(true);

            //if (uiName == "Status Window Canvas")
            //{
            //    Animator animator = canvas.GetComponent<Animator>();
            //    if (animator != null)
            //    {
            //        animator.SetTrigger("Show");
            //        IsAnimationCompleted = false;
            //    }
            //}
        }
        else
        {
            Debug.Log($"{uiName} UI�� ã�� �� �����ϴ�.");
        }
    }


    /// <summary>
    /// Ư�� UI�� ��Ȱ��ȭ ��ų �� ���
    /// </summary>
    /// <param name="uiName"></param>
    public void HideUI(string uiName)
    {
        if (uiCanvases.ContainsKey(uiName))
        {
            uiCanvases[uiName].SetActive(false);
        }
        else
        {
            Debug.Log($"{uiName} UI�� ã�� �� �����ϴ�.");
        }
    }

    /// <summary>
    /// Ư�� UI�� Ȱ��ȭ�Ǿ� �ִ��� Ȯ��
    /// </summary>
    /// <param name="uiName"></param>
    /// <returns>Ȱ��ȭ ����</returns>
    public bool IsUIActive(string uiName)
    {
        if (uiCanvases.ContainsKey(uiName))
        {
            return uiCanvases[uiName].activeSelf;
        }
        else
        {
            Debug.Log($"{uiName} UI�� ã�� �� �����ϴ�.");
            return false;
        }
    }

    /// <summary>
    /// UI ĵ������ �������� �޼���
    /// </summary>
    /// <param name="uiName"></param>
    /// <returns>ã�� UI ĵ����</returns>
    public GameObject GetUICanvas(string uiName)
    {
        if (uiCanvases.ContainsKey(uiName))
        {
            return uiCanvases[uiName];
        }
        else
        {
            Debug.Log($"{uiName} UI�� ã�� �� �����ϴ�.");
            return null;
        }
    }

    /// <summary>
    /// ��� UI ��Ȱ��ȭ ��ų �� ���
    /// </summary>
    public void HideAllUI()
    {
        foreach (var uiCanvas in uiCanvases.Values)
        {
            uiCanvas.SetActive(false);
        }
    }
}
