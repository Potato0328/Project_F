using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public enum StageState { Battle, NonBattle, Clear }
    [SerializeField] StageState curState;
    [SerializeField] int stageNum;

    [Header("���������� ���̺� ��")]
    [SerializeField] List<int> maxWave;
    [Header("���� �Ŵ���")]
    [SerializeField] MonsterManager monsterManager;
    [Header("���� ���� ������Ʈ")]
    [SerializeField] CreateStageMonster curStageMonster;
    [Header("�������� Ŭ���� ���� ����")]
    [SerializeField] GameObject clearBox;
    [SerializeField] Transform createPoint;
    [SerializeField] InGameManager inGame;


    private int curWave;
    [SerializeField] CreateStageMonster[] createStageMonsters;

    public int StageNum { get { return stageNum; } set { stageNum = value; } }
    public int CurWave { get { return curWave; } set { curWave = value; } }

    private void Awake()
    {
        createStageMonsters = FindObjectsOfType<CreateStageMonster>();
    }

    private void Start()
    {
        for (int i = 0; i < createStageMonsters.Length; i++)
        {
            Debug.Log(createStageMonsters[i].transform.parent + " �θ�");
            if (createStageMonsters[i].transform.parent.gameObject.activeSelf)
            {
                curStageMonster = createStageMonsters[i];
            }
        }
        StartCoroutine(MonsterSpawnRoutine());
    }

    IEnumerator MonsterSpawnRoutine()
    {
        while (curState != StageState.NonBattle)
        {
            yield return new WaitForSeconds(1.5f);
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

            if (curState == StageState.Clear)
            {
                Instantiate(clearBox, createPoint.position, Quaternion.identity);
                yield break;
            }
        }
    }
}
