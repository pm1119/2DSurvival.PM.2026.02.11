using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 총탄을 배치하고 스스로 회전하여 배치된 총탄들로 적을 공격하는 무기
/// </summary>
public class RotatingWeapon : Weapon
{
	[Header("----- 리소스 -----")]
	[SerializeField] Bullet _bulletPrefab;      //총탄 프리팹

	[Header("----- 런타임 데이터 -----")]
	[SerializeField] float _rotSpeed;           //회전 속력

	List<Bullet> _bullets = new (); //생성된 총탄 리스트

	Coroutine _rotateRoutine;                   //회전 코루틴 변수

	//private void Update()
	//{
	//	HandleRotaion();
	//}

	IEnumerator RotateRoutine()
	{
		while (true)
		{
			HandleRotaion();
			yield return null;
		}
	}

	public override void Initialize()
	{
		CalculateStats();
		_rotateRoutine = StartCoroutine(RotateRoutine());
	}

	protected override void CalculateStats()
	{
		base.CalculateStats();
		WeaponStatData weaponStatData = _weaponData.GetStatData(Level);
        _rotSpeed = weaponStatData.GetStat(WeaponStatType.RotSpeed);

		//총탄 생성
		SpawnBullets();
	}

	void HandleRotaion()
	{
		transform.Rotate(0, 0, _rotSpeed * Time.deltaTime);

		//오일러 값을 직접 수정하는 방식
		//Vector3 euler = transform.eulerAngles;
		//euler.z += _rotSpeed * Time.deltaTime;
		//transform.eulerAngles = euler;
	}

	void SpawnBullets()
	{
		//기존 총탄들 제거
		RemoveBullets();

		//자신 게임오브젝트 회전 초기화
		transform.rotation = Quaternion.identity;

		//총탄 사이 각도
		float angle = 360 / _bulletCount;
		for (int i = 0; i < _bulletCount; i++)
		{
			Bullet bullet = Instantiate(_bulletPrefab, transform);

			//원 위의 점 위치는 x축으로부터 몇 도 떨어져있는지에 따라 구할 수 있다
			//x: cos(각도)
			//y: sin(각도)

			Vector3 dir = new (
				Mathf.Cos(i * angle * Mathf.Deg2Rad), 
				Mathf.Sin(i * angle * Mathf.Deg2Rad),
				0);
			bullet.transform.up = dir;
			bullet.transform.localPosition = dir * _shootingRange;

			//총탄 대미지 설정
			bullet.SetDamage(Damage);

			_bullets.Add(bullet);
		}
	}

	/// <summary>
	/// 기존에 생성된 총탄들을 제거하는 함수
	/// </summary>
	void RemoveBullets()
	{
		foreach (var bullet in _bullets)
		{
			//총탄 게임오브젝트 파괴
			Destroy(bullet.gameObject);
		}
		//생성된 총탄 리스트 지우기
		_bullets.Clear();
	}
}
