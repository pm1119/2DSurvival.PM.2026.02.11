using UnityEngine;

/// <summary>
/// 처음에 정해진 방향을 향해 일정한 속력으로 날아가 적 캐릭터에 닿으면
/// 대미지를 입히고 파괴되는 역할
/// </summary>
public class ProjectileBullet : Bullet
{
	[Header("----- 런타임 데이터 -----")]
	[SerializeField] protected float _speed;          //이동 속력
	[SerializeField] protected float _duration;       //지속 시간(총탄이 적을 빗겨나가게 될 경우를 대비)
	[SerializeField] protected Vector3 _dir;          //이동 방향

	protected float _timer;					//타이머

	public void SetSpeed(float speed)
	{
		_speed = speed;
	}

	public void SetDuration(float duration)
	{
		_timer = 0;
		_duration = duration;
	}

	/// <summary>
	/// 총탄의 이동 방향을 설정하는 함수
	/// </summary>
	/// <param name="dir"></param>
	public void SetDirection(Vector2 dir)
	{
		//이동 방향 설정
		_dir = dir.normalized;

		//이동 방향에 맞게 회전
		transform.up = _dir;
	}

	protected virtual void FixedUpdate()
	{
		transform.Translate(_dir * _speed * Time.fixedDeltaTime, Space.World);

		_timer += Time.fixedDeltaTime;
		if (_timer > _duration )
		{
			gameObject.DestroyOrReturnPool();
		}
	}

	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		base.OnTriggerEnter2D(collision);
	}

	//public override void Attack(Enemy enemy)
	//{
	//	//적 공격
	//	base.Attack(enemy);

	//	//자신 게임오브젝트 파괴
	//	Destroy(gameObject);
	//}

	//public void MissBullet()
	//{
	//	if (_duration <= 0)
	//	{
	//		Destroy(gameObject);
	//	}
	//}
}
