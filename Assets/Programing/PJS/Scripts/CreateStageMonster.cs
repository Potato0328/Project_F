using System.Collections.Generic;
using UnityEngine;
using static StageManager;

public class CreateStageMonster : MonoBehaviour
{
    const int ELITECOUNT = 3;
    [SerializeField] MonsterData monsterData;
    [Header("�����Ǵ� ������ �θ� �Ǵ� ������Ʈ")]
    [SerializeField] MonsterManager monsterManager;
    [Header("�����Ǵ� ������ ��ġ")]
    [SerializeField] List<Transform> monsterPoints;

    private void Awake()
    {
        monsterData = GetComponent<MonsterData>();
    }

    /// <summary>
    /// ��ġ�Ǿ� �ִ� ����Ʈ �� �������� �ϳ��� ��� ������ ��ġ�� ���� ����
    /// </summary>
    public void MonsterSpawn(StageState state, int curWave, int fullWave)
    {
        string[] point = RandomList(state, curWave, fullWave).Split(',');

        for (int i = 0; i < point.Length; i++)
        {
            bool isId = int.TryParse(point[i], out int id);
            if (isId)
            {
                Debug.Log($"{i + 1}. {point[i]}");
                for (int j = 0; j < monsterData.MonsterKey.Count; j++)
                {
                    if (monsterData.MonsterKey[j] == id)
                    {
                        Debug.Log(monsterData.Monster[id] + " 1");
                        Debug.Log(monsterPoints[i].position + " 2");
                        Debug.Log(monsterPoints[i].rotation + " 3");
                        GameObject monster = Instantiate(monsterData.Monster[id], monsterPoints[i].position, monsterPoints[i].rotation);

                        if (monster == null)
                            continue;

                        Monster newMonster = monster.GetComponent<Monster>();
                        newMonster.transform.parent = monsterManager.transform;

                        monsterManager.AddMonster(newMonster);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
    }

    private string RandomList(StageState _state, int _curWave, int _fullWave)
    {
        int num;
        int normalMosterCount = monsterData.MonsterList.Count - ELITECOUNT;

        if (_state == StageState.Elite && (_curWave + 1) == _fullWave)
        {
            num = Random.Range(normalMosterCount, monsterData.MonsterList.Count);
        }
        else
        {
            num = Random.Range(0, normalMosterCount);
        }

        if (num <= 0)
        {
            num = 0;
        }

        return monsterData.MonsterList[num];
    }
}
