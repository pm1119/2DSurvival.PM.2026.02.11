using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 적 캐릭터를 담당하는 역할
/// </summary>
public class Enemy : MonoBehaviour
{
    Hero _hero;             //플레이어 캐릭터
    EnemySpawner _spawner;  //적 생성기

    [Header("----- 컴포넌트 -----")]
	[SerializeField] EnemyModel _enemyModel;
    [SerializeField] CharacterView _enemyView;
	[SerializeField] Mover _mover;           //이동자
    [SerializeField] HeroRender _heroRender; //애니메이터 핸들러
    [SerializeField] Collider2D _collider;
    [SerializeField] Rigidbody2D _rigidbody;

	[Header("----- 런타임 데이터 -----")]
	[SerializeField] float _maxDist;        //타겟으로부터 최대 거리
    [SerializeField] float _minDist;
    [SerializeField] float _cooltime;

    IEnemyState _state;                     //현재 상태 객체(를 가리키는 인터페이스 변수)
    IEnemyState[] _enemyStates;        //상태 객체 배열

    Coroutine _attackRoutine;

    float _timer;                            //타이머

	private void Awake()
	{
        //이동 이벤트 구독
        _mover.OnMoved += _heroRender.HandleMoved;

		//피격 이벤트 구독
		_enemyModel.OnHpChanged += _enemyView.UpdateHpBar;

        //사망 이벤트 구독
        _enemyModel.OnDead += Die;
	}

    public void Initialize(Hero hero, EnemySpawner spawner)
    {
        _hero = hero;
        _spawner = spawner;

        _enemyStates = new IEnemyState[]
        {
            new NormalState(this),
            new AttackState(this),
            new StaggerState(this, 0.3f),
            new DeadState(this, 1.0f)
        };

		//현재 상태를 Normal 상태로 설정
		_state = _enemyStates[(int)EnemyStateType.Normal];
    }

    /// <summary>
    /// 상태 변경 함수
    /// 기존 상태와 새 상태가 동일한 경우에는 실행하지 않음
    /// 기존 상태가 사망 상태일 경우에도 실행하지 않음
    /// </summary>
    /// <param name="stateType"></param>
    public void ChangeState(EnemyStateType stateType)
    {
        if (_state != null)
        {
            //기존 상태가 사망 상태인 경우
            if (_state.StateType == EnemyStateType.Dead) return;

            //기존 상태와 새 상태가 같은 경우
            if (_state.StateType == stateType) return;
            
            //기존 상태 종료 실행
            _state.Exit();
        }

        //현재 상태를 새 상태로 변경
        _state = _enemyStates[(int)stateType];

        //현재 상태 시작 실행
        _state.Enter();
    }

	private void Update()
	{
        if (_state == null) return;

        //현재 상태의 업데이트 함수 호출
        _state.Update();
	}

    IEnumerator AttackRoutine()
    {
        while (true)
        {
            AttackTarget();
            yield return new WaitForSeconds(_cooltime);
        }
    }

    /// <summary>
    /// 목표 방향으로 추적하는 함수
    /// </summary>
    public void Follow()
    {
        //재배치 체크
        float dist = Vector3.Distance(_hero.transform.position, transform.position);
        if (dist > _maxDist)
        {
            transform.position = _spawner.GetSpawnPosition();
        }

        //목표 방향 구하기
        Vector3 dir = (_hero.transform.position - transform.position).normalized;

        //목표 방향으로 이동
        _mover.Move(dir);
    }

    /// <summary>
    /// 이동 정지 함수
    /// </summary>
	public void Stop()
	{
		_mover.Move(Vector3.zero);
	}

	/// <summary>
	/// 피격 함수
	/// </summary>
	/// <param name="damage">대미지</param>
	public void TakeHit(float damage)
    {
        //1)로직 처리
        _enemyModel.TakeDamage(damage);

        if (_enemyModel.IsAlive == false) return;

		//2) 피격 애니메이션 재생
		_heroRender.HandleHit();

        //3) 상테 변경(Stagger)
        ChangeState(EnemyStateType.Stagger);
	}

	/// <summary>
	/// 타겟을 공격하는 함수
	/// </summary>
	public void AttackTarget()
    {
		_heroRender.Animator.SetTrigger("OnAttacked");

        _hero.TakeHit(_enemyModel.Damage);
	}

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _attackRoutine = StartCoroutine(AttackRoutine());
			ChangeState(EnemyStateType.Attack);
		}
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StopCoroutine(_attackRoutine);
            ChangeState(EnemyStateType.Normal);
        }
    }

    /// <summary>
    /// 사망 함수
    /// </summary>
    public void Die()
    {
        //사망 애니메이션 재생
        _heroRender.HandleDead();

        //콜라이더 끄기
        _collider.enabled = false;

        //리지드바디 끄기
        _rigidbody.simulated = false;

        //상태 변경(Dead)
        ChangeState(EnemyStateType.Dead);

        // 플레이어 캐릭터(주인공) 경험치 획득
        _hero.AddExp(_enemyModel.ExpReward);
	}

	/// <summary>
	/// 게임에서 제거하는 함수
	/// </summary>
	public void Remove()
    {
        Destroy(gameObject);
    }
}




































//public class Enemy : MonoBehaviour
//{
//	Hero _hero;             //플레이어 캐릭터
//	EnemySpawner _spawner;  //적 생성기

//	[Header("----- 컴포넌트 -----")]
//	[SerializeField] EnemyModel _enemyModel;
//	[SerializeField] CharacterView _enemyView;
//	[SerializeField] Mover _mover;           //이동자
//	[SerializeField] HeroRender _heroRender; //애니메이터 핸들러
//	[SerializeField] Collider2D _collider;
//	[SerializeField] Rigidbody2D _rigidbody;

//	[Header("----- 런타임 데이터 -----")]
//	[SerializeField] float _maxDist;        //타겟으로부터 최대 거리
//	[SerializeField] float _cooltime;
//	[SerializeField] EnemyStateType _currentState;

//	Coroutine _attackRoutine;

//	float _timer;                            //타이머

//	private void Awake()
//	{
//		//이동 이벤트 구독
//		_mover.OnMoved += _heroRender.HandleMoved;

//		//피격 이벤트 구독
//		_enemyModel.OnHpChanged += _enemyView.UpdateHpBar;

//		//사망 이벤트 구독
//		_enemyModel.OnDead += Die;
//	}

//	public void Initialize(Hero hero, EnemySpawner spawner)
//	{
//		_hero = hero;
//		_spawner = spawner;
//	}

//	private void Update()
//	{
//		switch (_currentState)
//		{
//			case EnemyStateType.Normal:
//				Follow();
//				break;
//			case EnemyStateType.Attack:
//				break;
//			case EnemyStateType.Stagger:
//				_timer -= Time.deltaTime;
//				if (_timer <= 0)
//				{
//					_currentState = EnemyStateType.Normal;
//				}
//				break;
//			case EnemyStateType.Dead:
//				break;
//		}
//	}

//	IEnumerator AttackRoutine()
//	{
//		while (true)
//		{
//			AttackTarget();
//			yield return new WaitForSeconds(_cooltime);
//		}
//	}

//	/// <summary>
//	/// 목표 방향으로 추적하는 함수
//	/// </summary>
//	public void Follow()
//	{
//		//재배치 체크
//		float dist = Vector3.Distance(_hero.transform.position, transform.position);
//		if (dist > _maxDist)
//		{
//			transform.position = _spawner.GetSpawnPosition();
//		}

//		//목표 방향 구하기
//		Vector3 dir = (_hero.transform.position - transform.position).normalized;

//		//목표 방향으로 이동
//		_mover.Move(dir);
//	}

//	/// <summary>
//	/// 이동 정지 함수
//	/// </summary>
//	public void Stop()
//	{
//		_mover.Move(Vector3.zero);
//	}

//	/// <summary>
//	/// 피격 함수
//	/// </summary>
//	/// <param name="damage">대미지</param>
//	public void TakeHit(float damage)
//	{
//		//1)로직 처리
//		_enemyModel.TakeDamage(damage);

//		if (_enemyModel.IsAlive == false) return;

//		//2) 이동 정지
//		Stop();

//		//3) 피격 애니메이션 재생
//		_heroRender.HandleHit();

//		//4) 상태 변화
//		_currentState = EnemyStateType.Stagger;

//		//5) 일정 시간 후 자동으로 일반 상태 전환
//		_timer = 0.3f;
//	}

//	/// <summary>
//	/// 타겟을 공격하는 함수
//	/// </summary>
//	public void AttackTarget()
//	{
//		Stop();

//		_hero.TakeHit(_enemyModel.Damage);

//		_currentState = EnemyStateType.Attack;
//	}

//	private void OnCollisionEnter2D(Collision2D collision)
//	{
//		if (collision.gameObject.tag == "Player")
//		{
//			_attackRoutine = StartCoroutine(AttackRoutine());
//			_heroRender.OnCollisionEnter2D(collision);
//		}
//	}

//	private void OnCollisionExit2D(Collision2D collision)
//	{
//		if (collision.gameObject.tag == "Player")
//		{
//			StopCoroutine(_attackRoutine);
//			_currentState = EnemyStateType.Normal;
//		}
//	}

//	/// <summary>
//	/// 사망 함수
//	/// </summary>
//	public void Die()
//	{
//		//사망 애니메이션 재생
//		_heroRender.HandleDead();

//		//상태 변경
//		_currentState = EnemyStateType.Dead;

//		//콜라이더 끄기
//		_collider.enabled = false;

//		//리지드바디 끄기
//		_rigidbody.simulated = false;

//		//N초 뒤 게임오브젝트 파괴
//		Invoke("Remove", 1.0f);             //Invoke(): 남용하면 안 됨
//	}

//	/// <summary>
//	/// 게임에서 제거하는 함수
//	/// </summary>
//	public void Remove()
//	{
//		Destroy(gameObject);
//	}
//}
