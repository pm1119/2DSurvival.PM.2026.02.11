using System.IO;
using UnityEngine;

/// <summary>
/// 도전 과제 관리자
/// </summary>
public class ChallangeManager : MonoBehaviour
{
    [Header("----- 도전 과제 -----")]
    [SerializeField] Challange[] _challanges;

    string _path;

	private void Awake()
	{
		_path = $"{Application.persistentDataPath}/challange.json";
	}

	/// <summary>
	/// 도전 과제 카운트를 증가시키는 함수
	/// </summary>
	/// <param name="challangeType">도전 과제 종류</param>
	/// <param name="count">카운트</param>
	public void AddChallangeCount(ChallangeType challangeType, int count = 1)
    {
        int index = (int)challangeType;
        if (index < 0 || index >= _challanges.Length)
        {
            Debug.Log($"존재하지 않는 도전과제입니다. Type: {challangeType}");
            return;
        }

		//challangeType에 맞는 도전 과제 객체 가져오기
		Challange challange = _challanges[index];

        challange.AddCount(count);
    }

    /// <summary>
    /// 세이브 데이터를 만들어 반환하는 함수
    /// </summary>
    /// <returns></returns>
    public ChallengeSave[] GetSaveData()
    {
        ChallengeSave[] challengeSaves = new ChallengeSave[_challanges.Length];
        for (int i = 0; i < _challanges.Length; i++)
        {
            challengeSaves[i] = _challanges[i].GetSaveData();
        }
        return challengeSaves;
    }

    public void LoadFrom(ChallengeSave[] challengeSaves)
    {
        if (challengeSaves == null || challengeSaves.Length == 0) return;

        int count = Mathf.Min(_challanges.Length, challengeSaves.Length);
        for (int i = 0; i < count; i++)
        {
            _challanges[i].LoadFrom(challengeSaves[i]);
        }
    }
}
