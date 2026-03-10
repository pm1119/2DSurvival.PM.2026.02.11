using UnityEngine;

namespace OOPStudy
{
	/*
	다형성을 활용하여 수직으로도 이동할 수 있게 하기
	(수직 이동 확인을 위해서는 RigidBody가 없거나, 중력이 없는 게임오브젝트 사용)
	*/
	public class AdvancedCharacter : Character
	{
		[SerializeField] protected float _verSpeed;

		protected override void Update()
		{
			HandleMove();
		}

		protected override void HandleMove()
		{
			base.HandleMove();
			MoveVertical();
		}

		void MoveVertical()
		{
			float y = Input.GetAxis("Vertical");
			transform.Translate(Vector3.up * y * _verSpeed * Time.deltaTime);
		}
	}
}
