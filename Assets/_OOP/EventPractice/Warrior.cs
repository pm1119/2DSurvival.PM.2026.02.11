using UnityEngine;
using UnityEngine.Events;

/*
유저가 A키를 누르면 
-> 현재 체력 10 감소
-> 체력 변경 이벤트 알림(현재 체력과 최대 체력을 매개변수로 전달)
유저가 S키를 누르면
-> 현재 체력 5 증가
-> 체력 변경 이벤트 알림(현재 체력과 최대 체력을 매개변수로 전달)
*/

public class Warrior : MonoBehaviour
{
	//event 키워드: 외부에서 구독/구독 해제만 가능하게 키워드
	/// <summary>
	/// 체력 변경 이벤트(현재 체력, 최대 체력)
	/// </summary>
	public event UnityAction<float, float> OnHpChanged;

    //public UnityAction<float, float> OnAKeyDown;
    //public UnityAction<float, float> OnSKeyDown;  

    [Header("----- 체력 -----")]
    [SerializeField] float _maxHp;
    [SerializeField] float _currentHp;

    private void Start()
    {
        //Initialize();
    }

    private void Update()
	{
		if (Input.GetKeyDown(KeyCode.A) == true)
        {
            //_currentHp -= 10;
            //OnAKeyDown?.Invoke(_currentHp, _maxHp);
            AddHp(-5);
        }

        if (Input.GetKeyDown(KeyCode.S) == true)
        {
			//_currentHp += 5;
			//OnSKeyDown?.Invoke(_currentHp, _maxHp);
            AddHp(10);
        }
	}

    public void AddHp(float amount)
    {
        _currentHp = Mathf.Clamp(_currentHp + amount, 0, _maxHp);

        OnHpChanged?.Invoke(_currentHp, _maxHp);
    }

    //public void Initialize()
    //{
    //    _currentHp = _maxHp;
    //}
}
