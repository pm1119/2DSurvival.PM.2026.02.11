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

	[Header("----- 기본 스탯 -----")]
	[SerializeField] float _baseDamage;     //기본 공격력
	[SerializeField] float _baseHp;         //기본 체력
	[SerializeField] float _baseExp;        //기본 보상 경험치

	public float Damage => _damage;

	public float ExpReward => _expReward;

	public void Initialize(WaveData waveData)
	{
		_damage = _baseDamage * waveData.DamageRate;
		_maxHp = _baseHp * waveData.HpRate;
		_expReward = _baseExp * waveData.ExpRate;

		_currentHp = _maxHp;

		InvokeHpChanged();
	}
}
