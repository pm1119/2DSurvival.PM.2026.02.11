using TMPro;
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
    [SerializeField] Image _icon;
    [SerializeField] TMP_Text _nameText;
    [SerializeField] Image _startWeaponIcon;
    [SerializeField] Button _button;

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

        _icon.sprite = _heroData.Sprite;
        _nameText.text = _heroData.Name;
        _startWeaponIcon.sprite = _heroData.StartWeaponData.IconSprite;
    }

    public void HandleClicked()
    {
        Debug.Log($"{_heroData.Name} 캐릭터 선택");

        //캐릭터 선택 이벤트 발행
        OnHeroSelected?.Invoke( _heroData );
    }
}
