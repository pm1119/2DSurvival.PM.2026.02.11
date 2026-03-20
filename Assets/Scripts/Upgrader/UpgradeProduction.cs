using DG.Tweening;
using UnityEngine;

public class UpgradeProduction : MonoBehaviour
{
    [Header("----- ÄÄÆ÷³ÍÆ® -----")]
	[SerializeField] RectTransform _rectTransform;
	[SerializeField] float _popUpDuration;

	Vector3 _upgradeOriginScale;
	Vector3 _upgradeHighlightScale = new Vector3(1, 1, 1);	

	//Tween _popUpTween;

	Sequence _upgradeSequence;

	private void Awake()
	{
		_upgradeOriginScale = _rectTransform.localScale;
	}

	public void PopUp()
	{
		_rectTransform.localScale = _upgradeOriginScale;

		_upgradeSequence?.Kill();

		_upgradeSequence = DOTween.Sequence();
        _upgradeSequence.SetUpdate(true);

        _upgradeSequence.Append(_rectTransform.DOScale(_upgradeHighlightScale, _popUpDuration));
	}

	public void PopDown()
	{
		_rectTransform.localScale = _upgradeHighlightScale;

		_upgradeSequence?.Kill();

		_upgradeSequence = DOTween.Sequence();

        _upgradeSequence.Append(_rectTransform.DOScale(_upgradeOriginScale, _popUpDuration));
    }
}
