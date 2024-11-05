using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEditor.Rendering;
using UnityEngine;

public class DoT : MonoBehaviour
{
    [SerializeField] int dmg;
    [SerializeField] string naming;
    [SerializeField] float speed;
    Coroutine damageCoroutine;
    Dictionary<IDamageable, Coroutine> _damageCoroutine = new Dictionary<IDamageable, Coroutine>();

        
    IDamageable damageable = null;

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
        bool valid = other.CompareTag(naming);
        Debug.Log(valid);
        if (!valid)
            return;

        Transform curTransform = other.transform;
        // Find IDamageable through parents
        while (curTransform != null)
        {
            Debug.Log(curTransform);
            damageable = curTransform.GetComponent<IDamageable>();
            if (damageable != null)
                break;
            curTransform = curTransform.parent;
            Debug.Log(damageable);
        }

        if (damageable != null)
        {
            Coroutine coroutine = StartCoroutine(InflictdamageOverTime(damageable));
            _damageCoroutine.Add(damageable, coroutine);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // XXX: ���̾��ũ ����� ������
        //bool valid = (layerMask & (1 << other.gameObject.layer)) != 0;
        //if (!valid)
        //    return;
        bool valid = other.CompareTag(naming);
        if (!valid)
            return;

        Transform curTransform = other.transform;
        // Find IDamageable through parents
        while (curTransform != null)
        {
            damageable = curTransform.GetComponent<IDamageable>();
            if (damageable != null)
                break;
            curTransform = curTransform.parent;
        }

        if (damageable != null)
        {
            Coroutine coroutine = _damageCoroutine[damageable];
            StopCoroutine(coroutine);
            _damageCoroutine.Remove(damageable);
        }
    }

    private void StartDamage(IDamageable damageable)      //���� ������ �ڷ�ƾ
    {
        damageCoroutine = StartCoroutine(InflictdamageOverTime(damageable));

    }

    private void EndDamage()         // ���� ������ �ڷ�ƾ
    {
        StopCoroutine(damageCoroutine);
    }

    private IEnumerator InflictdamageOverTime(IDamageable damageable)
    {
        while (true)
        {
            damageable.TakeHit(dmg);        // ���� ������ �κ�
            yield return new WaitForSeconds(1f * (1f - (speed / 100)));        // ���ð�
        }
    }


    /// <summary>
    /// Time to DoT to tag name
    /// </summary>
    /// <param name="name">Tag name of the attack target</param>
    /// <param name="attackDamage">attackDamage</param>
    public void Damage(string name, int attackDamage)       // ������ ���� ������ �κ�
    {
        this.naming = name;
        dmg = attackDamage;


    }
}
