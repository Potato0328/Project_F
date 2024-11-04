using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    [Header("�÷��̾� ���� ��ġ")]
    [SerializeField] List<Transform> playerPoints;  //�÷��̾� ���� ��ġ
    [Header("����")]
    [SerializeField] Transform store;
    [Header("��ں�")]
    [SerializeField] Transform bonfire;
    [Header("���� ��������")]
    [SerializeField] List<GameObject> stages;       //�����Ǵ� ��������

    [Header("���� �����ϴ� �÷��̾�, ���� ��")]
    [SerializeField] GameObject player;             //�÷��̾�
    [SerializeField] GameObject startZone;          //�������� �÷��̾� ��������

    [SerializeField] int stageNum;

    public Transform CurPlayerPoint { get { return playerPoints[stageNum]; } }
    public GameObject CurStage { get { return stages[StageNum]; } }
    public int StageNum { get { return stageNum; } }
    public GameObject Player { get { return player; } }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        RandomStagePoint();
    }

    public void RandomStagePoint()
    {
        stageNum = Random.Range(0, stages.Count);
        
        StageMovePosition(player.transform, startZone.transform, stageNum);
    }

    private void StageMovePosition(Transform player, Transform lifeZone, int num)
    {
        player.position = playerPoints[num].position;
        lifeZone.position = playerPoints[num].position;
        lifeZone.GetComponent<SphereCollider>().enabled = true;
    }

    public void StoreOrBonfirePosition(Transform player, bool choice)
    {
        if (choice)
            player.position = store.position;
        else
            player.position = bonfire.position;
    }
}
