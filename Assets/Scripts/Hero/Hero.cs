using UnityEngine;

/// <summary>
/// 플레이어 캐릭터 담당(이동, HUD, 무기 홀더, 스탯 등등)
/// </summary>
public class Hero : MonoBehaviour
{
	[Header("----- 설정 데이터 -----")]
	[SerializeField] HeroData _data;					//설정 데이터

    [Header("----- 컴포넌트 -----")]
	[SerializeField] HeroModel _heroModel;				//런타임 데이터
    [SerializeField] Mover _mover;						//이동자
	[SerializeField] HeroRender _heroRender;			//애니메이터 핸들러
	[SerializeField] CharacterView _characterView;		//HUD(체력바)
	[SerializeField] Upgrader _upgrader;				//업그레이드 담당
	[SerializeField] StatusView _statusView;			//PlayScene 상태 뷰

	public void Awake()
	{
		//이동 이벤트 구독
		_mover.OnMoved += _heroRender.HandleMoved;

		//체력 변경 이벤트 구독
		_heroModel.OnHpChanged += _characterView.UpdateHpBar;

		//레벨 변경 이벤트 구독
		_heroModel.OnLevelChanged += HandleLevelChanged;

		//이동 속력 변경 이벤트 구독
		_heroModel.OnSpeedChanged += _mover.SetSpeed;

		//경험치 변경 이벤트 구독
		_heroModel.OnExpChanged += _statusView.UpdateExp;
	}

	public void Initialize()
	{
		_heroModel.Initialize(_data);
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

	public void HandleLevelChanged(int prelevel, int level)
	{
		_upgrader.HandleLevelUp(prelevel, level);
		_statusView.UpdateLevel(level);
	}
}
