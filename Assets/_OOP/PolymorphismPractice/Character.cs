using UnityEngine;

namespace OOPStudy
{
	public class Character : MonoBehaviour
	{
		//public: 전체 공개
		//private: 내부
		//protected: 자식 클래스까지만 공개
		[SerializeField] protected float _horspeed;        //자식 클래스에서만 접근 가능한 멤버

		protected virtual void Update()
		{
			HandleMove();
		}

		protected virtual void HandleMove()
		{
			MoveHorizontal();
		}

		protected virtual void MoveHorizontal()
		{
			float x = Input.GetAxis("Horizontal");
			transform.Translate(Vector3.right * x * _horspeed * Time.deltaTime);
		}
	}
}