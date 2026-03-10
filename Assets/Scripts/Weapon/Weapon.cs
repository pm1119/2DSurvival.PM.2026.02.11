using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 여러 종류의 무기들의 공통 기능을 포함하는 추상 클래스
/// </summary>
public abstract class Weapon : MonoBehaviour, IUpgradable
{
	[Header("----- 설정 데이터(공통) -----")]
	[SerializeField] protected WeaponData _weaponData;

	[Header("----- 런타임 데이터(공통) -----")]
	[SerializeField] protected float _damage;			//공격력
	[SerializeField] protected int _level;              //현재 무기 레벨
	[SerializeField] protected float _shootingRange;	//사정거리
	[SerializeField] protected int _bulletCount;		//총탄 개수

	public float Damage => _damage;

	public int Level => _level;

	public string UpgradeName => _weaponData.Name;

	public string Desc => _weaponData.Desc;

	public Sprite IconSprite => _weaponData.IconSprite;

	public bool IsMaxLevel => _level >= _weaponData.MaxLevel;

	public abstract void Initialize();

	protected virtual void CalculateStats()
	{
		_damage = _weaponData.GetStatData(_level).GetStat(WeaponStatType.Damage);
		_shootingRange = _weaponData.GetStatData(_level).GetStat(WeaponStatType.ShootingRange);
		//_bulletCount = (int)_weaponData.GetStatData(Level).GetStat(WeaponStatType.BulletCount);
		//Round = 반올림, Floor = 내림, Ceil = 올림
		_bulletCount = Mathf.CeilToInt(_weaponData.GetStatData(_level).GetStat(WeaponStatType.BulletCount));
	}

	public virtual void Upgrade()
	{
		_level++;

		//최초로 무기를 획득했을 경우
		if (_level == 0)
		{
			Initialize();
		}
		else
		{
			CalculateStats();
		}
	}
}
