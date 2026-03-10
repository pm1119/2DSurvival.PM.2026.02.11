using System.Collections;
using System.Diagnostics.Contracts;
using UnityEngine;

/// <summary>
/// 일정 시간마다 가까운 적을 향해 GunBullet을 발사하는 무기
/// </summary>
public class GunWeapon : FiringWeapon
{
    [Header("----- 런타임 데이터 -----")]
    [SerializeField] int _count;            //공격 횟수

	[Header("----- 리소스 -----")]
	[SerializeField] GunBullet _gunBulletsPrefab;

    //Coroutine _coroutine;

    //public override void Initialize()
    //{
    //    CalculateStats();
    //    _coroutine = StartCoroutine(FireRoutine());
    //}

    //IEnumerator FireRoutine()
    //{
    //    while (true)
    //    {
    //        BulletSpawn();
    //        yield return new WaitForSeconds(_cooltime);
    //    }
    //}

    protected override void CalculateStats()
    {
		base.CalculateStats();

        //_count 스탯 계산
        WeaponStatData weaponStatData = _weaponData.GetStatData(Level);
        _count = Mathf.CeilToInt(weaponStatData.GetStat(WeaponStatType.AttackCount));
    }

    protected override void BulletSpawn()
	{
		GunBullet gunBullet = Instantiate(_gunBulletsPrefab);

		//총탄 생성 위치 선정
		gunBullet.transform.position = transform.position;

		gunBullet.SetDirection(GetNearestTargetDirection());

		//총탄 대미지 설정
		gunBullet.SetDamage(_damage);

		//총탄 속도 설정
		gunBullet.SetSpeed(_speed);

        //총탄 지속시간 설정
        gunBullet.SetDuration(_duration);

        //총탄 카운트 설정
        gunBullet.SetCount(_count);
	}
}
