using UnityEngine;

public class Teleport : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("�ڷ���Ʈ ui ���� ��");
        UIManager.Instance.ShowUI("Next Stage Canvas");
        Debug.Log("�ڷ���Ʈ ui ���� ��");
    }
}
