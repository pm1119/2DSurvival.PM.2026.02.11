using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 하나의 업그레이드 요소 담당
/// </summary>
public class UpgradeView : MonoBehaviour
{
    [Header("----- 컴포넌트 -----")]
    [SerializeField] Image _icon;           //업그레이드 아이콘 이미지
    [SerializeField] TMP_Text _nameText;    //이름 텍스트
	[SerializeField] TMP_Text _discText;    //설명 텍스트
	[SerializeField] TMP_Text _levelText;   //레벨 텍스트

    /// <summary>
    /// 업그레이드 완료 이벤트
    /// </summary>
    public event UnityAction OnUpgradeCompleted;

    //업그레이드 대상 객체
    IUpgradable _upgradable;

    /// <summary>
    /// 초기화 함수, 업그레이드 정보를 UI에 표시하고 클릭 이벤트 준비
    /// </summary>
    /// <param name="upgradable"></param>
    public void Initialize(IUpgradable upgradable)
    {
        _upgradable = upgradable;

        _icon.sprite = _upgradable.IconSprite;
        _nameText.text = _upgradable.UpgradeName;
        _discText.text = _upgradable.Desc;
        _levelText.text = $"- Level {_upgradable.Level + 2}";
    }

    /// <summary>
    /// 클릭되었을 때를 다루는 함수
    /// </summary>
    public void HandleClicked()
    {
        if (_upgradable != null)
        {
            _upgradable.Upgrade();

            //업그레이드 선택 끝났음 알림
            OnUpgradeCompleted?.Invoke();
        }
    }
}
