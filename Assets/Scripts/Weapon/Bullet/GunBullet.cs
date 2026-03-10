using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gun 무기의 총탄
/// Count만큼 적 캐릭터를 공격할 때마다 Count가 감소하고 0이 되면 파괴
/// </summary>
public class GunBullet : ProjectileBullet
{
    [SerializeField] int _count;            //공격 횟수

    public void SetCount(int count)
    {
        _count = count;
    }

	public override void Attack(Enemy enemy)
	{
		base.Attack(enemy);
        _count--;
		if (_count <= 0)
		{
			Destroy(gameObject);
		}
	}
}
