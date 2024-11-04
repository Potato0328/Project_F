using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public enum StageState { Battle, NonBattle, Clear, Choice }
    [SerializeField] StageState curState;
    [SerializeField] int stageNum;

    [Header("���������� ���̺� ��")]
    [SerializeField] List<int> maxWave;
    [Header("���� �Ŵ���")]
    [SerializeField] MonsterManager monsterManager;
    [Header("���� ���� ������Ʈ (�巡�� �� ��� X)")]
    [SerializeField] CreateStageMonster curStageMonster;
    [Header("�������� Ŭ���� ���� ����")]
    [SerializeField] ClearBox clearBox;
    [SerializeField] Teleport potal;
    [SerializeField] InGameManager inGame;

    private int curWave;
    [SerializeField] CreateStageMonster[] createStageMonsters;

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
            if (monsterManager.MonsterCount == 0 && curState == StageState.Battle)
            {
                // �ش� ���������� ��� ���̺긦 ���� ���Ͻ�
                //���̺� Ȯ�� �� ���̺� ���� �� ���� ����
                if (curWave != maxWave[stageNum])
                {
                    curStageMonster.MonsterSpawn();
                    curWave++;
                }
                // �ش� ���������� ��� ���̺긦 �� Ŭ���� ������
                // �������� Ŭ���� ���̺� �ʱ�ȭ Ŭ���� ���� ����
                else
                {
                    //Ŭ���� ���� Ȯ��
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
            clearBox.transform.position = Vector3.zero;
            potal.transform.position = inGame.CurPlayerPoint.position;
        }
        else
        {
            clearBox.IsOpen = false;
        }
    }

    /// <summary>
    /// �������� �̵�
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
            if (createStageMonsters[i].transform.parent.gameObject == inGame.CurStage && curState == StageState.Battle)
            {
                curStageMonster = createStageMonsters[i];
            }
        }
    }
}
