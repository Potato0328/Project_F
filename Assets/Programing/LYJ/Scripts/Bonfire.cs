using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour, IInteractable
{
    private StoreController storeController;

    private void Start()
    {
        storeController = FindObjectOfType<StoreController>();
    }

    public void Interact()
    {
        if (GameManager.Instance.currentHealth < GameManager.Instance.maxHealth * 0.7f)
        {
            GameManager.Instance.Heal(0.7f);
            Debug.Log("ü���� ȸ���Ǿ����ϴ�.");
        }
    }
}