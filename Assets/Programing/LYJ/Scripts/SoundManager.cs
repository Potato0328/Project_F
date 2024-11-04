using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("�����")]
    [SerializeField] private AudioSource startBGM;

    [Header("�÷��̾� ����")]
    [SerializeField] private AudioSource playerWalkSound;
    [SerializeField] private AudioSource playerRunSound;
    [SerializeField] private AudioSource playerJumpSound;
    [SerializeField] private AudioSource playerDashSound;
    [SerializeField] private AudioSource playerHitSound;

    [Header("���� ����")]
    [SerializeField] private AudioSource roundStartSound;
    [SerializeField] private AudioSource coinSound;
    [SerializeField] private AudioSource buyItemSound;
    [SerializeField] private AudioSource inventorySound;

    [Header("��ư")]
    [SerializeField] private AudioSource buttonClickSound;

    [Header("����")]
    [SerializeField] private AudioSource bookOpenSound;



    //���� BGM ��ġ ����
    private float previousTime = 0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetStartBGM()
    {
        PlayBGM(startBGM);
    }

    public void PlayBGM(AudioSource bgm)
    {
        bgm.loop = true;
        bgm.Play();
        previousTime = 0f;
    }

    public void StopBGM()
    {
        previousTime = startBGM.time;
        startBGM.Stop();
    }

    /// <summary>
    /// �÷��̾� �ȴ� �Ҹ�
    /// </summary>
    public void PlayerWalkSound()
    {
        playerWalkSound.PlayOneShot(playerWalkSound.clip);
    }

    /// <summary>
    /// �÷��̾� �޸��� �Ҹ�
    /// </summary>
    public void PlayerRunnSound()
    {
        playerRunSound.PlayOneShot(playerRunSound.clip);
    }

    /// <summary>
    /// �÷��̾� ���� �Ҹ�
    /// </summary>
    public void PlayerJumpSound()
    {
        playerJumpSound.PlayOneShot(playerJumpSound.clip);
    }

    /// <summary>
    /// �÷��̾� �뽬 �Ҹ�
    /// </summary>
    public void PlayerDashSound()
    {
        playerDashSound.PlayOneShot(playerDashSound.clip);
    }

    /// <summary>
    /// �÷��̾� �ǰ� �Ҹ�
    /// </summary>
    public void PlayerHitSound()
    {
        playerHitSound.PlayOneShot(playerHitSound.clip);
    }

    /// <summary>
    /// ���� ���� �Ҹ�
    /// </summary>
    public void RoundStartSound()
    {
        roundStartSound.PlayOneShot(roundStartSound.clip);
    }

    /// <summary>
    /// ���� ȹ�� �Ҹ�
    /// </summary>
    public void CoinSound()
    {
        coinSound.PlayOneShot(coinSound.clip);
    }

    /// <summary>
    /// ������ ���� �Ҹ� - �������� �̿�
    /// </summary>
    public void BuyItemSound()
    {
        buyItemSound.PlayOneShot(buyItemSound.clip);
    }

    /// <summary>
    /// �κ��丮 ���� �ݴ� �Ҹ�
    /// </summary>
    public void InventorySound()
    {
        inventorySound.PlayOneShot(inventorySound.clip);
    }

    /// <summary>
    /// ��ư Ŭ�� �Ҹ�
    /// </summary>
    public void ButtonClickSound()
    {
        buttonClickSound.PlayOneShot(buttonClickSound.clip);
    }

    /// <summary>
    /// ���� ��ġ�� �Ҹ�
    /// </summary>
    public void BookOpenSound()
    {
        bookOpenSound.PlayOneShot(bookOpenSound.clip);
    }
}
