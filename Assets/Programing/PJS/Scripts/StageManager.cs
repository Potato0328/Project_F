using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public StageState CurState { set { curState = value; } }

    private void Awake()
    {
        createStageMonsters = FindObjectsOfType<CreateStageMonster>();
    }

    private void Start()
    {
        SelectCreateStage();

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
            //�������� ������ ��Ȳ
            else
            {
                yield return null;
            }
        }
    }

    private void Update()
    {
        if(curState == StageState.Clear)
        {
            clearBox.transform.position = inGame.CurPlayerPoint.position;
            curState = StageState.Choice;
        }
        else if (curState == StageState.Choice && clearBox.IsOpen)
        {
            clearBox.transform.position = Vector3.zero;
            potal.transform.position = inGame.CurPlayerPoint.position;
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
        SelectCreateStage();
    }

    private void SelectCreateStage()
    {
        //���������� �� ���� ���� ���� ���������� �ش��� CreateStageMonster ã��
        for (int i = 0; i < createStageMonsters.Length; i++)
        {
            if (createStageMonsters[i].transform.parent.gameObject == inGame.CurStage)
            {
                curStageMonster = createStageMonsters[i];
            }
            else
            {
                createStageMonsters[i].transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
