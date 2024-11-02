using System.Collections;
using UnityEngine;

public class DoT : MonoBehaviour
{
    [SerializeField] int lifeTime;
    [SerializeField] int dmg;
    [SerializeField] string name;
    Coroutine damageCoroutine;

    IDamageable damageable;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void OnTriggerEnter(Collider other)
    {
        //bool valid = (layerMask & (1 << other.gameObject.layer)) != 0;
        //if (!valid)
        //    return;
        bool valid = other.CompareTag(name);
        if (!valid)
            return;

        damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            StartDamage();
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // XXX: ���̾��ũ ����� ������
        //bool valid = (layerMask & (1 << other.gameObject.layer)) != 0;
        //if (!valid)
        //    return;
        bool valid = other.CompareTag(name);
        if (!valid)
            return;

        damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            EndDamage();
        }
    }

    private void StartDamage()      //���� ������ �ڷ�ƾ
    {
        damageCoroutine = StartCoroutine(InflictdamageOverTime());

    }

    private void EndDamage()         // ���� ������ �ڷ�ƾ
    {
        StopCoroutine(damageCoroutine);
    }

    private IEnumerator InflictdamageOverTime()
    {
        while (lifeTime > 0)
        {
            damageable.TakeHit(dmg);        // ���� ������ �κ�
            yield return new WaitForSeconds(1f);        // ���ð�

            lifeTime -= 1;
        }
    }


    /// <summary>
    /// Time to DoT to tag name
    /// </summary>
    /// <param name="name">Tag name of the attack target</param>
    /// <param name="attackDamage">attackDamage</param>
    /// <param name="time">duration of attack</param>
    public void Damage(string name, int attackDamage, int time)       // ������ ���� ������ �κ�
    {
        this.name = name;
        dmg = attackDamage;
        lifeTime = time;
    }
}
