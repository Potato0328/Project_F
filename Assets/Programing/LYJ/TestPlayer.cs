using System.Collections;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        InGameItemReference itemReference = other.GetComponent<InGameItemReference>();

        if (itemReference != null)
        {
            // �������� ����
            StatusWindowController.Instance.CollectItem(itemReference.item);

            Destroy(other.gameObject);

        }
    }
}
