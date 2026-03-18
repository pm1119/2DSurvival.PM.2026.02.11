using System.Collections;
using DG.Tweening;
using UnityEngine;

public class TweenTest : MonoBehaviour
{
    [SerializeField] float _duration;

    Coroutine _moveRoutine;

	//재생되고 있는 Tween 변수
	Tween _tween;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.C) == true)
        {
            StartMoveRoutine();
        }
        else if (Input.GetKeyDown(KeyCode.D) == true)
        {
            StartMoveTween();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha1) == true)
        {
            StartRotateTween();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2) == true)
        {
            StarSequenceTween();
        }
	}

	public void StartMoveRoutine()
    {
        _moveRoutine = StartCoroutine(MoveRoutine());
    }

    IEnumerator MoveRoutine()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.position + Vector3.right * 5;
        float timer = 0;
        while (timer < _duration)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, endPos, timer / _duration);
            yield return null;
        }
    }

    public void StartMoveTween()
    {
        //_tween.Kill();
		//DOMove(목표 위치, 지속 시간)
		_tween = transform
			.DOMove(transform.position + Vector3.left * 5, _duration)
            .SetEase(Ease.OutQuad);
    }

    public void StartRotateTween()
    {
        _tween = transform
            .DORotate(new Vector3(0, 0, 360), _duration, RotateMode.FastBeyond360);
    }

    public void StarSequenceTween()
    {
        //시퀀스 객체 생성
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOMoveX(transform.position.x + 1.5f, 1));                          //순차 실행
        seq.Append(transform.DOMoveY(transform.position.y + 2.0f, 1));                          //순차 실행
        seq.Join(transform.DORotate(new Vector3(0, 0, 180), 1, RotateMode.FastBeyond360));      //동시 실행
        seq.AppendInterval(2);                                                                  //2초 대기
        seq.AppendCallback(LogMessage);                                                         //함수 실행

        _tween = seq;
    }

    void LogMessage()
    {
        Debug.Log("시퀀스 종료");
    }
}