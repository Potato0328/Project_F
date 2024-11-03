using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    [Header("�÷��̾� ���� ��ġ")]
    [SerializeField] List<Transform> playerPoints;  //�÷��̾� ���� ��ġ
    [Header("���� ��������")]
    [SerializeField] List<GameObject> stages;       //�����Ǵ� ��������

    [Header("���� �����ϴ� �÷��̾�, ���� ��")]
    [SerializeField] GameObject player;             //�÷��̾�
    [SerializeField] GameObject startZone;          //�������� �÷��̾� ��������

    private int stageNum;
    public int StageNum { get { return stageNum; } }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        RandomPoint();
    }

    private void Start()
    {
        for (int i = 0; i < stages.Count; i++)
        {
            if (i != stageNum)
                stages[i].SetActive(false);
        }
    }

    private void RandomPoint()
    {
        stageNum = Random.Range(0, stages.Count);
        
        StageMovePosition(player.transform, startZone.transform, stageNum);
    }

    private void StageMovePosition(Transform player, Transform lifeZone, int num)
    {
        player.position = playerPoints[num].position;
        lifeZone.position = playerPoints[num].position;
    }
}
