using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class DoT : MonoBehaviour
{
    [SerializeField] int dmg;
    [SerializeField] string naming;
    [SerializeField] float time;
    Coroutine damageCoroutine;
    Dictionary<IDamageable, Coroutine> _damageCoroutine = new Dictionary<IDamageable, Coroutine>();

        
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
        bool valid = other.CompareTag(naming);
        if (!valid)
            return;

        damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            Coroutine coroutine = StartCoroutine(InflictdamageOverTime(damageable));
            _damageCoroutine.Add(damageable, coroutine);
            Debug.Log(coroutine);
            foreach (KeyValuePair<IDamageable, Coroutine> item in _damageCoroutine)
            {
                Debug.Log($"{item.Key} / {item.Value}");
            }
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


        damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            Debug.Log(_damageCoroutine.ContainsKey(damageable));
            Debug.Log(_damageCoroutine[damageable]);
            Coroutine coroutine = _damageCoroutine[damageable];
            StopCoroutine(coroutine);
            _damageCoroutine.Remove(damageable);
            Debug.Log(_damageCoroutine.ContainsKey(damageable));


            Debug.Log("----");
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
        Debug.Log(damageable);
        while (true)
        {
            damageable.TakeHit(dmg);        // ���� ������ �κ�
            Debug.Log(dmg);
            yield return new WaitForSeconds(1f);        // ���ð�
        }
    }


    /// <summary>
    /// Time to DoT to tag name
    /// </summary>
    /// <param name="name">Tag name of the attack target</param>
    /// <param name="attackDamage">attackDamage</param>
    public void Damage(string name, int attackDamage, float time)       // ������ ���� ������ �κ�
    {
        this.naming = name;
        dmg = attackDamage;
        this.time = time;
    }
}
