using UnityEngine;

public class TestScene : MonoBehaviour
{
    [SerializeField] EventStudy _publisher;		//이벤트 발행자
    [SerializeField] EventTest _subscriber;     //이벤트 구독자
	[SerializeField] Warrior _warrior;
	[SerializeField] WarriorHpBar _warriorHpBar;

	private void Start()
	{
		//이벤트 발행자의 S키 눌림 이벤트를 
		//구독자의 UseSkill() 함수가 구독
		_publisher.OnSKeyDown += _subscriber.UseSkill;  //+=: 더해서 값을 저장하는 같은 기호의 다른 연산자

		//_warrior의 체력 변경 이벤트를
		//_warriorHpBar의 UpdateHpBar() 함수가 구독
		_warrior.OnHpChanged += _warriorHpBar.UpdateHpBar;
	}
}
