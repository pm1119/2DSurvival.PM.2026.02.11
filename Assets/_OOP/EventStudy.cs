using System;
using UnityEngine;
using UnityEngine.Events;

//이벤트(Event)

public class EventStudy : MonoBehaviour
{
	//UnityEvent: 인스펙터뷰에서 함수 연결 가능
	public UnityEvent OnAKeyDown;

	//UnityAction: 코드로 함수 연결
	//함수를 변수처럼 저장
	public UnityAction<int> OnSKeyDown;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.A) == true)
		{
			if(OnAKeyDown != null)
			{
				OnAKeyDown.Invoke();
			}
		}

		if (Input.GetKeyUp(KeyCode.S) == true)
		{
			OnSKeyDown?.Invoke(3);
			//if (OnSKeyDown != null)
			//{
			//	OnSKeyDown.Invoke();
			//}
		}
	}
}
