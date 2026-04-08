using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LinqStudy : MonoBehaviour
{
    [SerializeField] List<int> _numbers;                //원본 리스트
    [SerializeField] List<int> _result;                 //결과 리스트

	[SerializeField] PlayerData[] _playerData;
	[SerializeField] List<PlayerData> _filteredPlayers;

	[SerializeField] Transform[] _targets;
	[SerializeField] Transform _nearestTarget;

	[ContextMenu("0. Sort(정렬)")]
	public void SortEx()
	{
		Debug.Log("------ 0. Sort -----");
		//리스트 복제
		_result = new List<int>(_numbers);

		//(오름차순)정렬
		//Sort(): 오름차순으로 정렬
		_result.Sort();

		_result.Sort(Compare);
	}

	//내림차순 비교 함수
	int Compare(int x, int y)
	{
		return x.CompareTo(y);
	}

	[ContextMenu("1. ToList(리스트로 반환)")]
	public void ToList()
	{
		Debug.Log("------ 1. ToList -----");
		_result = _numbers.ToList();

		int[] nums = { 3, 8, 13, 55, 64 };
		_result = nums.ToList();
	}

	[ContextMenu("2. Where(조건으로 필터링)")]
	public void Where()
	{
		//_numbers 리스트에서 3보다 큰 요소만 필터링하여 리스트로 반환
		_result = _numbers.Where(x => x > 3).ToList();
	}

	bool Test(int x)
	{
		return x > 3;
	}

	[ContextMenu("3. First, Last")]
	public void FirstLast()
	{
		int firstValue = _numbers.First();
		Debug.Log($"첫 번째 요소: {firstValue}");

		int firstValueOverHundered = _numbers.First(x => x > 100);
		Debug.Log($"100보다 큰 첫 번째 요소: {firstValueOverHundered}");

		int lastValue = _numbers.Last();
		Debug.Log($"마지막 요소: {lastValue}");
	}

	[ContextMenu("4. OrderBy(정렬)")]
	public void OrderBy()
	{
		_result = _numbers.OrderBy(x => x).ToList();
	}

	[ContextMenu("5. Count(개수)")]
	public void Count()
	{
		int count = _numbers.Count(x => x % 2 == 0);
		Debug.Log($"짝수의 개수: {count}");
	}

	[ContextMenu("6. 객체 리스트 활용")]
	public void FilterPlayers()
	{
		//레벨 10 이하 플레이어 리스트
		//이름 순으로 정렬
		_filteredPlayers = _playerData.Where(p => p.Level <= 10)
			.OrderBy(p => p.Name).ToList();

		//가장 레벨 높은 플레이어
		PlayerData playerData = _playerData.OrderByDescending(p => p.Level).First();
		Debug.Log($"최고 레벨 플레이어: {playerData.Name} 레벨: {playerData.Level}");
	}

	[ContextMenu("6. 객체 찾기")]
	public void FindNearestTarget()
	{
		//요소들을 가장 가까운 거리 오름차순으로 정렬한 뒤
		//첫 번째 요소 반환
		_nearestTarget
			= _targets.OrderBy(t => Vector3.Distance(transform.position, t.position))
			.FirstOrDefault();
	}
}
