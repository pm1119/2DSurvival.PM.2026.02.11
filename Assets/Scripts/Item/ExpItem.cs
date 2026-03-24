using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 플레이어 캐릭터와 닿으면 경험치를 획득하는 아이템
/// </summary>
public class ExpItem : MonoBehaviour
{
    [SerializeField] Hero _hero;                //플레이어 캐릭터

    [Header("----- 컴포넌트 -----")]
    [SerializeField] Animator _animator;

    [Header("----- 경험티 보상 -----")]
    [SerializeField] float _expReward;          //경험치 보상

    [Header("----- 자석 효과 -----")]
    [SerializeField] float _startSpeed;
    [SerializeField] float _accleration;

    Coroutine _magnetRoroutine;                 //자석 효과 코루틴 변수

    /// <summary>
    /// 자신 제거 이벤트
    /// </summary>
    public event UnityAction<ExpItem> OnRemoved;

    public void Initialize(Hero hero)
    {
        _hero = hero;
    }

    /// <summary>
    /// 자석 루틴을 실행하는 함수
    /// </summary>
    public void StartMagnetRoutine()
    {
        if (_magnetRoroutine != null)
        {
            StopCoroutine(MagnetRoutine());
            _magnetRoroutine = null;
        }
        _magnetRoroutine = StartCoroutine(MagnetRoutine());
    }

    IEnumerator MagnetRoutine()
    {
        //애니메이터 비활성화
        _animator.enabled = false;

        //시작 속력
        float speed = _startSpeed;

        while (true)
        {
            //자신 게임오브젝트에서 플레이어 캐릭터 위치로 향하는 방향 벡터
            Vector3 dir = (_hero.transform.position - transform.position).normalized;

            //이동
            transform.Translate(dir * speed * Time.deltaTime);

            //속력 증가
            speed = _accleration * Time.deltaTime;

            //한 프레임 대기
            yield return null;
        }
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        //상대 콜라이더 게임오브젝트의 태그가 Player이면
		if (collision.CompareTag("Player") == true)
        {
            _hero.AddExp(_expReward);
            OnRemoved?.Invoke(this);
            Destroy(gameObject);
        }
	}
}
