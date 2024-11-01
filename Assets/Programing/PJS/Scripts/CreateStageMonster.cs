using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateStageMonster : MonoBehaviour
{
    private MonsterData monsterData;

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
    public void MonsterSpawn()
    {
        int num = RandomNum();
        Debug.Log(num);
        string[] point = monsterData.MonsterList[num].Split(',');

        for (int i = 0; i < point.Length; i++)
        {
            int.TryParse(point[i], out int id);
            for (int j = 0; j < monsterData.MonsterKey.Count; j++)
            {
                if (monsterData.MonsterKey[j] == id)
                {
                    Monster newMonster = Instantiate(monsterData.Monster[id], monsterPoints[i].position, monsterPoints[i].rotation).GetComponent<Monster>();
                    newMonster.transform.parent = monsterManager.transform;
                    monsterManager.AddMonster(newMonster);
                    Debug.Log($"������Ʈ ���� {id}");
                }
            }
        }
    }

    private int RandomNum()
    {
        int num = Random.Range(0, monsterData.MonsterList.Count);

        if (!monsterData.MonsterListActive[num])
        {
            monsterData.MonsterListActive[num] = true;
            return num;
        }
        else
            return RandomNum();
    }
}
