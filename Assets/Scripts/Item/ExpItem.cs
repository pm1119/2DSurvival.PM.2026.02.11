using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 플레이어 캐릭터와 닿으면 경험치를 획득하는 아이템
/// </summary>
public class ExpItem : MonoBehaviour
{
    [SerializeField] Hero _hero;                //플레이어 캐릭터

    [Header("----- 경험티 보상 -----")]
    [SerializeField] float _expReward;          //경험치 보상

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
        Debug.Log("자석 효과 코루틴 실행");
        //TODO
        //점점 가속하면서 _hero의 위치로 이동하는 코루틴
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
