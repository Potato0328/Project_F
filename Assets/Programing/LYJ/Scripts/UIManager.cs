using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private Dictionary<string, GameObject> uiCanvases = new Dictionary<string, GameObject>();

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
    }

    private void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.CompareTag("UICanvas"))
            {
                uiCanvases[child.name] = child.gameObject;
                child.gameObject.SetActive(false); //��� UI ��Ȱ��ȭ
            }
        }
    }

    /// <summary>
    /// Ư�� UI�� Ȱ��ȭ��ų �� ���
    /// </summary>
    /// <param name="uiName"></param>
    public void ShowUI(string uiName)
    {
        if (uiCanvases.ContainsKey(uiName))
        {
            uiCanvases[uiName].SetActive(true);
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
