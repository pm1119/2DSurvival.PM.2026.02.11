using UnityEngine;

/*
인터페이스(Interface)
- 클래스가 어떤 기능을 갖고 있는지 미리 약속하는 것

추상 클래스처럼 기능들을 직접 구현하지 않고 
해당 인터페이스를 상속하는 클래스가 반드시 그 기능들을 구현한다는 것을 보장

-> 모든 멤버가 abstract인 추상 클래스
-> C#에서는 클래스 상속을 단일 상속만 지원함 
다중 상속 필요 시 인터페이스 활용
*/

//인터페이스의 정의
public interface IInteractable          //상호작용 가능한 
{
    //인터페이스에서는 접근 제어자를 생략하면 기본이 public
    void Interact();
}

public class Door : IInteractable
{
	string _name;

	public Door(string name)
	{
		_name = name;
	}

	public void Interact()
	{
		Debug.Log($"{_name}이(가) 열립니다");
	}
}

public class Chest : IInteractable
{
	string _name;

	public Chest(string name)
	{
		_name = name;
	}

	public void Interact()
	{
		Debug.Log($"{_name}을(를) 열어 보물을 획득합니다");
	}
}

public class InterfaceStudy : MonoBehaviour
{
	private void Start()
	{
		//상호작용 가능한 객체들을 배열로 관리
		IInteractable[] interactables = new IInteractable[]
		{
			new Door("철문"),
			new Chest("황금상자")
		};

		foreach (var item in interactables)
		{
			item.Interact();
		}
	}
}
