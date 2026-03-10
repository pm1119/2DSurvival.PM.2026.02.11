using System.Collections.Generic;
using UnityEngine;

//추상화(Abstraction)
// -> 객체의 복잡한 내부 구현은 숨기고, 
//외부에는 필요한 기능(사용법)만 제공하는 설계 방식

//abstract, override
//interface

//1. 추상 클래스
//2. 인터페이스

public class AbstractionStudy : MonoBehaviour
{
    //추상 클래스
    //1. 이 클래스의 객체를 직접 만들 수 없음
    //2. 자식 클래스가 해당 멤버를 반드시 구현하도록 강제한다
    public abstract class Enemy
    {
        public string Name;

        public Enemy(string name)
        {
            Name = name;
        }

        //공통 기능
        public void Move()
        {
            Debug.Log($"{Name}(이)가 이동합니다");
        }

        //반드시 자식 클래스가 구현해야 하는 함수
        public abstract void Attack();
    }

	//자식 클래스 1
	public class Slime : Enemy
	{
		public Slime() : base("슬라임")
		{

		}

		public override void Attack()
		{
			Debug.Log($"{Name}(이)가 산성 액체로 공격합니다");
		}
	}

	//자식 클래스 2
	public class Orc : Enemy
	{
		public Orc() : base("오크")
		{

		}

		public override void Attack()
		{
			Debug.Log($"{Name}(이)가 도끼로 내려칩니다");
		}
	}

	private void Start()
	{
		//Enemy는 추상 클래스이므로 직접 객체를 만들 수 없음
		//Enemy enemy = new Enemy("적");

		//대신 자식 클래스 객체를 통해 사용
		List<Enemy> enemies = new List<Enemy>
		{
			new Slime(),
			new Orc()
		};

		foreach (var enemy in enemies)
		{
			enemy.Move();		//공통 동작
			enemy.Attack();		//각기 다른 구현
		}
	}
}
