using System.Collections.Generic;
using UnityEngine;

//다형성(Polymorphism)
//여러 가지 형태를 가질 수 있음
// -> 같은 이름의 함수라고 해도 객체에 따라 다르게 동작할 수 있다.

//부모 클래스: Enemy
//자식 클래스: Slime, Orc, Bat
//슬라임은 통통 튀어서 이동(Slime.Move())
//오크는 뛰어서 이동(Orc.Move())
//박쥐는 날아서 이동(Bat.Move())

//서로 다른 Move() 함수를 Enemy.Move()로 한번에 처리하는 방식

//virtual: 자식 클래스가 해당 함수를 재정의할 수 있도록 허용(부모 클래스에 적어줌)
//override: 상속받은 virtual 함수를 자식 클래스의 구체적인 동작으로 재정의

public class PolymorphismStudy : MonoBehaviour
{
    //부모 클래스
    public class Enemy
    {
        public string Name;

        //생성자
        //어떤 클래스의 객체를 만들 때 자동으로 실행되는 특수한 함수
        public Enemy(string name)
        {
            Name = name;
        }
          
        public virtual void Move()
        {
            Debug.Log($"{Name}이(가) 이동하려고 합니다. (부모 클래스 기본 동작)");
        }
    }

    //자식 클래스
    public class Slime : Enemy
    {
		//부모 클래스에 명시적인 생성자가 존재할 경우 
		//자식 클래스가 반드시 부모 클래스 중의 생성자 중 하나를 사용해야 한다.
		public Slime() : base("슬라임") { }

		public override void Move()
        {
            //부모 클래스(Enemy)의 Move() 함수 실행
            base.Move();

            Debug.Log($"{Name}이(가) 통통 튀며 이동합니다.");
		}
    }

	public class Orc : Enemy
	{
		public Orc() : base("오크") { }

		public override void Move()
		{
			base.Move();
			Debug.Log($"{Name}이(가) 뛰어가며 돌진합니다.");
		}
	}

	public class Bat : Enemy
	{
		public Bat() : base("박쥐") { }

		public override void Move()
		{
			base.Move();
			Debug.Log($"{Name}이(가) 날아갑니다.");
		}
	}

	private void Start()
	{
		//Enemy 타입 리스트에 다양한 자식 객체를 담는다.
		List<Enemy> enemies = new ();
		enemies.Add(new Enemy("기본 적"));
		enemies.Add(new Slime());
		enemies.Add(new Orc());
		enemies.Add (new Bat());

		//다형성의 핵심: 하나의 사용법으로 다양한 동작 실행
		foreach (var enemy in enemies)
		{
			enemy.Move();
		}
	}
}
