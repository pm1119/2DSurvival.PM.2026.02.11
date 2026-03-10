using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 게임오브젝트를 일정 속력으로 원하는 방향으로 이동하는 역할
/// </summary>
public abstract class Mover : MonoBehaviour
{
	[Header("----- 런타임 데이터 -----")]
	[SerializeField] protected float _speed;          //이동 속력

	/// <summary>
	/// 이동 알림 이벤트(초당 이동 속도)
	/// </summary>
	public abstract event UnityAction<Vector3> OnMoved;

	public float Speed => _speed;

	public abstract void Move(Vector3 dir);

	public void SetSpeed(float speed)
	{
		_speed = speed;
	}
}
