using System.IO;
using UnityEngine;

/// <summary>
/// 세이브/로드 담당
/// </summary>
public class SaveManager : MonoBehaviour
{
    ChallangeManager _challangeManager;

    GameSaveData _gameSaveData = new GameSaveData();

    string _gameSavePath;

	private void Awake()
	{
		_gameSavePath = $"{Application.persistentDataPath}/save.json";
	}

	public void Initialize(ChallangeManager challangeManager)
    {
		_challangeManager = challangeManager;
    }

	public void Save()
	{
		//챌린지 매니저에서 세이브 필요한 데이터 가져오기
		_gameSaveData.ChallengeSaves = _challangeManager.GetSaveData();

		//세이브 데이터 객체를 Json 문자열로 변환
		string json = JsonUtility.ToJson(_gameSaveData);

		//텍스트 파일 생성
		File.WriteAllText(_gameSavePath, json);

		//폴더 열기
		Application.OpenURL(Application.persistentDataPath);
	}

	public void Load()
	{
		//세이브 파일 존재 여부 확인
		if (File.Exists(_gameSavePath) == false) return;

		//세이브 파일 텍스트 읽어 오기
		string json = File.ReadAllText(_gameSavePath);

		//Json 문자열 객체로 변환
		_gameSaveData = JsonUtility.FromJson<GameSaveData>(json);

		//불러온 세이브 데이터를 필요로 하는 게임의 각 객체들에 전달
		_challangeManager.LoadFrom(_gameSaveData.ChallengeSaves);
	} 
		
}
