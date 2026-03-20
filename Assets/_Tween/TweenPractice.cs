using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TweenPractice : MonoBehaviour
{
    Tween _tween;

    [Header("----- 공통 설정 -----")]
    [SerializeField] float _duration = 1;

    [Header("----- RectTransForm -----")]
    [SerializeField] RectTransform _rectTransform;
    [SerializeField] CanvasGroup _canvasGroup;

    [Header("----- Image -----")]
    [SerializeField] Image _image;

    [Header("----- TMP -----")]
    [SerializeField] TMP_Text _text;

	private void Update()
	{
        if (Input.GetKeyDown(KeyCode.Alpha1) == true)
        {
            StartAnchoredPosition();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) == true)
        {
            StartScaleTween();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3) == true)
        {
            StartFadeTween();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4) == true)
        {
            StartExpBarTween();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha5) == true)
        {
            StartTextTween();
        }
	}

    //1. UI 이동
	public void StartAnchoredPosition()
    {
        _tween = _rectTransform
            .DOAnchorPos(new Vector2(200, 0), _duration);
    }

    //2. UI 스케일
    public void StartScaleTween()
    {
        _rectTransform.localScale = Vector3.zero;

        _tween = _rectTransform.DOScale(Vector3.one, _duration);
    }

    //3. UI 페이드
    public void StartFadeTween()
    {
        _canvasGroup.alpha = 1;

        _tween = _canvasGroup.DOFade(0, _duration)
            .SetLoops(2, LoopType.Yoyo);
    }

    //4. 경험치 바 이미지 차오르기
    public void StartExpBarTween()
    {
        float fillAmount = _image.fillAmount;

        _tween = _image
            .DOFillAmount(fillAmount + 0.2f, _duration)
            .SetEase(Ease.OutCubic);
    }

    //5. TMP 텍스트
    public void StartTextTween()
    {
        _text.color = Color.black;
        _text.rectTransform.localScale = Vector3.one;

        Sequence seq = DOTween.Sequence();

        seq.Append(_text.DOColor(Color.yellow, 0.5f));
        seq.Join(_text.rectTransform.DOScale(1.2f, 0.5f));
        seq.Append(_text.DOColor(Color.black, 0.5f));
        seq.Join(_text.rectTransform.DOScale(1, 0.5f));

        _tween = seq;
    }
}
