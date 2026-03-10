using UnityEngine;

/// <summary>
/// 플레이어 캐릭터 담당(이동, HUD, 무기 홀더, 스탯 등등)
/// </summary>
public class Hero : MonoBehaviour
{
    [Header("----- 컴포넌트 -----")]
	[SerializeField] HeroModel _heroModel;
    [SerializeField] Mover _mover;
	[SerializeField] HeroRender _heroRender;
	[SerializeField] CharacterView _characterView;
	[SerializeField] Upgrader _upgrader;

	public void Awake()
	{
		_mover.OnMoved += _heroRender.HandleMoved;

		_heroModel.OnHpChanged += _characterView.UpdateHpBar;

		_heroModel.OnLevelChanged += _upgrader.HandleLevelUp;
	}

	public void Initialize()
	{
		_heroModel.Initialize();
	}

	/// <summary>
	/// 유저 입력에 따라 이동하는 함수
	/// </summary>
	/// <param name="dir">이동 방향</param>
	public void Move(Vector3 dir)
	{
		_mover.Move(dir);
	}

	public void TakeHit(float damage)
	{
		_heroModel.TakeDamage(damage);
	}

	public void AddExp(float amount)
	{
		_heroModel.AddExp(amount);
	}
}
