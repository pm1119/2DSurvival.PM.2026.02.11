using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 타겟의 위치에 따라 배경 게임오브젝트를 반복적으로 재배치하는 클래스
/// </summary>
public class GroundReposition : MonoBehaviour
{
    [SerializeField] Transform _target;     //재배치 기준이 되는 대상의 트랜스폼
    [SerializeField] float _senseDist;      //재배치 감도 거리(이 거리보다 타겟이 멀면 재배치)
    [SerializeField] float _reposDist;      //재배치 시 이동 거리

	private void Update()
	{
		if (_target == null) return;

		//1) 거리 차이 계산
		float distX = _target.position.x - transform.position.x;

		//2) 감도 거리보다 크면
		if (Mathf.Abs(distX) > _senseDist)
		{
			//3) 재배치
			//Mathf.Sign(): 부호 함수
			//+면 1 반환
			//-면 -1 반환
			transform.position += Vector3.right * _reposDist * Mathf.Sign(distX);
		}

		//1) 거리 차이 계산
		float distY = _target.position.y - transform.position.y;

		//2) 감도 거리보다 크면
		if (Mathf.Abs(distY) > _senseDist)
		{
			//3) 재배치
			//Mathf.Sign(): 부호 함수
			//+면 1 반환
			//-면 -1 반환
			transform.position += Vector3.up * _reposDist * Mathf.Sign(distY);
		}
	}
}
