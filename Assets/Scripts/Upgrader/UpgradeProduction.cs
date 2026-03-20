using DG.Tweening;
using UnityEngine;

public class UpgradeProduction : MonoBehaviour
{
    [Header("----- ÄÄÆ÷³ÍÆ® -----")]
	[SerializeField] RectTransform _rectTransform;
	[SerializeField] float _popUpDuration;

	Tween _popUpTween;

	private void Awake()
	{
		_rectTransform.localScale = Vector3.zero;
	}

	public void PopUp()
	{
		_popUpTween = _rectTransform.DOScale(Vector3.one, _popUpDuration);
	}

	public void PopDown()
	{
		_popUpTween = _rectTransform.DOScale(Vector3.zero, _popUpDuration);
	}
}
