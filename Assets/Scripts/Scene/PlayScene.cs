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

	private void Awake()
	{
		//이동 입력 이벤트 구독
		_inputHandler.OnMoveInput += HandleMoveInput;
	}

	private void Start()
	{
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
