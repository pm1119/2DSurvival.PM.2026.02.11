using UnityEngine;

/// <summary>
/// 플레이 씬 관리자
/// </summary>
public class PlayScene : MonoBehaviour
{
    [Header("----- 컴포넌트 -----")]
    [SerializeField] Hero _hero;                    //플레이어 캐릭터
    [SerializeField] InputHandler _inputHandler;    //입력 핸들러
	[SerializeField] EnemySpawner _enemySpawner;    //적 생성기
	[SerializeField] StatusView _statusView;

	[Header("----- 인풋 핸들러 -----")]
	[SerializeField] InputHandler _pcInput;
	[SerializeField] InputHandler _mobileInput;

	private void Awake()
	{
		CheckInputHandler();

		//이동 입력 이벤트 구독
		_inputHandler.OnMoveInput += HandleMoveInput;

		//킬 수 변경 이벤트 구독
		_enemySpawner.OnKillCountChanged += _statusView.UpdateKillCount;

		//시간 변경 이벤트 구독
		_enemySpawner.OnRemainingTimeChanged += _statusView.UpdateRemainingTime;
	}

	void CheckInputHandler()
	{
		//현재 구동 중인 환경이 모바일일 경우
		if (Application.isMobilePlatform == true)
		{
			_pcInput.gameObject.SetActive(false);
			_inputHandler = _mobileInput;
		}
		else
		{
			_pcInput.gameObject.SetActive(true);
			_inputHandler = _pcInput;
		}
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

		//스테이지 배경음악 재생
		GameManager.Instance.SoundManager.PlayBgm(Bgm.Stage0);
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
