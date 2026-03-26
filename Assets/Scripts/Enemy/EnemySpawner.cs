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
    [SerializeField] WaveData _waveData;        //현재 웨이브 데이터

    [Header("----- 스폰 기준 -----")]
    [SerializeField] Hero _hero;                //플레이어 캐릭터

    [Header("----- 리소스 -----")]
    [SerializeField] HeroModel _heroModel;
    [SerializeField] Enemy[] _enemyPrefabs;              //적 캐릭터 배열
    [SerializeField] ExpItem[] _expItemPrefabs;          //경험치 아이템 배열
    [SerializeField] MagnetItem _magnetItemPrefab;       //자석 아이템 프리팹
    [SerializeField] HealthItem _healthItemPrefab;       //체력 아이템
    [SerializeField] GameObject _clear;

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

    Pool _magnetItemPool;                       //자석 아이템 풀
    Pool[] _expItemPools;                       //경험치 아이템 풀 배열
    Pool[] _enemyPools;                         //적 풀 배열                                     

    /// <summary>
    /// 생성한 경험치 아이템 리스트
    /// </summary>
    [SerializeField] List<ExpItem> _expItems = new List<ExpItem>();

	public void Initialize()
    {
        _clear.SetActive(false);

        //자석 아이템 풀 생성
        CreateMagnetItemPool();

        //경험치 아이템 풀들 생성
        CreateExpItemPools();

        //적 풀들 생성
        CreateEnemyPools();

        //코루틴 실행
		_spawnEnemyCoroutine = StartCoroutine(SpawnEnemyRoutine());
        _playTimeRoutine = StartCoroutine(PlayTimeRoutine());

        //킬 수 변경 이벤트 발행
        OnKillCountChanged?.Invoke(_killCount);

		//시간 변경 이벤트 발행
		OnRemainingTimeChanged?.Invoke(_playTime);
    }

    /// <summary>
    /// 자석 아이템 풀을 만드는 함수
    /// </summary>
    void CreateMagnetItemPool()
    {
        //풀의 부모 게임오브젝트 생성
        GameObject parent = new GameObject("MagnetItem Pool");

        //풀 생성
        _magnetItemPool = 
            new Pool(_magnetItemPrefab.gameObject, parent.transform, 10); 
    }

    /// <summary>
    /// 경험치 아이템 풀들을 만드는 함수
    /// </summary>
    void CreateExpItemPools()
    {
        _expItemPools = new Pool[_expItemPrefabs.Length];
        for (int i = 0; i < _expItemPrefabs.Length; i++)
        {
            //풀의 부모 게임오브젝트 생성
            GameObject parent 
                = new GameObject($"{_expItemPrefabs[i].name}_Pool");

            //풀 생성
            _expItemPools[i]
                = new Pool(_expItemPrefabs[i].gameObject, parent.transform, 10);
        }
    }

    /// <summary>
    /// 적 풀을 만드는 함수
    /// </summary>
    void CreateEnemyPools()
    {
        _enemyPools = new Pool[_enemyPrefabs.Length];
        for (int i = 0; i < _enemyPrefabs.Length; i++)
        {
            //풀 부모 게임오브젝트 생성
            GameObject parent
                = new GameObject($"{_enemyPrefabs[i].name}_Pool");

            //풀 생성
            _enemyPools[i] = new Pool(_enemyPrefabs[i].gameObject, parent.transform, 10);
        }
    }

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            StartMagnetEffect();
        }
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
                Time.timeScale = 0;
                _clear.SetActive(true);
            }
            yield return null;
        }
    }

    /// <summary>
    /// 적을 생성하는 함수
    /// </summary>
    void SpawnEnemy()
    {
        //풀 선택
        Pool pool = _enemyPools[_waveData.GetRandomEnemyIndex()];

        //게임오브젝트 꺼내오기
        GameObject enemyGo = pool.Pop();

        //위치 설정
        enemyGo.transform.SetParent(transform);
        enemyGo.transform.position = GetSpawnPosition();

        //Enemy 컴포넌트 가져오기 
        Enemy enemy = enemyGo.GetComponent<Enemy>();

        //생성된 적 사망 이벤트 구독
        enemy.OnDead += HandleEnemyDead;

		//생성된 적 초기화
		enemy.Initialize(_hero, this, _waveData);
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

        //자석 아이템 생성 확률 판정
        if (Random.value < _waveData.MagnetItemDropRate)
        {
            SpawnMagnetItem(enemy.transform.position);
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

        //풀 선택
        Pool pool = _expItemPools[index];

        //풀에서 꺼내오기
        GameObject expItemGo = pool.Pop();

        //위치 설정
        expItemGo.transform.SetParent(transform);
        expItemGo.transform.position = pos;

		//ExpItem 컴포넌트 가져오기
		ExpItem expItem = expItemGo.GetComponent<ExpItem>();

        //경험치 아이템 제거 이벤트 구독
        expItem.OnRemoved += HandleExpItemRemoved;

        //초기화
        expItem.Initialize(_hero);

        //생성된 경험치 아이템을 리스트에 추가
        _expItems.Add(expItem);
    }

    public void SpawnMagnetItem(Vector3 pos)
    {
        //풀에서 자식 아이템 게임오브젝트 가져오기
        GameObject magnetItemGo = _magnetItemPool.Pop();
        magnetItemGo.transform.SetParent(transform);
        magnetItemGo.transform.position = pos;

		//게임오브젝트에서 MagnetItem 컴포넌트 가져오기
		MagnetItem magnetItem = magnetItemGo.GetComponent<MagnetItem>();
        magnetItem.Initialize(this);
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
