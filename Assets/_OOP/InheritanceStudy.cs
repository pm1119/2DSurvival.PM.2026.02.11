using UnityEngine;

//상속(Inheritance)
// - 상속은 기존에 있는 클래스를 기반으로 새로운 클래스를 만들 수 있도록 하는 문법
//부모 클래스가 가진 멤버 변수와 함수를 자식 클래스가 물려받아 사용할 수 있다
//마치 부모가 자식에게 유산을 넘기듯 이미 작성한 코드를 재활용할 수 있게 해준다

// -> 코드 재활용
// -> 자식 클래스로 만들어진 객체는 부모 클래스 변수로 다뤄질 수 있다.
//대신 해당 부모 클래스에 정의된 멤버들만 사용할 수 있다.

public class InheritanceStudy : MonoBehaviour
{
    public class Animal
    {
        public string Name;

        public void Speak()
        {
            Debug.Log($"{Name}(이)가 소리를 냅니다");
        }
    }

    public class Dog : Animal
    { 
        public void Bark()
        {
            Debug.Log($"{Name}(이)가 짖습니다");
        }
    }

	private void Start()
	{
		//유니티 MonoBehaviour 상속 클래스 객체는 new 키워드 사용 제한
		//일반 C# 클래스 객체는 new 키워드로 객체 생성
		//MonoBehaviour 상속 클래스 객체는 AddComponent()나 Instantiate() 등 사용

		Dog dog = new Dog();
        dog.Name = "초코";

        dog.Speak();
        dog.Bark();
	}
}
