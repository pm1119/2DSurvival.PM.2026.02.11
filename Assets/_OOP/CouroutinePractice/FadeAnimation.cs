using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeAnimation : MonoBehaviour
{
    [SerializeField] Image _image;      //이미지 컴포넌트
    [SerializeField] float _duration;   //연출 지속 시간

    Coroutine _fadeRoutine;             //페이드 애니메이션 코루틴 변수
	Coroutine _moveCoroutine;			//이동 애니메이션 코루틴 변수

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1) == true)
        {
            if (_fadeRoutine != null)
            {
                StopCoroutine( _fadeRoutine );
                _fadeRoutine = null;
            }
            _fadeRoutine = StartCoroutine(FadeOut());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) == true)
        {
			if (_fadeRoutine != null)
			{
				StopCoroutine(_fadeRoutine);
				_fadeRoutine = null;
			}
			_fadeRoutine = StartCoroutine(FadeIn());
		}
		else if (Input.GetKeyDown(KeyCode.Space) == true)
		{
			if(_moveCoroutine != null)
			{
				StopCoroutine(_moveCoroutine);
			}
			_moveCoroutine = StartCoroutine(MoveUp());
		}
	}

	IEnumerator FadeOut()
    {
        float timer = 0;

        Color color = _image.color;

        while (timer < _duration)
        {
            timer += Time.deltaTime;

			//알파값 계산(1 -> 0)
			//float alpha = 1 - (timer / _duration);

			//Mathf.Lerp(시작값, 목표값, 중간 비율 t)
			//t가 0이면 시작값
			//t가 1이면 목표값
			float alpha = Mathf.Lerp(1.0f, 0.0f, timer / _duration);

            //알파값 적용
            color.a = alpha;

            //컬러 적용
            _image.color = color;

            //다음 프레임까지 대기
            yield return null;
        }

        color.a = 0;
        _image.color = color;

        Debug.Log("페이드 아웃 완료");
    }

    IEnumerator FadeIn()
    {
		float timer = 0;

		Color color = _image.color;

		while (timer < _duration)
		{
			timer += Time.deltaTime;

			//알파값 계산(0 -> 1)
			float alpha = Mathf.Lerp(0.0f, 1.0f, timer / _duration);

			//알파값 적용
			color.a = alpha;

			//컬러 적용
			_image.color = color;

			//다음 프레임까지 대기
			yield return null;
		}

		color.a = 1;
		_image.color = color;

		Debug.Log("페이드 인 완료");
	}

	IEnumerator MoveUp()
	{
		//타이머
		float timer = 0;

		//이미지 컴포넌트가 붙어 있는 게임오브젝트의 RectTransform 컴포넌트 가져오기
		RectTransform rect = _image.rectTransform;

		//시작 위치
		Vector2 startPos = rect.anchoredPosition;

		//목표 위치
		//Vector2 endPos = rect.anchoredPosition + new Vector2(0, 200);
		Vector2 endPos = rect.anchoredPosition + Vector2.up * 200;

		//타이머
		while (timer < _duration)
		{
			timer += Time.deltaTime;

			float alpha = timer / _duration;

			//위치 적용
			rect.anchoredPosition = Vector2.Lerp(startPos, endPos , alpha);

			yield return null;
		}

		rect.anchoredPosition = endPos;

		Debug.Log("이동 완료");
	}
}
