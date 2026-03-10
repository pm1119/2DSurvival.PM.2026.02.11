using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

//유한 상태 머신

//상태 패턴(State Pattern)
//한 대상의 상태를 개별 클래스로 설계하는 방식

//-> 이 프로젝트에서는 Normal, Stagger, Dead 각각의 상태를 하나의 클래스로 설계한다

/// <summary>
/// 적 상태 종류
/// </summary>
public enum EnemyStateType
{
    Normal,     //일반
    Attack,     //공격
    Stagger,    //경직
    Dead,       //사망

	Count		//상태의 개수
}

/// <summary>
/// 적 상태 인터페이스
/// </summary>
public interface IEnemyState
{ 
    /// <summary>
    /// 상태 종류
    /// </summary>
    EnemyStateType StateType { get; }

    /// <summary>
    /// 상태 시작
    /// </summary>
    void Enter();

    /// <summary>
    /// 상태 유지
    /// </summary>
    void Update();

    /// <summary>
    /// 상태 종료
    /// </summary>
    void Exit();
}

/// <summary>
/// 일반 상태 - 타겟을 추적
/// </summary>
public class NormalState : IEnemyState
{
	public EnemyStateType StateType => EnemyStateType.Normal;

    Enemy _enemy;                //상태 객체가 명령을 내릴 대상

    public NormalState(Enemy enemy)
    {
        _enemy = enemy;
    }

	public void Enter()
	{
        //Normal 상태가 시작할 때 실행할 동작
        _enemy.Follow();
	}

	public void Update()
	{
		//Normal 상태가 유지될 때 지속적으로 실행할 동작
        //타겟 추적
        _enemy.Follow();
	}

	public void Exit()
	{
		//Normal 상태가 종료돨 때 실행할 동작
        _enemy.Stop();
	}
}

public class AttackState : IEnemyState
{
	public EnemyStateType StateType => EnemyStateType.Attack;

	Enemy _enemy;

	public AttackState(Enemy enemy)
	{
		_enemy = enemy;
	}

	public void Enter()
	{
		_enemy.Stop();
	}

	public void Exit()
	{
		
	}

	public void Update()
	{
		
	}
}

/// <summary>
/// 경직 상태 - 일정 시간 후 Normal 상태 복귀
/// </summary>
public class StaggerState : IEnemyState
{
	public EnemyStateType StateType => EnemyStateType.Stagger;

    Enemy _enemy;
    float _duration;
    float _timer;

    public StaggerState(Enemy enemy, float duration)
	{
		_enemy = enemy;
        _duration = duration;
	}

	public void Enter()
	{
        _enemy.Stop();
        _timer = 0;
	}

	public void Exit()
	{
	}

	public void Update()
	{
		_timer += Time.deltaTime;           //타이머 충전
        if (_timer > _duration)             //_duration만큼 시간 경과가 됐을 경우
		{
			//Normal 상태로 변경 요청
            _enemy.ChangeState(EnemyStateType.Normal);
		}
	}
}

/// <summary>
/// 사망 상태 - 일정 시간 후 적 게임오브젝트 제거
/// </summary>
public class DeadState : IEnemyState
{
	public EnemyStateType StateType => EnemyStateType.Dead;

	Enemy _enemy;
	float _duration;            //사망 상태 지속 시간
	float _timer;				//타이머

	public DeadState(Enemy enemy, float duration)
	{
		_enemy = enemy;
		_duration = duration;
	}

	public void Enter()
	{
		_enemy.Stop();
		_timer = 0;
	}

	public void Exit()
	{
		
	}

	public void Update()
	{
		_timer += Time.deltaTime;
		if (_timer > _duration)
		{
			_enemy.Remove();
		}
	}
}
