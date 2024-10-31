using System.Collections;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        InGameItemReference itemReference = other.GetComponent<InGameItemReference>();

        if (itemReference != null)
        {
            // 아이템을 수집
            StatusWindowController.Instance.CollectItem(itemReference.item);

            // 오브젝트 삭제
            Destroy(other.gameObject);

        }
    }
}
