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

    [SerializeField] List<string> pointsList = new List<string>();

    private void Awake()
    {
        foreach(string monsterPos in monsterData.MonsterList)
        {
            pointsList.Add(monsterPos);
        }
    }

    /// <summary>
    /// ��ġ�Ǿ� �ִ� ����Ʈ �� �������� �ϳ��� ��� ������ ��ġ�� ���� ����
    /// </summary>
    public void MonsterSpawn(StageState state, int curWave, int fullWave)
    {
        string[] point = RandomList(state, curWave, fullWave);
       
        AddPointList();

        for (int i = 0; i < point.Length; i++)
        {
            int.TryParse(point[i], out int id);
            for (int j = 0; j < monsterData.MonsterKey.Count; j++)
            {
                if (monsterData.MonsterKey[j] == id)
                {
                    Monster newMonster = Instantiate(monsterData.Monster[id], monsterPoints[i].position, monsterPoints[i].rotation).GetComponent<Monster>();

                    if (newMonster == null)
                        continue;

                    newMonster.transform.parent = monsterManager.transform;
                    monsterManager.AddMonster(newMonster);
                }
            }
        }
    }

    private string[] RandomList(StageState _state, int _curWave, int _fullWave)
    {
        int num;
        int normalMosterCount = pointsList.Count - ELITECOUNT;

        if (_state == StageState.Elite && (_curWave + 1) == _fullWave)
        {
            num = Random.Range(normalMosterCount, pointsList.Count);
        }
        else
        {
            num = Random.Range(0, normalMosterCount);
        }

        if (num <= 0)
        {
            num = 0;
        }

        return monsterData.MonsterList[num].Split(',');
    }

    private void AddPointList()
    {
        for (int i = 0; i < monsterData.MonsterList.Count; i++)
        {
            Debug.Log(monsterData.MonsterList[i]);
            pointsList.Add(monsterData.MonsterList[i]);
        }
    }
}
