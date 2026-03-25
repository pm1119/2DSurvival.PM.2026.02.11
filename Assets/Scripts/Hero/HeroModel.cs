using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Events;

public class HeroModel : CharacterModel
{
	HeroData _heroData;

	[Header("----- 경험치 -----")]
	[SerializeField] float _currentExp;
	[SerializeField] float _maxExp;
	[SerializeField] float _maxExpUpFactor;
	[SerializeField] int _level;

	[Header("----- 이동 -----")]
	[SerializeField] float _speed;

	/// <summary>
	/// 경험치 변경 이벤트
	/// </summary>
	public event UnityAction<float, float> OnExpChanged;

	/// <summary>
	/// 레벨 변경 이벤트(이전 레벨, 현재 레벨)
	/// </summary>
	public event UnityAction<int, int> OnLevelChanged;

	/// <summary>
	/// 이동 속력 변경 이벤트
	/// </summary>
	public event UnityAction<float> OnSpeedChanged;

	public void Initialize(HeroData heroData)
	{
		_heroData = heroData;

		_maxHp = _heroData.BaseHp;
		_currentHp = _maxHp;
		_speed = _heroData.Speed;
		_maxExp = _heroData.GetExp(_level);

		InvokeHpChanged();
		OnSpeedChanged?.Invoke(_speed);
		OnExpChanged?.Invoke(_currentExp, _maxExp);
		OnLevelChanged?.Invoke(_level, _level);
	}

	/// <summary>
	/// 경험치를 획득하는 함수
	/// </summary>
	/// <param name="amount"></param>
	public void AddExp(float amount)
	{
		//이전 레벨
		int prelevel = _level;

		//경험치 증가
		_currentExp += amount;

		//경험치가 다 찼으면
		while (_currentExp >= _maxExp)
		{
			_currentExp -= _maxExp;

			//레벨업
			_level++;

			//경험치 재계산
			_maxExp = _heroData.GetExp(_level);
		}

		//레벨 변경 이벤트 발행
		//레벨이 올랐을 경우에만 이벤트 발행
		if (_level > prelevel)
			OnLevelChanged?.Invoke(prelevel ,_level);

		//경험치 변경 이벤트 발행
		OnExpChanged?.Invoke(_currentExp, _maxExp);
	}

	/// <summary>
	/// 체력 보너스를 설정하는 함수
	/// </summary>
	/// <param name="amount"></param>
	public void SetHpBonus(float amount)
	{
		//기존 체력 비율 계산
		float ratio = _currentHp / _maxHp;

		_maxHp += _heroData.BaseHp + amount;
		_currentHp = _maxHp * ratio;

		//체력 변경 이벤트 발행
		InvokeHpChanged();
	}

	public override void TakeDamage(float damage)
	{
		base.TakeDamage(damage);
		if (IsAlive == false)
		{
			Time.timeScale = 0;
		}
	}

	/// <summary>
	/// 이동 속력 증가 계수를 설정하는 함수
	/// </summary>
	/// <param name="rate"></param>
	public void SetSpeedMultiplier(float rate)
	{
		_speed = _heroData.Speed * (1 + rate);

		//이동 속력 변경 이벤트 발행
		OnSpeedChanged?.Invoke(_speed);
	}

	/// <summary>
	/// 체력 회복 함수
	/// </summary>
	public void Recover()
	{
		_currentHp = _maxHp;

		InvokeHpChanged();
	}

	public void HpRecover(float amount)
	{
		_currentHp += amount;
		if (_currentHp >= _maxHp)
		{
			_currentHp = _maxHp;
		}
	}
}
