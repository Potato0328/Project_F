using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartItemButton : MonoBehaviour
{
    public StartItemData startItemData;

    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] TextMeshProUGUI itemNameTextMain;
    [SerializeField] TextMeshProUGUI specialEffects;
    [SerializeField] TextMeshProUGUI specialEffectsDescription;
    [SerializeField] Image itemImageMain;
    [SerializeField] Image itemImage;
    [SerializeField] Image itemAttributesImage;

    [SerializeField] Image specialEffectsImage1;
    [SerializeField] Image specialEffectsImage2;
    [SerializeField] Image specialEffectsImage3;

    [SerializeField] TextMeshProUGUI specialEffectsFigure1;
    [SerializeField] TextMeshProUGUI specialEffectsFigure2;
    [SerializeField] TextMeshProUGUI specialEffectsFigure3;

    [SerializeField] GameObject startItemCanvas;
    [SerializeField] GameObject startItemExplanationCanvas;

    [SerializeField] GameObject specialEffect1;
    [SerializeField] GameObject specialEffect2;
    [SerializeField] GameObject specialEffect3;

    [SerializeField] Button button;
    [SerializeField] Button itemNameButton;

    [SerializeField] TextMeshProUGUI hoverTextButton;
    [SerializeField] TextMeshProUGUI hoverTextItemNameButton;

    private PlayerAttack playerAttack;

    private void Start()
    {
        startItemCanvas = GameObject.Find("Start Item Canvas");

        button = gameObject.GetComponent<Button>();
        playerAttack = FindObjectOfType<PlayerAttack>();

        if (startItemCanvas != null && startItemExplanationCanvas != null)
        {
            startItemCanvas.SetActive(true);
            startItemExplanationCanvas.SetActive(false);
        }

        button.onClick.AddListener(OnClickButton);
        itemNameButton.onClick.AddListener(GameStartButton);

        hoverTextButton.gameObject.SetActive(false);
        hoverTextItemNameButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (startItemExplanationCanvas != null && startItemExplanationCanvas.activeSelf && Input.GetMouseButtonDown(0))
        {
            startItemExplanationCanvas.SetActive(false);
        }

        itemNameTextMain.text = startItemData.itemName;
        itemImageMain.sprite = startItemData.itemImage;
    }

    public void OnClickButton()
    {
        SoundManager.Instance.ButtonClickSound();
        if (startItemExplanationCanvas != null)
            startItemExplanationCanvas.SetActive(true);


        itemNameText.text = startItemData.itemName;
        specialEffects.text = startItemData.specialEffects;
        specialEffectsDescription.text = startItemData.specialEffectsDescription;
        itemImage.sprite = startItemData.itemImage;
        itemAttributesImage.sprite = startItemData.itemAttributesImage;
        specialEffectsImage1.sprite = startItemData.specialEffectsImage1;
        specialEffectsFigure1.text = startItemData.specialEffectsFigure1;

        if (startItemData.specialEffectsImage2 != null && startItemData.specialEffectsFigure2 != null)
        {
            specialEffectsImage2.sprite = startItemData.specialEffectsImage2;
            specialEffectsFigure2.text = startItemData.specialEffectsFigure2;
            specialEffect2.gameObject.SetActive(true);
        }
        else
        {
            specialEffect2.gameObject.SetActive(false);
        }

        if (startItemData.specialEffectsImage3 != null && startItemData.specialEffectsFigure3 != null)
        {
            specialEffectsImage3.sprite = startItemData.specialEffectsImage3;
            specialEffectsFigure3.text = startItemData.specialEffectsFigure3;
            specialEffect3.gameObject.SetActive(true);
        }
        else
        {
            specialEffect3.gameObject.SetActive(false);
        }
    }

    public void GameStartButton()
    {
        SoundManager.Instance.ButtonClickSound();
        startItemCanvas.SetActive(false);
        startItemExplanationCanvas.SetActive(false);

        UIManager.Instance.HideUI("Start Item Canvas");

        //선택한 유물이 가지고 있는 스테이터스 수치가 인벤토리에 반영됨
        StatusWindowController.Instance.UpdateStartStatUI(startItemData);

        //선택한 정보 인벤토리에 반영
        StatusWindowController.Instance.AddItemToInventory(startItemData.itemImage, startItemData.itemName, startItemData.specialEffectsDescription, startItemData.elemental);

        //아이템 이름에 따라 총알 타입 변경
        if (startItemData.itemName == "포격의 깨달음")
        {
            playerAttack.SwapBullet(0);
        }
        else if (startItemData.itemName == "흐름의 깨달음")
        {
            playerAttack.SwapBullet(1);
        }
        else if (startItemData.itemName == "발사의 깨달음")
        {
            playerAttack.SwapBullet(2);
        }

        Debug.Log($"{startItemData.itemName}을 선택하셨습니다.");
    }
    public void ShowTextButton()
    {
        hoverTextButton.text = "상세보기";
        hoverTextButton.transform.position = button.transform.position + new Vector3(-100, 50, 0);
        hoverTextButton.gameObject.SetActive(true);
    }

    public void HideTextButton()
    {
        hoverTextButton.gameObject.SetActive(false);
    }

    public void ShowTextItemNameButton()
    {
        hoverTextItemNameButton.text = "선택하기";
        hoverTextItemNameButton.transform.position = itemNameButton.transform.position + new Vector3(-100, 50, 0);
        hoverTextItemNameButton.gameObject.SetActive(true);
    }

    public void HideTextItemNameButton()
    {
        hoverTextItemNameButton.gameObject.SetActive(false);
    }
}
