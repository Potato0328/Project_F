using UnityEngine;
using UnityEngine.Events;

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
        StatusWindowController.ItemChanged += Test;
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

    public void IncreaseDamage(int value)
    {
        Debug.Log("���ݷ� ����");
        dmg += value;
    }

    private void Test(string itemName)
    {
        switch (itemName)
        {
            case "�Ұ��� �г�":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "������� ����":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "������ ��":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "�ı��� ��":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "����ġ�� �Ҳ�":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "���ٱ� �¾�":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "������ �ü�":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "���� ����":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "����ġ�� ����ǳ":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "������ ��":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "���ٴ� ����":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "�����ſ�":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "õ���� ��":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "���� ����":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "���ſ� ����":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "�ö�� ����":
                IncreaseDamage(5);
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                break;
            case "ġ���� ��":
                IncreaseDamage(5);
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                break;
            case "�ð��� �帧":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "������ ��":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "���̾��� �ູ":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "���� ����":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "������ ���":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "ö�� ����":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "������ ��":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
        }
    }
}
