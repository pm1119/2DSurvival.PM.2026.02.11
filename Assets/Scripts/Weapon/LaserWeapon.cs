using System.Collections;
using UnityEngine;

public class LaserWeapon : FiringWeapon
{
    [Header("----- 프리팹 -----")]
    [SerializeField] LaserBullet _laserBulletPrefab;

	[SerializeField] float _rotSpeed;           //회전 속력

	Coroutine _rotateRoutine;                   //회전 코루틴 변수

	Pool _laserPool;

	public override void Initialize()
	{
		_laserPool =
			new Pool(_laserBulletPrefab.gameObject, transform, 5);
		base.Initialize();
		GetStatData();
		_rotateRoutine = StartCoroutine(RotateRoutine());
	}

	public void GetStatData()
	{
		WeaponStatData weaponStatData = _weaponData.GetStatData(Level);
		_rotSpeed = weaponStatData.GetStat(WeaponStatType.RotSpeed);
		_shootingRange = weaponStatData.GetStat(WeaponStatType.ShootingRange);
	}

	IEnumerator RotateRoutine()
	{
		while (true)
		{
			HandleRotaion();
			yield return null;
		}
	}

	void HandleRotaion()
	{
		transform.Rotate(0, 0, _rotSpeed * Time.deltaTime);

		//오일러 값을 직접 수정하는 방식
		//Vector3 euler = transform.eulerAngles;
		//euler.z += _rotSpeed * Time.deltaTime;
		//transform.eulerAngles = euler;
	}

	protected override void BulletSpawn()
	{
		GameObject go = _laserPool.Pop();

		go.transform.localPosition = Vector3.zero;
		go.transform.localRotation = Quaternion.identity;

		LaserBullet laserBullet = go.GetComponent<LaserBullet>();

		laserBullet.SetDamage(_damage);

		laserBullet.SetDuration(_duration);

		laserBullet.SetRange(_shootingRange);
	}
}
