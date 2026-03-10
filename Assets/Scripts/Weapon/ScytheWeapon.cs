using Mono.Cecil;
using UnityEngine;

/// <summary>
/// 일정 주기로 회전하는 낫 총탄을 발사하는 무기
/// </summary>
public class ScytheWeapon : FiringWeapon
{
	[Header("----- 리소스 -----")]
	[SerializeField] ScytheBullet _scytheBulletPrefab;

	[Header("----- 런타임 데이터 -----")]
	[SerializeField] float _damageDelay;
	[SerializeField] float _bulletRange;
	[SerializeField] float _rotSpeed;

	protected override void CalculateStats()
	{
		base.CalculateStats();

		WeaponStatData weaponStatData = _weaponData.GetStatData(Level);
		_damageDelay = weaponStatData.GetStat(WeaponStatType.DamageDelay);
		_bulletRange = weaponStatData.GetStat(WeaponStatType.BulletRange);
		_rotSpeed = weaponStatData.GetStat(WeaponStatType.RotSpeed);
	}

	protected override void BulletSpawn()
	{
		ScytheBullet scytheBullet = Instantiate(_scytheBulletPrefab);

		scytheBullet.transform.position = transform.position;

		scytheBullet.SetDamage(_damage);
		scytheBullet.SetDuration(_duration);
		scytheBullet.SetSpeed(_speed);	
		scytheBullet.SetStat(_bulletRange, _rotSpeed, _damageDelay);

		scytheBullet.SetDirection(GetRandomDirection());
	}
}
