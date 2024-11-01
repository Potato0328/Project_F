using UnityEngine;

//���� ������ ��ũ���ͺ� ������Ʈ�� ������ ��������
/*
 
 private void OnTriggerEnter(Collider other)
    {
        InGameItemReference itemReference = other.GetComponent<InGameItemReference>();

        if (itemReference != null)
        {
            // �������� ����
            StatusWindowController.Instance.CollectItem(itemReference.item);

            Destroy(other.gameObject);

        }
    }
 
 */
//�� �ڵ带 ����Ͽ� ������ ������ �Ծ��� �� �κ��丮�� ���� ��
//����: �ش� �������� Ŭ���Ͽ� ����â�� ���� �ش� ������ ������ �ƴ� CSV ������� ����Ǵ� ���� ����
//���� ��ȹ�� �������� ���� �ش� �ڵ尡 �ʿ������

public enum ElementalType
{
    Flame,
    Ice,
    Electricity,
    Earth
}

[CreateAssetMenu(fileName = "Item_", menuName = "InGameItem")]
public class InGameItemData : ScriptableObject
{
    public Sprite itemImage;
    public int ATK;
    public int ATS;
    public int DEF;
    public int HP;
    public int RAN;
    public int SPD;
    public ElementalType elemental;

    public void ApplyEffect(StatusWindowController statusWindow)
    {
        ApplyStatEffect(statusWindow, "ATK", ATK);
        ApplyStatEffect(statusWindow, "ATS", ATS);
        ApplyStatEffect(statusWindow, "DEF", DEF);
        ApplyStatEffect(statusWindow, "HP", HP);
        ApplyStatEffect(statusWindow, "RAN", RAN);
        ApplyStatEffect(statusWindow, "SPD", SPD);
    }

    private void ApplyStatEffect(StatusWindowController statusWindow, string statName, int value)
    {
        statusWindow.ChangeStat(statName, value);
    }
}
