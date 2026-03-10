using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 캐릭터 HUD UI 담당
/// </summary>
public class CharacterView : MonoBehaviour
{
	[Header("----- 컴포넌트 -----")]
	[SerializeField] Image _hpBar;

	public void UpdateHpBar(float currentHp, float maxHp)
	{
		_hpBar.fillAmount = currentHp / maxHp;
	}
}
