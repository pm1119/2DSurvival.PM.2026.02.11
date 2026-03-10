using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 적 캐릭터의 런타임 데이터 로직
/// </summary>
public class EnemyModel : CharacterModel
{
	[Header("----- 공격 -----")]
	[SerializeField] float _damage;

	[Header("----- 보상 -----")]
	[SerializeField] float _expReward;

	public float Damage => _damage;

	public float ExpReward => _expReward;
}
