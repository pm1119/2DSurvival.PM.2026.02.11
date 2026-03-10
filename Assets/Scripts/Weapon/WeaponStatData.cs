using UnityEngine;

/// <summary>
/// 무기 스탯 종류
/// </summary>
public enum WeaponStatType
{
    Damage,                 //대미지
	ShootingRange,          //사정거리  
	RotSpeed,               //회전 속력
	BulletCount,            //총탄 개수
	Cooltime,               //쿨타임
	Speed,                  //총탄 속력
	Duration,               //총탄 지속 시간
	FireDelay,               //연발 사이 간격
    AttackCount,             //공격 횟수
    DamageDelay,            //지속 대미지 시간 간격
    BulletRange,            //총탄 범위
}

/// <summary>
/// 각 무기 각 레벨의 스탯 데이터
/// </summary>
[System.Serializable]                       //비 MonoBehaviour 객체를 인스펙터뷰에서 수정 가능하게 해준다.
public class WeaponStatData
{
    [SerializeField] float _damage;         //대미지
    [SerializeField] float _shootingRange;  //사정거리  
    [SerializeField] float _rotSpeed;       //회전 속력
    [SerializeField] float _bulletCount;    //총탄 개수
    [SerializeField] float _cooltime;       //쿨타임
    [SerializeField] float _speed;          //총탄 속력
    [SerializeField] float _duration;       //총탄 지속 시간
    [SerializeField] float _fireDelay;      //연발 사이 간격
    [SerializeField] int _attackCount;      //공격 횟수
    [SerializeField] float _damageDelay;    //지속 대미지 시간 간격
    [SerializeField] float _bulletRange;    //총탄 범위

	public float GetStat(WeaponStatType type)
    {
        switch (type)
        {
            case WeaponStatType.Damage:
                return _damage;
            case WeaponStatType.ShootingRange:
                return _shootingRange;
            case WeaponStatType.RotSpeed:
                return _rotSpeed;
            case WeaponStatType.BulletCount:
                return _bulletCount;
            case WeaponStatType.Cooltime:
                return _cooltime;
            case WeaponStatType.Speed:
                return _speed;
            case WeaponStatType.Duration:
                return _duration;
            case WeaponStatType.FireDelay:
                return _fireDelay;
            case WeaponStatType.AttackCount:
                return _attackCount;
            case WeaponStatType.DamageDelay:
                return _damageDelay;
            case WeaponStatType.BulletRange:
                return _bulletRange;
            default:
                return 0;
        }
    }
}
