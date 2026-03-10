using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 게임오브젝트의 Rigidbody2D를 조절해 원하는 방향으로 일정 속력으로 이동하는 역할
/// </summary>
[RequireComponent (typeof(Rigidbody2D))]
public class RigidMover : Mover
{
	Rigidbody2D _rigidbody;

	public override event UnityAction<Vector3> OnMoved;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	public override void Move(Vector3 dir)
	{
		_rigidbody.linearVelocity = dir * _speed;

		OnMoved?.Invoke(_rigidbody.linearVelocity);
	}
}
