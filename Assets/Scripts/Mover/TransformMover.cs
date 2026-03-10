using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 게임오브젝트를 일정 속력으로 원하는 방향으로 이동하는 역할
/// </summary>
public class TransformMover : Mover
{
	public override event UnityAction<Vector3> OnMoved;

	Vector3 _moveVector;

	public override void Move(Vector3 dir)
	{
		if (dir.magnitude < Util.Epsilon)
		{
			_moveVector = Vector3.zero;
		}
		else
		{
			dir.Normalize();        //방향 벡터 정규화(벡터의 크기를 1로)
			_moveVector = dir * _speed;
		}

		//이동
		transform.Translate(_moveVector * Time.deltaTime);

		//이동 이벤트 발행
		OnMoved?.Invoke(dir * _speed);
	}
}
