using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public enum StageState { Normal, Elite, NonBattle, Clear, Choice, Potal }
    [SerializeField] InGameManager inGame;

    [Header("���� ����, ���� ����")]
    [SerializeField] StageState curState = StageState.Normal;
    [SerializeField] StageState preState;
    
    [Header("���� ��������, ���� ���̺� (0 ���� ����)")]
    [SerializeField] int stageNum;
    [SerializeField] int curWave;

    [Header("���������� ���̺� ��")]
    [SerializeField] List<int> maxWave;
    [Header("���� �Ŵ���")]
    [SerializeField] MonsterManager monsterManager;

    [Header("���� ���� ������Ʈ")]
    [SerializeField] CreateStageMonster[] createStageMonsters;
    [SerializeField] CreateStageMonster curStageMonster;

    [Header("�������� Ŭ����")]
    [SerializeField] ClearBox clearBox;
    [SerializeField] Teleport potal;

    public int StageNum { get { return stageNum; } set { stageNum = value; } }
    public int CurWave { get { return curWave; } set { curWave = value; } }
    public int LastStage { get { return maxWave.Count - 1; } }
    public StageState CurState { set { curState = value; } }

    private void Start()
    {
        SelectStage();

        StartCoroutine(MonsterSpawnRoutine());
    }

    IEnumerator MonsterSpawnRoutine()
    {
        WaitForSeconds delay = new WaitForSeconds(1.5f);

        while (true)
        {
            yield return delay;
            //�������� ���� ��Ȳ
            if (monsterManager.MonsterCount == 0 && (curState == StageState.Normal || curState == StageState.Elite))
            {
                // �ش� ���������� ��� ���̺긦 ���� ���Ͻ�
                //���̺� Ȯ�� �� ���̺� ���� �� ���� ����
                if (curWave != maxWave[stageNum])
                {
                    curStageMonster.MonsterSpawn(curState, curWave + 1, maxWave[stageNum]);
                    curWave++;
                }
                // �ش� ���������� ��� ���̺긦 �� Ŭ���� ������
                // �������� Ŭ���� ���̺� �ʱ�ȭ Ŭ���� ���� ����
                else
                {
                    //Ŭ���� ���� Ȯ��
                    preState = curState;
                    curState = StageState.Clear;
                    curWave = 0;
                }
            }
            else if(curState == StageState.NonBattle)
            {
                potal.transform.position = inGame.Player.transform.position;
                curState = StageState.Choice;
            }
        }
    }

    private void Update()
    {
        if (curState == StageState.Clear)
        {
            clearBox.transform.position = inGame.CurPlayerPoint.position;
            curState = StageState.Choice;
        }
        else if (curState == StageState.Choice && clearBox.IsOpen)
        {
            if(preState == StageState.Elite)
            {
                Debug.Log("2400��� ȹ��");
                GameManager.Instance.AddGold(2400);
            }
            else
            {
                Debug.Log("1500��� ȹ��");
                GameManager.Instance.AddGold(1500);
            }

            curState = StageState.Potal;
            clearBox.transform.position = Vector3.zero;
            potal.transform.position = inGame.CurPlayerPoint.position;
        }
        else
        {
            clearBox.IsOpen = false;
        }
    }

    /// <summary>
    /// ���� �������� �̵�
    /// </summary>
    /// <param name="changeStage">�̵��� ��������</param>
    public void NextStage(StageState changeStage)
    {
        stageNum++;
        curState = changeStage;
        curStageMonster = null;
        potal.transform.position = Vector3.zero;
        SelectStage();
    }

    private void SelectStage()
    {
        //���������� �� ���� ���� ���� ���������� �ش��� CreateStageMonster ã��
        for (int i = 0; i < createStageMonsters.Length; i++)
        {
            if (createStageMonsters[i].transform.parent.gameObject == inGame.CurStage &&
                (curState == StageState.Normal || curState == StageState.Elite))
            {
                curStageMonster = createStageMonsters[i];
            }
        }
    }
}
