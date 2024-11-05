using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    [Header("�÷��̾� ���� ��ġ(�������� ������������)")]
    [SerializeField] List<Transform> playerPoints;  //�÷��̾� ���� ��ġ
    [Header("����, ��ں� ��������")]
    [SerializeField] Transform store;               //����
    [SerializeField] Transform bonfire;             //��ں�
    [Header("���� ��������")]
    [SerializeField] List<GameObject> stages;       //�����Ǵ� ��������

    [Header("���� �����ϴ� �÷��̾�, ���� ��������, ���� �巡��")]
    [SerializeField] GameObject player;             //�÷��̾�
    [SerializeField] GameObject startZone;          //�������� �÷��̾� ��������
    [SerializeField] GameObject boss;               //���� �巡��

    private int stageNum;

    public Transform CurPlayerPoint { get { return playerPoints[stageNum]; } }
    public GameObject CurStage { get { return stages[StageNum]; } }
    public int StageNum { get { return stageNum; } }
    public GameObject Player { get { return player; } }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        boss.SetActive(false);
        RandomStagePoint();
    }

    /// <summary>
    /// ������ ���� �������� �̵�
    /// </summary>
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

    /// <summary>
    /// ���� �Ǵ� ��ں� �������� �̵�
    /// </summary>
    /// <param name="player">�÷��̾�</param>
    /// <param name="choice">����or��ں�</param>
    public void StoreOrBonfirePosition(Transform player, bool choice)
    {
        if (choice)
        {
            player.position = store.position;
            SoundManager.Instance.StoreOpenSound();
        }
        else
        {
            player.position = bonfire.position;
        }
    }

    /// <summary>
    /// ���� �������� �̵�
    /// </summary>
    /// <param name="player">�ÿ��̾�</param>
    public void BossStagePosition(Transform player)
    {
        player.position = playerPoints[playerPoints.Count - 1].position;
        boss.SetActive(true);
    }
}
