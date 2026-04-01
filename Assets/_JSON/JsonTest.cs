using System.IO;
using UnityEngine;

public class JsonTest : MonoBehaviour
{
	//저장하고 싶은 데이터 객체
    [SerializeField] PlayerData _playerData;

	//저장할 경로
	string _path;

	private void Awake()
	{
		//저장 경로 설정 
		_path = $"{Application.persistentDataPath}/test.json";
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1) == true)
		{
			Save();
		}
		if (Input.GetKeyDown(KeyCode.Alpha2) == true)
		{
			Load();
		}
	}

	public void Save()
    {
		//_playerData 변수가 가리키고 있는 객체를 
        //Json 문자열 형식으로 만들어 변환
		string json = JsonUtility.ToJson( _playerData );

		//저장할 경로에 텍스트 파일 생성
		File.WriteAllText( _path, json );

		//저장한 폴더 열기
		Application.OpenURL( _path );

        Debug.Log($"저장 완료\n{json}");
    }

	public void Load()
	{
		//세이브 파일 존재 여부 확인
		if(File.Exists(_path) == false) return;

		//Json 문자열 읽어오기
		string json = File.ReadAllText( _path );

		//Json 문자열을 객체로 변환
		_playerData = JsonUtility.FromJson<PlayerData>( json );
	}
}
