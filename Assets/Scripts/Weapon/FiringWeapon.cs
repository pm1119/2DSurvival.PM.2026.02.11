using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//대미지(Weapon)
//쿨타임
//속력
//지속 시간

//한 번에 발사하는 총탄 수
//연발 사이 간격 시간


/// <summary>
/// 일정 시간마다 총탄을 발사하는 무기
/// </summary>
public abstract class FiringWeapon : Weapon
{
	[Header("----- 컴포넌트 -----")]
	[SerializeField] TargetSensor _targetSensor;                    //대상 감지기

	[Header("----- 런타임 데이터 -----")]
    [SerializeField] protected float _speed;          //속도
    [SerializeField] protected float _cooltime;       //쿨타임
    [SerializeField] protected float _duration;       //지속 시간
    [SerializeField] protected float _fireDelay;      //총탄 사이 간격 시간

    Coroutine _shootingRoutine;             //공격 코루틴 변수

	public override void Initialize()
    {
        CalculateStats();
        _shootingRoutine = StartCoroutine(ShootingRoutine());
    }

    protected override void CalculateStats()
    {
        WeaponStatData weaponStatData = _weaponData.GetStatData(Level);
		base.CalculateStats();
        _speed = weaponStatData.GetStat(WeaponStatType.Speed);
        _cooltime = weaponStatData.GetStat(WeaponStatType.Cooltime);
        _duration = weaponStatData.GetStat(WeaponStatType.Duration);
        _fireDelay = weaponStatData.GetStat(WeaponStatType.FireDelay);
    }

    /// <summary>
    /// 일정 주기로 발사 루틴을 실행하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator ShootingRoutine()
    {
        while (true)
        {
			StartCoroutine(FireRoutine());
			yield return new WaitForSeconds(_cooltime);
        }
    }

    /// <summary>
    /// 설정된 수만큼 연발로 총탄을 발사하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator FireRoutine()
    {
        for (int i = 0; i < _bulletCount; i++)
        {
            BulletSpawn();
            yield return new WaitForSeconds(_fireDelay);
        }
    }

    protected abstract void BulletSpawn();

    protected Vector3 GetNearestTargetDirection()
    {
		//사정거리 이내 가장 가까운 적 가져오기
		Transform target = _targetSensor.GetNearestTarget(_shootingRange);

		if (target == null)
			return Random.insideUnitCircle.normalized;
		else
		{
			Vector3 dir = (target.position - transform.position).normalized;
			return dir;
		}
	}

    protected Vector3 GetRandomDirection()
    {
        return Util.GetRandomDirection2D();
    }

    //protected virtual void BulletSpawn()
    //{
    //    //투사체 총탄 프리팹 복제 생성
    //    ProjectileBullet projectileBullet = Instantiate(_projectileBulletPrefab);

    //    //총탄 생성 위치 선정
    //    projectileBullet.transform.position = transform.position;

    //    //float rand = Random.Range(0, 360);

    //    //Vector3 dir = new Vector3(
    //    //    Mathf.Cos(rand * Mathf.Deg2Rad),
    //    //    Mathf.Sin(rand * Mathf.Deg2Rad));

    //    //projectileBullet.SetDirection(dir);

    //    //총탄 방향 설정
    //    //Vector3 dir = GetBulletDirection();
    //    projectileBullet.SetDirection(GetBulletDirection());

    //    //총탄 대미지 설정
    //    projectileBullet.SetDamage(Damage);

    //    //총탄 속도 설정
    //    projectileBullet.SetSpeed(_speed);

    //    //총탄 지속시간 설정
    //    projectileBullet.SetDuration(_duration);
    //}
}
 