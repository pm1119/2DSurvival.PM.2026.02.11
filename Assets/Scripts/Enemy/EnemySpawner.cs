using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 일정 시간 간격으로 타겟(플레이어 캐릭터) 주변에 생성하는 역할
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    [Header("----- 설정 데이터 -----")]
    [SerializeField] StageData _stageData;      //스테이지 데이터
    [SerializeField] WaveData _waveData;                         //현재 웨이브 데이터

    [Header("----- 스폰 기준 -----")]
    [SerializeField] Hero _hero;                //플레이어 캐릭터

    [Header("----- 리소스 -----")]
    [SerializeField] HeroModel _heroModel;
    [SerializeField] Enemy[] _enemyPrefabs;        //적 캐릭터 배열
    [SerializeField] ExpItem[] _expItemPrefabs;    //경험치 아이템 배열
    [SerializeField] HealthItem _healthItemPrefab;       //체력 아이템

    [Header("----- 런타임 데이터 -----")]
    [SerializeField] int _killCount;            //킬 수
    [SerializeField] float _playTime;           //플레이 시간 
    [SerializeField] float _minRadius;          //생성 범위 안쪽 원 반지름
    [SerializeField] float _maxRadius;          //생성 범위 바깥쪽 원 반지름
    [SerializeField] float _spawnSpan;          //생성 간격 시간(초)
    [SerializeField] float _distance;
	[SerializeField] int _hpItemDropRate;

	public event UnityAction<float> OnRemainingTimeChanged;

    public event UnityAction<int> OnKillCountChanged;       //킬 수 변경 이벤트

	Coroutine _spawnEnemyCoroutine;             //적 생성 코루틴 변수

    Coroutine _playTimeRoutine;                 //현재 웨이브 계산 코루틴 변수

    /// <summary>
    /// 생성한 경험치 아이템 리스트
    /// </summary>
    [SerializeField] List<ExpItem> _expItems = new List<ExpItem>();

	public void Initialize()
    {
        //코루틴 실행
		_spawnEnemyCoroutine = StartCoroutine(SpawnEnemyRoutine());
        _playTimeRoutine = StartCoroutine(PlayTimeRoutine());

        //킬 수 변경 이벤트 발행
        OnKillCountChanged?.Invoke(_killCount);

		//시간 변경 이벤트 발행
		OnRemainingTimeChanged?.Invoke(_playTime);
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

    IEnumerator PlayTimeRoutine()
    {
        while (true)
        {
            _playTime += Time.deltaTime;
            //현재 플레이 시간에 맞는 웨이브 데이터 가져오기
            _waveData = _stageData.GetWaveData(_playTime);
            _spawnSpan = _waveData.SpawnSpan;

            //남은 시간 계산
            float reaminingTime = _stageData.PlayTime - _playTime;
			OnRemainingTimeChanged?.Invoke(reaminingTime);
			if (reaminingTime <= 0)
            {
                StopAllCoroutines();
            }
            yield return null;
        }
    }

    /// <summary>
    /// 적을 생성하는 함수
    /// </summary>
    void SpawnEnemy()
    {
        //랜덤한 적 프리팹 선택
        Enemy enemyPrefab = _enemyPrefabs[_waveData.GetRandomEnemyIndex()];

		//_enemyPrefab 복제 생성
		Enemy enemy = Instantiate(enemyPrefab, transform);

        //생성 위치 구하기
        Vector3 pos = GetSpawnPosition();

        //생성 위치 적용
        enemy.transform.position = pos;

        //생성된 적 사망 이벤트 구독
        enemy.OnDead += HandleEnemyDead;

		//생성된 적 초기화
		enemy.Initialize(_hero, this, _waveData);

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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="enemy"></param>
    public void HandleEnemyDead(Enemy enemy)
    {
        //킬 수 증가
        _killCount++;

        //킬 수 증가 이벤트 발행
        OnKillCountChanged?.Invoke(_killCount);

        //경험치 아이템 생성 확률 판정
        if (Random.value < _waveData.ExpItemDropRate)
        {
			//경험치 생성
			SpawnExpItem(enemy.transform.position);
		}

        if (Random.value < _hpItemDropRate)
        {
            SpawnHpItem(enemy.transform.position);
        }
    }

    /// <summary>
    /// 경험치 아이템을 스폰하는 함수
    /// </summary>
    /// <param name="pos"></param>
    public void SpawnExpItem(Vector3 pos)
    {
        //랜덤 인덱스 생성
        int index = Random.Range(0, _waveData.GetRandomExpItemIndex());

        //프리팹 선택
        ExpItem prefab = _expItemPrefabs[index];

        //복제본 생성
        ExpItem expItem = Instantiate(prefab);

        //위치 설정
        expItem.transform.position = pos;

        //경험치 아이템 제거 이벤트 구독
        expItem.OnRemoved += HandleExpItemRemoved;

        //초기화
        expItem.Initialize(_hero);

        //생성된 경험치 아이템을 리스트에 추가
        _expItems.Add(expItem);
    }

    public void SpawnHpItem(Vector3 pos)
    {
        int index = Random.Range(0, _hpItemDropRate);

        HealthItem healthItem = _healthItemPrefab;

        healthItem.transform.position = pos;

        healthItem.Initialize(_heroModel);
    }

    [ContextMenu("자석 아이템 효과 발동")]
    /// <summary>
    /// 자석 아이템 효과를 실행하는 함수
    /// </summary>
    public void StartMagnetEffect()
    {
        foreach (var item in _expItems)
        {
            item.StartMagnetRoutine();
        }
    }

    public void HandleExpItemRemoved(ExpItem expItem)
    {
        _expItems.Remove(expItem);
    }

	private void OnDrawGizmosSelected()
	{
        if (_hero == null) return;

		Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(_hero.transform.position, _minRadius);
		Gizmos.DrawWireSphere(_hero.transform.position, _maxRadius);
	}
}
