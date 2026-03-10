using UnityEngine;

namespace OOPStudy
{
	/*
	Character의 기본 좌우 이동 기능에 Rigidbody2D를 사용하여 점프 기능이 추가된 캐릭터 역할
	(Update()에도 virtual, override 적용 가능)
	Character의 기본 좌우 이동 기능을 Rigidbody2D를 사용하는 방식으로 덮어쓰기
	*/

	[RequireComponent(typeof(Rigidbody2D))]
	public class JumpableCharacter : Character
	{
		Rigidbody2D _rigidbody2D;
		[SerializeField] float _jumpPower;

		private void Awake()
		{
			_rigidbody2D = GetComponent<Rigidbody2D>();
		}

		protected override void Update()
		{
			//base.Update();
			HandleMove();
			HandleJump();
		}

		protected override void HandleMove()
		{
			//base.HandleMove();
			MoveHorizontal();
		}

		protected override void MoveHorizontal()
		{
			float x = Input.GetAxis("Horizontal");
			_rigidbody2D.linearVelocityX = x * _horspeed;
		}

		public void HandleJump()
		{
			if (Input.GetButtonDown("Jump") == true)
			{
				if (_rigidbody2D.IsTouchingLayers() == true)
				{
					_rigidbody2D.AddForceY(_jumpPower, ForceMode2D.Impulse);
				}
			}
		}
	}
}
