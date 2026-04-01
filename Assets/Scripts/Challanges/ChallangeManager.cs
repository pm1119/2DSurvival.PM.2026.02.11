using System.IO;
using UnityEngine;

/// <summary>
/// 도전 과제 관리자
/// </summary>
public class ChallangeManager : MonoBehaviour
{
    [Header("----- 도전 과제 -----")]
    [SerializeField] Challange[] _challanges;
    [SerializeField] ChallengeSave _challengeSave;

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

	public void Save()
	{
		//_challengeSave 변수가 가리키고 있는 객체를 
		//Json 문자열 형식으로 만들어 변환
		string json = JsonUtility.ToJson(_challengeSave);

		//저장할 경로에 텍스트 파일 생성
		File.WriteAllText(_path, json);

		//저장한 폴더 열기
		Application.OpenURL(_path);

		Debug.Log($"저장 완료\n{json}");
	}

	public void Load()
	{
		//세이브 파일 존재 여부 확인
		if (File.Exists(_path) == false) return;

		//Json 문자열 읽어오기
		string json = File.ReadAllText(_path);

		//Json 문자열을 객체로 변환
		_challengeSave = JsonUtility.FromJson<ChallengeSave>(json);
	}
}
