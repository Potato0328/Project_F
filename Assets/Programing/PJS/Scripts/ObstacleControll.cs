using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleControll : MonoBehaviour
{
    [Header("��ġ�� ��ֹ� ����Ʈ")]
    [SerializeField] List<GameObject> obstacles;

    private void Start()
    {
        CreateObs();
    }

    private void CreateObs()
    {
        int num = Random.Range(0, obstacles.Count);
        Instantiate(obstacles[num], obstacles[num].transform.position, Quaternion.identity);
    }
}
