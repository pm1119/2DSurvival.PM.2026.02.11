using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 유니티 InputManager 방식을 사용해 입력을 알리는 역할
/// </summary>
public class InputManagerHandler : InputHandler
{
	public override event UnityAction<Vector2> OnMoveInput;

	// Update is called once per frame
	void Update()
    {
		_inputVector.x = Input.GetAxis("Horizontal");
		_inputVector.y = Input.GetAxis("Vertical");

		//이동 입력 이벤트 발행
		OnMoveInput?.Invoke(_inputVector);
	}
}
