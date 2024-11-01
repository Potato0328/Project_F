using System.Collections;
using UnityEngine;

public class DoT : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] int lifeTime;
    [SerializeField] public int dmg;
    [SerializeField] public int dmgs;
    Coroutine damageCoroutine;

    IDamageable damageable;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void OnTriggerEnter(Collider other)
    {
        // layermask �� ����ü�� �δ��� ���̾� 
        //bool valid = (layerMask & (1 << other.gameObject.layer)) != 0;
        //if (!valid)
        //    return;
        bool valid = other.CompareTag("Monster");
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
        bool valid = other.CompareTag("Monster");
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
        damageCoroutine = StartCoroutine(InflictdamageOverTime(dmg));

    }

    private void EndDamage()         // ���� ������ �ڷ�ƾ
    {

        StopCoroutine(damageCoroutine);
    }

    private IEnumerator InflictdamageOverTime(int damage)
    {
        while (lifeTime > 0)
        {
            Damage(layerMask, dmg);        // ���� ������ �κ�
            damageable.TakeHit(dmg);
            yield return new WaitForSeconds(1f);        // ���ð�

            lifeTime -= 1;
        }

    }

    public void Damage(int friendlyLayer, int attackDamage)       // ������ ���� ������ �κ�
    {
        rb.position = Vector3.zero;
        layerMask &= ~friendlyLayer;

        dmg = attackDamage;
    }

    private void FIre()
    {

    }
}
