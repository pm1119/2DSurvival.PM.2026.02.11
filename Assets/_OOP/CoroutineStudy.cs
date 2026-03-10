using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.PlayerLoop;

/*
객체 지향과 직접적인 연관 X
유니티에서 시간 흐름 제어가 필요한 경우 유용한 도구

코루틴(Coroutine)
(면접용 답변 아님)
유니티에서 시간의 흐름을 제어할 수 있는 특별한 함수
-> 일반 함수와 달리 함수의 실행을 일시 중지하고 
다시 이어서 실행할 수 있는 기능

내가 마음대로 사용할 수 있는 Update() 비슷함
*/

public class CoroutineStudy : MonoBehaviour
{
	//현재 실행 중인 코루틴을 저장하기 위한 변수
	Coroutine _printRoutine;

	private void Update()
	{
		//스페이스 바를 누르면 코루틴 시작
		if (Input.GetKeyDown(KeyCode.Space) == true)
		{
			//이미 실행 중인 코루틴이 있으면
			if (_printRoutine != null)
			{
				StopCoroutine(_printRoutine);
				_printRoutine = null;
			}

			//코루틴 실행 후 해당 코루틴을 변수에 저장
			_printRoutine = StartCoroutine(PrintMessagesRoutine());
		}
	}

	IEnumerator PrintMessagesRoutine()
    {
		Debug.Log("첫 번째 메세지");

        //5초 대기 -> 이후 다음 줄 실행
        yield return new WaitForSeconds(1);

		Debug.Log("1초 후 메세지");

		//2.5초 대기
		yield return new WaitForSeconds(2.5f);

		Debug.Log("2초 더 후 메세지");

		//현실 시간 1초 대기
		yield return new WaitForSecondsRealtime(1);

		Debug.Log("현실 시간 1초 후 메세지");

		//한 프레임 대기
		yield return null;

		Debug.Log("한 프레임 대기 후 메세지");

		//다음 FixedUpdate 주기까지 대기
		yield return new WaitForFixedUpdate();

		Debug.Log("다음 FixedUpdate 대기 후 메세지");

		//코루틴 종료
		yield break;

		//Debug.Log("break 후 코드");  실행 안 됨
	}
}
