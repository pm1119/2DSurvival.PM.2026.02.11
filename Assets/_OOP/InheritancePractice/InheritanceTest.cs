using UnityEngine;

public class InheritanceTest : MonoBehaviour
{
    [SerializeField] ColorChanger _colorChanger;
    [SerializeField] MonoBehaviour _monoBehaviour;
    [SerializeField] Object _object; //Object는 유니티의 최상위 클래스이므로 씬을 포함한 모든 객체 연결 가능

	private void Start()
	{
		
	}
}
