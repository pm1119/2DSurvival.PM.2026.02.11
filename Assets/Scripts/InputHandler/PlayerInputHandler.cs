using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// 유니티 InputSystem의 PlayerInput을 사용해 입력을 알리는 역할
/// </summary>
public class PlayerInputHandler : InputHandler
{
	public override event UnityAction<Vector2> OnMoveInput;

	// Update is called once per frame
	void Update()
    {
		//이동 입력 이벤트 발행
		OnMoveInput?.Invoke(_inputVector);
	}

	public void OnMove(InputValue inputValue)
	{
		_inputVector = inputValue.Get<Vector2>();
	}

	public void OnClick(InputValue inputValue)
	{
		
	}
}
