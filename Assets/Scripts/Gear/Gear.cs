using UnityEngine;

/// <summary>
/// 장비들의 공통 기능을 담은 추상 클래스
/// </summary>
public abstract class Gear : MonoBehaviour, IUpgradable
{
	[Header("----- 대상 -----")]
	[SerializeField] protected HeroModel _heroModel;

	[Header("----- 설정 데이터 -----")]
	[SerializeField] protected GearData _gearData;

	[Header("----- 스탯 -----")]
	[SerializeField] protected int _level;		//레벨
	[SerializeField] protected float _value;	//레벨에 따른 보너스 수치

	public string UpgradeName => _gearData.GearName;

	public string Desc => _gearData.Desc;

	public Sprite IconSprite => _gearData.IconSprite;

	public int Level => _level;

	public bool IsMaxLevel => _level >= _gearData.MaxLevel;

	public void Upgrade()
	{
		//레벨 1 증가
		_level++;

		//보너스 수치 계산
		CalculateStat();

		//보너스 수치 적용
		Apply();
	}

	/// <summary>
	/// 현재 레벨에 따라 보너스 수치를 계산하는 함수
	/// </summary>
	void CalculateStat()
	{
		_value = _gearData.GetValue(_level);
	}

	/// <summary>
	/// 장비 보너스 수치를 적용하는 함수
	/// </summary>
	protected abstract void Apply();
}
