using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 캐릭터 런타임 데이터 담당
/// </summary>
public class CharacterModel : MonoBehaviour
{
	[Header("----- 컴포넌트 -----")]
	[SerializeField] Mover _mover;           //이동자

	[Header("----- 피격 -----")]
	[SerializeField] protected float _maxHp;          //최대 체력
	[SerializeField] protected float _currentHp;      //현재 체력

	public bool IsAlive => _currentHp > 0;

	/// <summary>
	/// 체력 변경 이벤트(현재 체력, 최대 체력)
	/// </summary>
	public event UnityAction<float, float> OnHpChanged;

	/// <summary>
	/// 사망 이벤트
	/// </summary>
	public event UnityAction OnDead;

	public void Initialize()
	{
		_currentHp = _maxHp;
	}

	/// <summary>
	/// 피격 함수
	/// </summary>
	/// <param name="damage">대미지</param>
	public virtual void TakeDamage(float damage)
	{
		if (IsAlive == false) return;

		_currentHp = Mathf.Min(_currentHp - damage, _maxHp);

		//체력 변경 이벤트 발행
		OnHpChanged?.Invoke(_currentHp, _maxHp);

		if (IsAlive == false)
		{
			OnDead?.Invoke();
		}
	}
}
