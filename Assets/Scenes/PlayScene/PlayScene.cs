using UnityEngine;

/// <summary>
/// 플레이 씬 관리자
/// </summary>
public class PlayScene : MonoBehaviour
{
    [Header("----- 컴포넌트 -----")]
    [SerializeField] Hero _hero;                    //플레이어 캐릭터
    [SerializeField] PlayerInputHandler _inputHandler;    //입력 핸들러
	[SerializeField] EnemySpawner _enemySpawner;    //적 생성기
	[SerializeField] StatusView _statusView;

	private void Awake()
	{
		//이동 입력 이벤트 구독
		_inputHandler.OnMoveInput += HandleMoveInput;

		//킬 수 변경 이벤트 구독
		_enemySpawner.OnKillCountChanged += _statusView.UpdateKillCount;

		//시간 변경 이벤트 구독
		_enemySpawner.OnRemainingTimeChanged += _statusView.UpdateRemainingTime;
	}

	private void Start()
	{
		GameManager.Instance.DoSomething();

		//게임매니저 싱글톤 객체의 플레이세팅에서
		//현재 선택된 주인공 캐릭터 데이터(HeroData) 가져오기
		HeroData heroData = GameManager.Instance.PlaySetting.SelectedHero;

		//플레이어 캐릭터 모델 초기화
		_hero.Initialize(heroData);

		//적 생성기 초기화
		_enemySpawner.Initialize();
	}

	/// <summary>
	/// 이동 입력이 들어왔을 때를 다루는 함수
	/// </summary>
	/// <param name="inputVec"></param>
	void HandleMoveInput(Vector2 inputVec)
	{
		_hero.Move(inputVec);
	}
}
