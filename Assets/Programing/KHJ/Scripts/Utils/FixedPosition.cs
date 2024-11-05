using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedPosition : MonoBehaviour
{
    public Transform origin;

    private void LateUpdate()
    {
        if (origin != null)
        {
            // referenceTransform�� ��ġ�� ȸ���� �״�� �����մϴ�.
            transform.position = origin.position;
            transform.rotation = origin.rotation;
        }
    }
}
