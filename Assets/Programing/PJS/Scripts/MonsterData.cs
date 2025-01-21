using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MonsterData : MonoBehaviour
{
    //���� ���̺�
    const string monsterData = "https://docs.google.com/spreadsheets/d/1vNCS05iqUkSMKadTZCZYP3mbEUtViNJuxROduaBiYk0/export?gid=0&format=csv";

    //���� ��ġ ���̺�
    const string monsterSpawnData = "https://docs.google.com/spreadsheets/d/1vNCS05iqUkSMKadTZCZYP3mbEUtViNJuxROduaBiYk0/export?gid=1049510758&format=csv";

    private Dictionary<int, GameObject> monster = new Dictionary<int, GameObject>();    // ���͸� ������ ��ųʸ�
    private List<int> monsterKey = new List<int>();         // ����� ���͵��� ID
    [SerializeField] List<string> monsterList;              // ������ ��ġ ����Ʈ
    private bool[] monsterListActive;

    public Dictionary<int, GameObject> Monster { get { return monster; } }
    public List<int> MonsterKey { get { return monsterKey; } }
    public List<string> MonsterList { get { return monsterList; } }

    [Header("�����Ǵ� ����")]
    [SerializeField] List<GameObject> monsterPrefabs;

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
            string[] datas = line[i].Split(',');
            int.TryParse(datas[0], out int id);
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
        }
    }

    private GameObject FindGameObject(string name)
    {
        for (int i = 0; i < monsterPrefabs.Count; i++)
        {
            if (monsterPrefabs[i].name == name)
            {
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
            monsterList.Add(line[i]);
        }
    }


}
