using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    // Player ��ũ��Ʈ�� �߰�
    private void OnTriggerEnter(Collider other)
    {
        InGameItemReference itemReference = other.GetComponent<InGameItemReference>();

        if (itemReference != null)
        {
            StatusWindowController.Instance.CollectItem(itemReference.item);
            Destroy(other.gameObject);

            UIManager.Instance.ShowUI("Stage Clear Canvas");
        }
    }

}
