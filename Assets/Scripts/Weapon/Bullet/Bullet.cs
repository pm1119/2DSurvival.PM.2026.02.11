using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 적 캐릭터에 대미지를 입히는 역할
/// </summary>
public class Bullet : MonoBehaviour
{
	[Header("----- 런타임 데이터 -----")]
	[SerializeField] protected float _damage;
    [SerializeField] protected LayerMask _targetLayerMask;    //타겟으로 판정할 레이어마스크

	/// <summary>
	/// 대미지를 설정하는 함수
	/// </summary>
	/// <param name="damage"></param>
	public void SetDamage(float damage)
	{
		_damage = damage;
	}

	protected virtual void OnTriggerEnter2D(Collider2D collision)
	{
		//상대 게임오브젝트의 Enemy 컴포넌트를 가져온다
		Enemy enemy = collision.GetComponent<Enemy>();

		//Enemy 컴포넌트가 있으면
		if (enemy != null)
		{
			Attack(enemy);
		}
	}

	/// <summary>
	/// 적을 공격하는 함수
	/// </summary>
	/// <param name="enemy">적</param>
	public virtual void Attack(Enemy enemy)
	{
		enemy.TakeHit(_damage);
	}	
}
