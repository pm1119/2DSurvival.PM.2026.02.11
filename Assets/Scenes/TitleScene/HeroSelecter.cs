using UnityEngine;

/// <summary>
/// 플레이어 캐릭터(주인공 캐릭터)를 고르는 역할
/// </summary>
public class HeroSelecter : MonoBehaviour
{
    [Header("----- 설정 데이터 -----")]
    [SerializeField] HeroData[] _heroDatas;

	[Header("----- 뷰 -----")]
	[SerializeField]HeroSelecterView[] _heroSelecterView;

	public void Initialize()
	{
		for (int i = 0; i < _heroSelecterView.Length; i++)
		{
			if (i < _heroDatas.Length)
			{
				_heroSelecterView[i].Initialize(_heroDatas[i]);
			}
			else
			{
				_heroSelecterView[i].gameObject.SetActive(false);
			}
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1) == true)
		{
			GameManager.Instance.PlaySetting.SetSelectedHero(_heroDatas[0]);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2) == true)
		{
			GameManager.Instance.PlaySetting.SetSelectedHero(_heroDatas[1]);
		}
	}
}
