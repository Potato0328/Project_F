using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    [SerializeField] List<Transform> playerPoints;  //�÷��̾� ���� ��ġ
    [SerializeField] List<GameObject> stages;       //�����Ǵ� ��������

    [SerializeField] GameObject curStage;           //���� ��������
    [SerializeField] GameObject player;             //�÷��̾�
    [SerializeField] GameObject startZone;          //�������� �÷��̾� ��������

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        RandomPoint();
    }

    private void RandomPoint()
    {
        int num = Random.Range(0, stages.Count);
        stages[num].SetActive(true);
        curStage = stages[num];
        //Instantiate(playerPrefab, playerPoints[num].position, Quaternion.identity);
        //Instantiate(startZone, playerPoints[num].position, Quaternion.identity);
        player.transform.position = playerPoints[num].position;
        startZone.transform.position = playerPoints[num].position;
    }
}
