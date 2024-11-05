using UnityEngine;
using UnityEngine.Events;
using static AttackBullet;

public class Player : MonoBehaviour, IDamageable
{
    //public UnityAction OnAlarm;

    [Header("Attributes")]
    [SerializeField] int maxHp;
    [SerializeField] public int curHp;
    [SerializeField] public float moveSpeed;
    [SerializeField] public int dmg;
    [SerializeField] public float time;
    [SerializeField] public float attackSpeed;
    [SerializeField] public string naming;



    Animator ani;
    PlayerMover mover;
    PlayerAttack attack;



    private static int idleHash = Animator.StringToHash("Idle03");
    private static int DieHash = Animator.StringToHash("Die");

    public int curAniHash { get; private set; }

    private void Awake()
    {
        ani = GetComponent<Animator>();
        mover = GetComponent<PlayerMover>();
        attack = GetComponent<PlayerAttack>();

        curHp = maxHp;

    }

    private void Start()
    {
        StatusWindowController.ItemChanged += PlayerStatusChange;
    }

    private void Update()
    {
        AnimationPlay();
        State();

    }

    public void TakeHit(int dmg)
    {
        //OnAlarm?.Invoke();

        curHp -= dmg;
        GameManager.Instance.TakeDamage(dmg);
        if (curHp <= 0)
        {
            ani.Play("Die");
            mover.GetComponent<PlayerMover>().enabled = false;
            attack.GetComponent<PlayerAttack>().enabled = false;
        }
        else if (curHp > 0)
        {
            mover.GetComponent<PlayerMover>().enabled = true;
            attack.GetComponent<PlayerAttack>().enabled = true;
        }
    }


    private void AnimationPlay()
    {
        int checkAniHash = 0;

        if (curHp <= 0)
        {
            checkAniHash = DieHash;
            gameObject.GetComponent<PlayerMover>().enabled = false;
            gameObject.GetComponent<PlayerAttack>().enabled = false;
        }
        else if (curHp > 0)
        {
            checkAniHash = idleHash;
            gameObject.GetComponent<PlayerMover>().enabled = true;
            gameObject.GetComponent<PlayerAttack>().enabled = true;
        }

        if (curAniHash != checkAniHash)
        {
            curAniHash = checkAniHash;
            ani.Play(curAniHash);
        }
    }


    private void State()
    {
        attack.attackDmg = dmg;
        mover.addSpeed = moveSpeed;
        attack.speed = attackSpeed;
    }

    /// <summary>
    /// ���ݷ� ����
    /// </summary>
    /// <param name="value"></param>
    public void IncreaseDamage(int value)
    {
        Debug.Log("���ݷ� ����");
        dmg += value;
    }

    /// <summary>
    /// ���ݼӵ� ����
    /// </summary>
    /// <param name="value"></param>
    public void IncreaseAttackSpeed(float percent)
    {
        Debug.Log("���ݼӵ� ����");
        attackSpeed += attackSpeed * (percent / 100);
    }

    /// <summary>
    /// �̵��ӵ� ����
    /// </summary>
    /// <param name="value"></param>
    public void IncreaseMoveSpeed(float percent)
    {
        Debug.Log("�̵��ӵ� ����");
        moveSpeed += moveSpeed * (percent / 100);
    }

    /// <summary>
    /// �����ۿ� ���� ������ �����ϴ� �޼���
    /// </summary>
    /// <param name="itemName">������ �̸�</param>
    /// <param name="atkIncrease">���ݷ�</param>
    /// <param name="atkSpeedIncreasePercent">���ݼӵ�</param>
    /// <param name="moveSpeedIncreasePercent">�̵��ӵ�</param>
    private void ChangeStats(string itemName, int atkIncrease, float atkSpeedIncreasePercent, float moveSpeedIncreasePercent)
    {
        //UI �������ͽ� ������Ʈ
        StatusWindowController.Instance.ChangeStat("ATK", atkIncrease);
        StatusWindowController.Instance.ChangeStat("ATS", (int)atkSpeedIncreasePercent);
        StatusWindowController.Instance.ChangeStat("SPD", (int)moveSpeedIncreasePercent);

        //���� �������ͽ� ����
        IncreaseDamage(atkIncrease);
        IncreaseAttackSpeed(atkSpeedIncreasePercent);
        IncreaseMoveSpeed(moveSpeedIncreasePercent);
    }

    private void PlayerStatusChange(string itemName)
    {
        switch (itemName)
        {
            case "�Ұ��� �г�":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "������� ����":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "������ ��":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "�ı��� ��":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "����ġ�� �Ҳ�":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "���ٱ� �¾�":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "������ �ü�":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "���� ����":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "����ġ�� ����ǳ":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "������ ��":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "���ٴ� ����":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "�����ſ�":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "õ���� ��":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "���� ����":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "���ſ� ����":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "�ö�� ����":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "ġ���� ��":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "�ð��� �帧":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "������ ��":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "���̾��� �ູ":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "���� ����":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "������ ���":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "ö�� ����":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "������ ��":
                ChangeStats(itemName, 10, 15, 5);
                break;
        }
    }
}
