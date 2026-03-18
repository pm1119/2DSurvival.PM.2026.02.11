using DG.Tweening;
using UnityEngine;

public class TweenPractice : MonoBehaviour
{
    Tween _tween;

    [SerializeField] float _duration;

	private void Update()
	{
        if (Input.GetKeyDown(KeyCode.Alpha1) == true)
        {
            PopUpTween();
        }    
        else if (Input.GetKeyDown(KeyCode.Alpha2) == true)
        {
            PopDownTween();
        }
	}

	public void PopUpTween()
    {
        _tween = transform
            .DOScale(new Vector3(1, 1, 1), 5)
            .SetEase(Ease.InElastic);
    }

    public void PopDownTween()
    {
		_tween = transform
			.DOScale(new Vector3(0, 0, 0), 5)
			.SetEase(Ease.InElastic);
	}
}
