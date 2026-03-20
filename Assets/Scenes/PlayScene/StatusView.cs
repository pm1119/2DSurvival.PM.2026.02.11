using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// PlayScene 상태 UI 표시(경험치 바, 레벨, 킬 수, 남은 시간)
/// </summary>
public class StatusView : MonoBehaviour
{
    [Header("----- 컴포넌트 -----")]
    [SerializeField] Image _expBar;                     //경험치 바
    [SerializeField] TMP_Text _levelText;               //레벨 텍스트
    [SerializeField] TMP_Text _killCount;               //킬 수 텍스트
    [SerializeField] TMP_Text _remainingTimeText;       //남은 시간 텍스트

    [Header("----- 트윈 설정 -----")]
    [SerializeField] float _expBarTweenDuration = 1f;
    [SerializeField] Color _killHighrightColor = Color.red;
	[SerializeField] float _killSeqDuration = 0.15f;
    [SerializeField] float _killSeqScale = 1.2f;

	Tween _expBarTween;                                 //경험치 바 트윈

    Sequence _killCountSequence;                        //킬 수 시퀀스

    Sequence _remainingTimeSequence;                    //플레이 타임 시퀀스

    Color _killOriginColor;                             //킬 수 텍스트 초기 색상
    Vector3 _killOrigonScale;                           //킬 수 텍스트 초기 스케일

	Color _remainingOriginColor;                             //킬 수 텍스트 초기 색상

	private void Awake()
	{
		_killOriginColor = _killCount.color;
        _killOrigonScale = _killCount.rectTransform.localScale;
        _remainingOriginColor = _remainingTimeText.color;
	}

	/// <summary>
	/// 경험치 UI를 갱신하는 함수
	/// </summary>
	/// <param name="currentExp"></param>
	/// <param name="maxExp"></param>
	public void UpdateExp(float currentExp, float maxExp)
    {
        float targetAmount = currentExp / maxExp;

		//이전 경험치 바 연줄 정지
		_expBarTween?.Kill();

		_expBarTween = _expBar
            .DOFillAmount(targetAmount, _expBarTweenDuration)
            .SetEase(Ease.OutCubic);
    }

    /// <summary>
    /// 레벨 UI를 갱신하는 함수
    /// </summary>
    /// <param name="level"></param>
    public void UpdateLevel(int level)
    {
        _levelText.text = (level + 1).ToString();
    }

    public void UpdateKillCount(int killCount)
    {
        _killCount.text = killCount.ToString();

        _killCountSequence?.Kill();

        //연출 전 초기화
        _killCount.color = _killOriginColor;
        _killCount.rectTransform.localScale = _killOrigonScale;

        _killCountSequence = DOTween.Sequence();
        _killCountSequence.SetUpdate(true);                 //Time.timeScale에 영향을 안 받는다

        _killCountSequence.Append(
            _killCount.DOColor(_killHighrightColor, _killSeqDuration));
        _killCountSequence.Join(
            _killCount.rectTransform.DOScale(_killSeqScale, _killSeqDuration));
		_killCountSequence.Append(
			_killCount.DOColor(_killOriginColor, _killSeqDuration));
		_killCountSequence.Join(
			_killCount.rectTransform.DOScale(_killOrigonScale, _killSeqDuration));
	}

    /// <summary>
    /// 남은 시간 UI를 갱신하는 함수
    /// </summary>
    /// <param name="time"></param>
    public void UpdateRemainingTime(float time)
    {
        _remainingTimeText.color = Color.white;

        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

		_remainingTimeText.text = $"{minutes}:{seconds:00}";
    }
}