using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 캐릭터 HUD UI 담당
/// </summary>
public class CharacterView : MonoBehaviour
{
	[Header("----- 컴포넌트 -----")]
	[SerializeField] Image _hpBar;

	Tween _hpTween;

	float _hpTweenDuration = 1f;

	public void UpdateHpBar(float currentHp, float maxHp)
	{
		float fillAmount = currentHp / maxHp;

		_hpTween = _hpBar
			.DOFillAmount(fillAmount, _hpTweenDuration)
			.SetEase(Ease.OutCubic);
	}
}
