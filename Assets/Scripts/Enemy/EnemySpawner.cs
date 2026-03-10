using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 일정 시간 간격으로 타겟(플레이어 캐릭터) 주변에 생성하는 역할
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    [Header("----- 스폰 기준 -----")]
    [SerializeField] Hero _hero;                //플레이어 캐릭터

    [Header("----- 리소스 -----")]
    [SerializeField] Enemy _enemyPrefab;        //적 캐릭터
    [SerializeField] StatusView _statusView;

    [Header("----- 런타임 데이터 -----")]
    [SerializeField] Enemy _enemy;

    [SerializeField] float _minRadius;          //생성 범위 안쪽 원 반지름
    [SerializeField] float _maxRadius;          //생성 범위 바깥쪽 원 반지름
    [SerializeField] float _spawnSpan;          //생성 간격 시간(초)
    [SerializeField] float _distance;

	Coroutine _spawnEnemyCoroutine;             //적 생성 코루틴 변수

	public void Initialize()
    {
        //코루틴 실행
		_spawnEnemyCoroutine = StartCoroutine(SpawnEnemyRoutine());
    }

    /// <summary>
    /// 적 생성 코루틴 함수
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnSpan);
            SpawnEnemy();
        }
    }

    /// <summary>
    /// 적을 생성하는 함수
    /// </summary>
    void SpawnEnemy()
    {
		//_enemyPrefab 복제 생성
		Enemy enemy = Instantiate(_enemyPrefab, transform);

        //생성 위치 구하기
        Vector3 pos = GetSpawnPosition();

        //생성 위치 적용
        enemy.transform.position = pos;

		//생성된 적 초기화
		enemy.Initialize(_hero, this);

		float reX = _hero.transform.position.x - enemy.transform.position.x;
		float reY = _hero.transform.position.y - enemy.transform.position.y;

		if (reX > _distance || reY > _distance)
		{
			enemy.transform.position = pos;
		}
	}

    /// <summary>
    /// 중심점에서 원형 영역 내 무작위 위치를 반환하는 함수
    /// </summary>
    /// <returns></returns>
    public Vector3 GetSpawnPosition()
    {
        //중심 위치 잡기
        Vector3 pos = _hero.transform.position;

        //랜덤 반지름 구하기
        float radius = Random.Range(_minRadius, _maxRadius);

        //랜덤 방향 구하기
        Vector2 dir = Random.insideUnitCircle.normalized;

        pos.x += dir.x * radius;
        pos.y += dir.y * radius;

        return pos;
    }

	private void OnDrawGizmosSelected()
	{
        if (_hero == null) return;

		Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(_hero.transform.position, _minRadius);
		Gizmos.DrawWireSphere(_hero.transform.position, _maxRadius);
	}
}
