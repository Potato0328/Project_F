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
    [SerializeField] private AudioSource stageClearSound;
    [SerializeField] private AudioSource storeOpenSound;
    [SerializeField] private AudioSource teleportSound;
    [SerializeField] private AudioSource clearBoxSound;

    [Header("��ư")]
    [SerializeField] private AudioSource buttonClickSound;

    [Header("����")]
    [SerializeField] private AudioSource bookOpenSound;

    [Header("���� ��������")]
    [SerializeField] private AudioSource bossAppearSound;
    [SerializeField] private AudioSource bossDieSound;
    [SerializeField] private AudioSource bossAttackSound1;
    [SerializeField] private AudioSource bossAttackSound2;
    [SerializeField] private AudioSource bossAttackSound3;
    [SerializeField] private AudioSource bossLandPatternSound1;
    [SerializeField] private AudioSource bossLandPatternSound2;


    [Header("�Ϲ�, ����Ʈ ��������")]
    [Header("���")]
    [SerializeField] private AudioSource goblinDieSound;
    [SerializeField] private AudioSource goblinChaseSound;
    [SerializeField] private AudioSource goblinHitSound;
    [SerializeField] private AudioSource goblinAttackSound;
    [SerializeField] private AudioSource xiamenGoblinAttackSound;

    [Header("��")]
    [SerializeField] private AudioSource golemDieSound;
    [SerializeField] private AudioSource golemHitSound;
    [SerializeField] private AudioSource golemAttackSound;

    [Header("���˸���")]
    [SerializeField] private AudioSource eyeMonsterAttackSound;
    [SerializeField] private AudioSource eyeMonsterDieSound;
    [SerializeField] private AudioSource eyeMonsterHitSound;

    [Header("����������")]
    [SerializeField] private AudioSource frogAttackSound;
    [SerializeField] private AudioSource frogDieSound;
    [SerializeField] private AudioSource frogHitSound;

    [Header("�� �ΰ�")]
    [SerializeField] private AudioSource snakeHumanAttackSound;
    [SerializeField] private AudioSource snakeHumanDieSound;
    [SerializeField] private AudioSource snakeHumanHitSound;




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
    /// �������� Ŭ���� �Ҹ�
    /// </summary>
    public void StageClearSound()
    {
        stageClearSound.PlayOneShot(stageClearSound.clip);
    }

    /// <summary>
    /// ���� ���� �ݴ� �Ҹ�
    /// </summary>
    public void StoreOpenSound()
    {
        storeOpenSound.PlayOneShot(storeOpenSound.clip);
    }

    /// <summary>
    /// ��Ż ���� �Ҹ�
    /// </summary>
    public void TeleportSound()
    {
        teleportSound.PlayOneShot(teleportSound.clip);
    }

    /// <summary>
    /// Ŭ���� �ڽ� ���� �Ҹ�
    /// </summary>
    public void ClearBoxSound()
    {
        clearBoxSound.PlayOneShot(clearBoxSound.clip);
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

    /// <summary>
    /// ���� ù ���� �� ������ �Ҹ�
    /// </summary>
    public void BossAppearSound()
    {
        bossAppearSound.PlayOneShot(bossAppearSound.clip);
    }

    /// <summary>
    /// ���� ���� �� ������ �Ҹ�
    /// </summary>
    public void BossDieSound()
    {
        bossDieSound.PlayOneShot(bossDieSound.clip);
    }

    /// <summary>
    /// ���� �Ϲ� ���� 1
    /// </summary>
    public void BossAttackSound1()
    {
        bossAttackSound1.PlayOneShot(bossAttackSound1.clip);
    }

    /// <summary> 
    /// ���� �Ϲ� ���� 2
    /// </summary>

    public void BossAttackSound2()
    {
        bossAttackSound2.PlayOneShot(bossAttackSound2.clip);
    }

    /// <summary>
    /// ���� �Ϲ� ���� 3
    /// </summary>
    public void BossAttackSound3()
    {
        bossAttackSound3.PlayOneShot(bossAttackSound3.clip);
    }

    /// <summary>
    /// ���� Ư�� ���� 1
    /// </summary>
    public void BossLandPatternSound1()
    {
        bossLandPatternSound1.PlayOneShot(bossLandPatternSound1.clip);
    }

    /// <summary>
    /// ���� Ư�� ���� 2,3
    /// </summary>
    public void BossLandPatternSound2()
    {
        bossLandPatternSound2.PlayOneShot(bossLandPatternSound2.clip);
    }

    /// <summary>
    /// ��� �״� �Ҹ�
    /// </summary>
    public void GoblinDieSound()
    {
        goblinDieSound.PlayOneShot(goblinDieSound.clip);
    }

    /// <summary>
    /// ��� �߰� �Ҹ�
    /// </summary>
    public void GoblinChaseSound()
    {
        goblinChaseSound.PlayOneShot(goblinChaseSound.clip);
    }

    /// <summary>
    /// ��� �ǰ� �Ҹ�
    /// </summary>
    public void GoblinHitSound()
    {
        goblinHitSound.PlayOneShot(goblinHitSound.clip);
    }

    /// <summary>
    /// ��� ���� �Ҹ�
    /// </summary>
    public void GoblinAttackSound()
    {
        goblinAttackSound.PlayOneShot(goblinAttackSound.clip);
    }

    /// <summary>
    /// ���� ��� ���� �Ҹ�
    /// </summary>
    public void XiamenGoblinAttackSound()
    {
        xiamenGoblinAttackSound.PlayOneShot(xiamenGoblinAttackSound.clip);
    }

    /// <summary>
    /// �� �״� �Ҹ�
    /// </summary>
    public void GolemDieSound()
    {
        golemDieSound.PlayOneShot(golemDieSound.clip);
    }

    /// <summary>
    /// �� �ǰ� �Ҹ�
    /// </summary>
    public void GolemHitSound()
    {
        golemHitSound.PlayOneShot(golemHitSound.clip);
    }

    /// <summary>
    /// �� ���� �Ҹ�
    /// </summary>
    public void GolemAttackSound()
    {
        golemAttackSound.PlayOneShot(golemAttackSound.clip);
    }

    /// <summary>
    /// ���� ���� ���� �Ҹ�
    /// </summary>
    public void EyeMonsterAttackSound()
    {
        eyeMonsterAttackSound.PlayOneShot(eyeMonsterAttackSound.clip);
    }

    /// <summary>
    /// ���� ���� �״� �Ҹ�
    /// </summary>
    public void EyeMonsterDieSound()
    {
        eyeMonsterDieSound.PlayOneShot(eyeMonsterDieSound.clip);
    }

    /// <summary>
    /// ���� ���� �ǰ� �Ҹ�
    /// </summary>
    public void EyeMonsterHitSound()
    {
        eyeMonsterHitSound.PlayOneShot(eyeMonsterHitSound.clip);
    }

    /// <summary>
    /// �������� ���� ���� �Ҹ�
    /// </summary>
    public void FrogAttackSound()
    {
        frogAttackSound.PlayOneShot(frogAttackSound.clip);
    }

    /// <summary>
    /// �������� ���� �״� �Ҹ�
    /// </summary>
    public void FrogDieSound()
    {
        frogDieSound.PlayOneShot(frogDieSound.clip);
    }

    /// <summary>
    /// �������� ���� �ǰ� �Ҹ�
    /// </summary>
    public void FrogHitSound()
    {
        frogHitSound.PlayOneShot(frogHitSound.clip);
    }

    /// <summary>
    /// ���ΰ� ���� �Ҹ�
    /// </summary>
    public void SnakeHumanAttackSound()
    {
        snakeHumanAttackSound.PlayOneShot(snakeHumanAttackSound.clip);
    }

    /// <summary>
    /// ���ΰ� �״� �Ҹ�
    /// </summary>
    public void SnakeHumanDieSound()
    {
        snakeHumanDieSound.PlayOneShot(snakeHumanDieSound.clip);
    }

    /// <summary>
    /// ���ΰ� �ǰ� �Ҹ�
    /// </summary>
    public void SnakeHumanHitSound()
    {
        snakeHumanHitSound.PlayOneShot(snakeHumanHitSound.clip);
    }
}
