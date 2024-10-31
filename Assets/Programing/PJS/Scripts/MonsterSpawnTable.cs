using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MonsterSpawnTable : MonoBehaviour
{
    //���� ���̺�
    const string monsterData = "https://docs.google.com/spreadsheets/d/1cqURKknVtc4HjHlWKmOfNi0SYTNzrUZoZbPl3gMIWqw/export?gid=0&format=csv";

    //���� ��ġ ���̺�
    const string monsterSpawnData = "https://docs.google.com/spreadsheets/d/1cqURKknVtc4HjHlWKmOfNi0SYTNzrUZoZbPl3gMIWqw/export?gid=1240725374&format=csv";

    private Dictionary<int, GameObject> monster = new Dictionary<int, GameObject>();    // ���͸� ������ ��ųʸ�
    private List<int> monsterKey = new List<int>();         // ����� ���͵��� ID
    private List<string> monsterList = new List<string>();  // ������ ��ġ ����Ʈ

    [SerializeField] StageManager stageManager;
    [Header("�����Ǵ� ������ �θ� �Ǵ� ������Ʈ")]
    [SerializeField] MonsterManager monsterManager;
    [Header("�����Ǵ� ����")]
    [ SerializeField] List<GameObject> monsterPrefabs;
    [Header("�����Ǵ� ������ ��ġ")]
    [SerializeField] List<Transform> monsterPoints;


    private void Awake()
    {
        StartCoroutine(GetDataRoutine());
    }

    IEnumerator GetDataRoutine()
    {
        UnityWebRequest requestMonsterData = UnityWebRequest.Get(monsterData);
        yield return requestMonsterData.SendWebRequest();

        string receiveText = requestMonsterData.downloadHandler.text;
        ParserToMonsterData(receiveText);

        UnityWebRequest requestMonsterSpawn = UnityWebRequest.Get(monsterSpawnData);
        yield return requestMonsterSpawn.SendWebRequest();

        string reciveText = requestMonsterSpawn.downloadHandler.text;
        ParserToMosterSpawnData(reciveText);
        yield break;
    }

    private void ParserToMonsterData(string data)
    {
        string[] line = data.Split('\n');
        for (int i = 1; i < line.Length; i++)
        {
            Debug.Log(line[i]);
            string[] datas = line[i].Split(',');
            int.TryParse(datas[0], out int id);
            Debug.Log(datas[1]);
            if (i != line.Length - 1)
            {
                datas[1] = datas[1].Remove(datas[1].IndexOf('\r'));
            }
            GameObject obj = FindGameObject(datas[1]);

            monster.Add(id, obj);
        }

        foreach (KeyValuePair<int, GameObject> item in monster)
        {
            monsterKey.Add(item.Key);
            Debug.Log($"{item.Key} / {item.Value}");
        }
    }

    private GameObject FindGameObject(string name)
    {
        for (int i = 0; i < monsterPrefabs.Count; i++)
        {
            if (monsterPrefabs[i].name == name)
            {
                Debug.Log(monsterPrefabs[i]);
                return monsterPrefabs[i];
            }
        }

        return null;
    }

    private void ParserToMosterSpawnData(string data)
    {
        string[] line = data.Split('\n');
        for (int i = 1; i < line.Length; i++)
        {
            stageManager.MonsterPlace.Add(line[i]);
        }

        for (int i = 0; i < stageManager.MonsterPlace.Count; i++)
        {
            Debug.Log(monsterList[i]);
        }
    }

    /// <summary>
    /// ��ġ�Ǿ� �ִ� ����Ʈ �� �������� �ϳ��� ��� ������ ��ġ�� ���� ����
    /// </summary>
    public void MonsterSapwn()
    {
        int num = Random.Range(0, stageManager.MonsterPlace.Count - 1);
        Debug.Log(num);
        string[] point = stageManager.MonsterPlace[num].Split(',');

        for (int i = 0; i < point.Length; i++)
        {
            int.TryParse(point[i], out int id);
            for (int j = 0; j < monsterKey.Count; j++)
            {
                if (monsterKey[j] == id)
                {
                    Monster newMonster = Instantiate(monster[id], monsterPoints[i].position, monsterPoints[i].rotation).GetComponent<Monster>();
                    newMonster.transform.parent = monsterManager.transform;
                    monsterManager.AddMonster(newMonster);
                    Debug.Log($"������Ʈ ���� {id}");
                }
                else
                    Debug.Log("�����");
            }
        }
        stageManager.CurWave++;
    }
}
