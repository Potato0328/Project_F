using UnityEngine;

public class MainMenuInitializer : MonoBehaviour
{
    //���θ޴� UI�� ó�������� �����ؾ���
    //UI ��ü�� �̱������� �صּ� �� ��ũ��Ʈ�� ������ ���� ĵ������ ��� �����ϰԵ�
    //Main Menu Manager
    private void Start()
    {
        if (!UIManager.Instance.IsUIActive("Main Canvas"))
        {
            UIManager.Instance.ShowUI("Main Canvas");
        }
    }
}
