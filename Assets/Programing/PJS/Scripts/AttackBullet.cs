using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBullet : MonoBehaviour
{
    public enum Parent { Player, Monster}   // ������ ����ü���� ���ϱ�
    [SerializeField] Parent curPerant;
    public Parent CurPerant { set { curPerant = value; } }  //get�� �ʿ��ϸ� ����ڰ� �ۼ�
    [SerializeField] Rigidbody rigid;
    [SerializeField] float speed;

    private void Start()
    {
        rigid.velocity = Vector3.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �÷��̾ ��� ���Ͱ� ���� ���
        if(collision.transform.CompareTag("Monster") && curPerant == Parent.Player)
        {
            // ���Ͱ� �ǰݵǴ� ��� �۾�
        }
        // ���Ͱ� ��� �÷��̾ ���� ���
        else if(collision.transform.CompareTag("Player") && curPerant == Parent.Monster)
        {
            // �÷��̾ �ǰݵǴ� ��� �۾�
        }

        // ���� ���� ���
        Destroy(gameObject);
    }
}
