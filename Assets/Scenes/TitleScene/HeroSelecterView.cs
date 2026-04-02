using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 주인공 캐릭터(플레이어 캐릭터) 선택 뷰(UI)
/// </summary>
public class HeroSelecterView : MonoBehaviour
{
    HeroData _heroData;

    [Header("----- 컴포넌트 -----")]
    [SerializeField] Image _image;                          //자신 게임오브젝트의 이미지 컴포넌트
    [SerializeField] Image _icon;
    [SerializeField] TMP_Text _nameText;
    [SerializeField] Image _startWeaponIcon;
    [SerializeField] Button _button;

    [SerializeField] GameObject _lockPanel;             //락 패널
    [SerializeField] Image _lockIcon;                   //락 상태의 이미지
    [SerializeField] TMP_Text _unlockGuideText;         //언락 조건 가이드 텍스트

    public event UnityAction<HeroData> OnHeroSelected;


	private void Awake()
	{
        //버튼 클릭 이벤트 등록
		_button.onClick.AddListener(HandleClicked);

        //무명함수(이름 없는 함수)
        //_button.onClick.AddListener(() => Debug.Log("무명 함수 테스트"));
	}

	public void Initialize(HeroData heroData)
    {
        _heroData = heroData;

        _image.color = _heroData.Color;
        _icon.sprite = _heroData.Sprite;
        _nameText.text = _heroData.Name;
        _startWeaponIcon.sprite = _heroData.StartWeaponData.IconSprite;

        _lockIcon.sprite = _heroData.Sprite;
        _unlockGuideText.text = _heroData.UnlockGuide;
    }

    /// <summary>
    /// 언락 여부를 설정하는 함수
    /// </summary>
    /// <param name="isUnlocked"></param>
    public void SetUnlocked(bool isUnlocked)
    {
        _lockPanel.SetActive(!isUnlocked);
    }

    public void HandleClicked()
    {
        Debug.Log($"{_heroData.Name} 캐릭터 선택");

        //캐릭터 선택 이벤트 발행
        OnHeroSelected?.Invoke( _heroData );
    }
}
