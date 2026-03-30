using UnityEngine;

//싱글톤 패턴
//싱글톤 클래스: 객체가 하나인 것이 보장되는 클래스
//전역(게임 전체) 관리자 역할로 활용 가능

/// <summary>
/// 게임 관리자(싱글톤)
/// </summary>
public class GameManager : MonoBehaviour
{
    //싱글톤 객체를 가리키는 변수
    static GameManager _instance;

    /// <summary>
    /// 싱글톤 객체
    /// </summary>
    public static GameManager Instance => _instance;

    [SerializeField] ChallangeManager _challangeManager;
    public ChallangeManager ChallangeManager => _challangeManager;

	private void Awake()
	{
        //싱글톤 객체가 아직 설정되지 않았다면
		if (_instance == null)
        {
            //싱글톤 객체를 자신으로 설정
            _instance = this;

            //씬 전환 시에도 자신 게임오브젝트가 파괴되지 않도록 설정
            DontDestroyOnLoad(gameObject);
        }
        //이미 싱글톤 객체가 존재하는 경우
        else
        {
            //자신 게임오브젝트 파괴
            Destroy(gameObject);
        }
	}

    public void DoSomething()
    {
        Debug.Log("전역 관리 기능 실행", gameObject);
    }
}
