using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 플레이어 캐릭터(주인공 캐릭터)를 고르는 역할
/// </summary>
public class HeroSelecter : MonoBehaviour
{
    [Header("----- 설정 데이터 -----")]
    [SerializeField] HeroData[] _heroDatas;					//히어로 데이터 배열

	[Header("----- 뷰 -----")]
	[SerializeField] GameObject _selectionPanel;			//히어로 선택 패널 게임오브젝트
	[SerializeField] HeroSelecterView[] _heroSelecterView;	//히어로 선택 배열

	public void Initialize()
	{
		for (int i = 0; i < _heroSelecterView.Length; i++)
		{
			if (i < _heroDatas.Length)
			{
				_heroSelecterView[i].OnHeroSelected += HandleHeroSelected;
				_heroSelecterView[i].Initialize(_heroDatas[i]);

				//해금 설정
				_heroSelecterView[i].SetUnlocked(GetIsHeroUnlocked(_heroDatas[i]));
			}
			else
			{
				_heroSelecterView[i].gameObject.SetActive(false);
			}
		}
	}

	/// <summary>
	/// 히어로가 해금되어 있는지 여부를 반환하는 함수
	/// </summary>
	/// <param name="heroData"></param>
	public bool GetIsHeroUnlocked(HeroData heroData)
	{
		if (heroData.DefaultUnlocked == true)
			return true;

		//도전과제 가져오기
		Challange challange = 
			GameManager.Instance.ChallangeManager.GetChallange(heroData.UnlockChallangeType);
		return challange.HasCleared;
	}

	/// <summary>
	/// 히어로 선택 패널
	/// </summary>
	public void OpenHeroSelectionPanel()
	{
		_selectionPanel.SetActive(true);
	}

	public void HandleHeroSelected(HeroData heroData)
	{
		//해금된 히어로가 맞는지 체크
		if (GetIsHeroUnlocked(heroData) == false) return;

		//현재 선택된 히어로 데이터를 교체
		GameManager.Instance.PlaySetting.SetSelectedHero(heroData);

		//Play 씬 로드 
		SceneManager.LoadScene("Play");
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
