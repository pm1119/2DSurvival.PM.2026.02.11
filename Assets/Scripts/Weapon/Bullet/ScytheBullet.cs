using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro.EditorUtilities;
using UnityEngine;

/// <summary>
/// 일정 방향을 향해 커지면서 날아가는 낫 형태의 총탄
/// </summary>
public class ScytheBullet : ProjectileBullet
{
    [Header("----- 런타임 데이터 -----")]
    [SerializeField] float _maxScale;       //최대 크기
    [SerializeField] float _rotspeed;       //회전 속도
    [SerializeField] float _damageDelay;    //대미지 간격

    //접촉한 적 컴포넌트 저장용 해쉬셋
    HashSet<Enemy> _enemies = new ();

    Coroutine _attackRoutine;

	private void Start()
	{
		_attackRoutine = StartCoroutine(AttackRoutine());
	}

    public void SetStat(float maxScale, float rotspeed, float damageDelay)
    {
        _maxScale = maxScale;
        _rotspeed = rotspeed;
        _damageDelay = damageDelay;
    }

	protected override void FixedUpdate()
	{
        //투사체 총탄(부모 클래스)와 같이 정해진 방향으로 이동
		base.FixedUpdate();
        
        float scale = Mathf.Lerp(1.0f, _maxScale, _timer / _duration);
        transform.localScale = Vector3.one * scale;

        //회전
        transform.Rotate(0, 0, _rotspeed * Time.fixedDeltaTime);
	}

	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            //접촉한 적 등록
            _enemies.Add(enemy);
        }
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		Enemy enemy = collision.GetComponent<Enemy> ();
        if (enemy != null)
        {
            //접촉한 적 등록 해제
            _enemies.Remove(enemy);
        }
	}

    /// <summary>
    /// 일정 시간마다 닿아있는 적들을 공격하는 코루틴 함수
    /// </summary>
    /// <returns></returns>
    IEnumerator AttackRoutine()
    {
        while (true)
        {
            //데이터 컨테이너(컬렉션)의 요소글을 가진 복제본 리스트 만들기 방식 1
            //List<Enemy> enemies = new List<Enemy>(_enemies);

            //방식 2
            List<Enemy> enemies = _enemies.ToList();
            foreach (Enemy enemy in enemies)
            {
                Attack(enemy);
            }

            //에러: _enemies 해쉬셋의 모든 요소를 공격
            //공격받은 적 중 일부는 파괴될 수 있다
            //적 게임오브젝트가 파괴되면서 콜라이더가 사라지기 때문에
            //OnTriggerExit2D()가 실행되고
            //이때 _enemies 해쉬셋에서 해당 enemy를 제거한다
            // -> foreach문이 돌아가는 중에 _enemies가 수정
            // => foreach문이 돌아가는 중에 대상 컬렉션이 수정되어선 안 된다
            //foreach (var item in _enemies)
            //{
            //    Attack(item);
            //}
            yield return new WaitForSeconds(_damageDelay);
        }
    }
}
