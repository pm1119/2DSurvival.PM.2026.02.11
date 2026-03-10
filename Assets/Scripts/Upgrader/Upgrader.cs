using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 무기를 업그레이드하는 함수
/// </summary>
public class Upgrader : MonoBehaviour
{
    [Header("----- 컴포넌트 -----")]			
    [SerializeField] Weapon[] _weapons;             //무기 배열
	[SerializeField] UpgradeView[] _upgradeViews;   //업그레이드 뷰 배열
	[SerializeField] GameObject _upgradePanel;      //업그레이드 패널\

	int _upgradeCount = 0;							//업그레이드 카운트

	private void Awake()
	{
		foreach (var view in _upgradeViews) 
		{
			//업그레이드 완료 이벤트 구독
			view.OnUpgradeCompleted += EndSelection;
		}
	}

	//테스트용
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1) == true)
		{
			BeginSelection();
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2) == true)
		{
			EndSelection();
		}
		//else if(Input.GetKeyDown(KeyCode.Alpha3) == true)
		//{
		//	_weapons[2].Upgrade();
		//}
	}

	public void HandleLevelUp(int prelevel, int level)
	{
		if (level > prelevel)
		{
			_upgradeCount = level -	prelevel;
			BeginSelection();
		}
	}

	/// <summary>
	/// 업그레이드 선택을 시작하는 함수
	/// </summary>
	public void BeginSelection()
	{
		//1. 업그레이드 가능한 후보만 선별
		List<IUpgradable> list = new List<IUpgradable>(_weapons);

		//2. 업그레이드 가능한 후보군 셔플
		for (int i = list.Count - 1; i > 0; i--)
		{
			int randIndex = Random.Range(0, i + 1);
			IUpgradable temp = list[i];
			list[i] = list[randIndex];
			list[randIndex] = temp;
		}

		//3. 그 중 최대 3개만 UI에 표시

		for (int i = 0; i < _upgradeViews.Length; i++)
		{
			if (i < list.Count)
			{
				_upgradeViews[i].Initialize(list[i]);
				_upgradeViews[i].gameObject.SetActive(true);
			}
			else
			{
				_upgradeViews[i].gameObject.SetActive(false);
			}
		}

		//4. 업그레이드 패널 온, 게임 일시정지
		_upgradePanel.SetActive(true);
		Time.timeScale = 0;
	}

	/// <summary>
	/// 업그레이드 선택을 종료하는 함수
	/// </summary>
	public void EndSelection()
	{
		//남은 업그레이드가 있으면 다시 업그레이드 선택 시작
		if (_upgradeCount > 0)
		{
			BeginSelection();
		}
		else
		{
			_upgradePanel.SetActive(false);
			Time.timeScale = 1.0f;
		}
	}
}
