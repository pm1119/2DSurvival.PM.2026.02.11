using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// 모바일 환경에서 사용자 입력을 다루는 역할
/// </summary>
public class MobileInputHandler : InputHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
	public enum JoyStickMode
	{
		Fixed,				//고정
		Floating,			//반응형
	}

	[Header("----- 컴포넌트 -----")]
	[SerializeField] RectTransform _background;		//모바일 조이스틱 배경
	[SerializeField] RectTransform _handle;			//모바일 조이스틱 핸들
	[SerializeField] RectTransform _touchArea;      //터치 영역

	[Header("----- 설정 데이터 -----")]
	[SerializeField] float _radius;                 //조이스틱 핸들의 활동 반경
	[SerializeField] JoyStickMode _stickMode;       //조이스틱 모드

	public override event UnityAction<Vector2> OnMoveInput;

	private void Update()
	{
		OnMoveInput?.Invoke(_inputVector);
	}

	public void ResetJoyStick()
	{
		_inputVector = Vector2.zero;
		_handle.anchoredPosition = Vector2.zero;
	}

	void UpdateJoyStick(PointerEventData eventData)
	{
		//_handle의 anchoredPosition에 적용할 Vector2
		Vector2 localPoint;

		//스크린 상의 점: eventData.position
		//카메라: eventData.pressEventCamera
		//기준으로 할 RectTransform: _background
		//스크린상에서 유저가 클릭한 지점의 좌표를
		//_background 기준의 좌표로 변환
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
			_background, eventData.position,
			eventData.pressEventCamera,
			out localPoint) == false) return;

		Vector2 normalized = localPoint / _radius;

		//반경을 벗어난 경우
		if (normalized.magnitude > 1)
			normalized = normalized.normalized;

		_inputVector = normalized;
		_handle.anchoredPosition = normalized * _radius;
	}

	//포인터를 눌렀을 때
	public void OnPointerDown(PointerEventData eventData)
	{
		UpdateJoyStick(eventData);
		if (_stickMode == JoyStickMode.Floating)
		{
			_background.position = eventData.position;
			_background.gameObject.SetActive(true);
		}
	}

	//포인터를 뗐을 때
	public void OnPointerUp(PointerEventData eventData)
	{
		ResetJoyStick();
		if (_stickMode == JoyStickMode.Floating)
		{
			_background.gameObject.SetActive(false);
		}
	}
	
	//드래그 중일 때 실행되는 함수
	//드래그 중: 포인터를 떼지 않고 위치가 변하는 중
	public void OnDrag(PointerEventData eventData)
	{
		UpdateJoyStick(eventData);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		Debug.Log("포인터 엔터");
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Debug.Log("포인터 엑시트");
	}
}
