using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                child.gameObject.SetActive(false); // 모든 UI 비활성화
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        HideUI("Main Canvas");
        HideUI("In Game Menu Canvas");
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// 특정 UI를 활성화시킬 때 사용
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
            Debug.Log($"{uiName} UI를 찾을 수 없습니다.");
        }
    }

    /// <summary>
    /// 특정 UI를 비활성화 시킬 때 사용
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
            Debug.Log($"{uiName} UI를 찾을 수 없습니다.");
        }
    }

    /// <summary>
    /// 특정 UI가 활성화되어 있는지 확인
    /// </summary>
    /// <param name="uiName"></param>
    /// <returns>활성화 상태</returns>
    public bool IsUIActive(string uiName)
    {
        if (uiCanvases.ContainsKey(uiName))
        {
            return uiCanvases[uiName].activeSelf;
        }
        else
        {
            Debug.Log($"{uiName} UI를 찾을 수 없습니다.");
            return false;
        }
    }

    /// <summary>
    /// UI 캔버스를 가져오는 메서드
    /// </summary>
    /// <param name="uiName"></param>
    /// <returns>찾은 UI 캔버스</returns>
    public GameObject GetUICanvas(string uiName)
    {
        if (uiCanvases.ContainsKey(uiName))
        {
            return uiCanvases[uiName];
        }
        else
        {
            Debug.Log($"{uiName} UI를 찾을 수 없습니다.");
            return null;
        }
    }

    /// <summary>
    /// 모든 UI 비활성화 시킬 때 사용
    /// </summary>
    public void HideAllUI()
    {
        foreach (var uiCanvas in uiCanvases.Values)
        {
            uiCanvas.SetActive(false);
        }
    }
}
