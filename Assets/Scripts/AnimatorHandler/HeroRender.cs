using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 캐릭터 애니메이터를 다루는 역할
/// </summary>
public class HeroRender : MonoBehaviour
{
	[Header("----- 컴포넌트 -----")]
	[SerializeField] Animator _animator;
	[SerializeField] SpriteRenderer _spriteRenderer;

	public Animator Animator => _animator;

	public void HandleMoved(Vector3 velocity)
	{
		//스프라이트 렌더러 플립
		if (velocity.x > 0)
		{
			_spriteRenderer.flipX = false;
		}
		else if (velocity.x < 0)
		{
			_spriteRenderer.flipX = true;
		}

		//애니메이터 패러미터
		_animator.SetFloat("MoveSpeed", velocity.magnitude);
	}

	public void HandleDead()
	{
		_spriteRenderer.sortingOrder = -1;

		_animator.SetTrigger("OnDead");
	}

	public void HandleHit()
	{
		_animator.SetTrigger("OnHit");
	}

	public void OnCollisionEnter2D(Collision2D collision)
	{
		_animator.SetTrigger("OnAttacked");
	}
}
