using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

//InputManagerHandler는 유니티의 InputManager 방식으로 OnMoveInput 이벤트 발행
//InputSystemHandler는 유니티의 InputSystem 방식으로 OnMoveInput 이벤트 발행

/// <summary>
/// 사용자 입력을 받아 알리는 역할
/// </summary>
public abstract class InputHandler : MonoBehaviour
{
    /// <summary>
    /// 이동 입력 이벤트
    /// </summary>
    public abstract event UnityAction<Vector2> OnMoveInput;

    protected Vector2 _inputVector;
}
