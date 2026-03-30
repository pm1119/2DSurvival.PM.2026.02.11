using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 타이틀 씬 관리
/// </summary>
public class TitleScene : MonoBehaviour
{
    [Header("----- 컴포넌트 -----")]
    [SerializeField] Button _startButton;
    [SerializeField] Button _quitButton;

	private void Awake()
	{
		_startButton.onClick.AddListener(LoadPlayChange);
		_quitButton.onClick.AddListener(QuitGame);
	}

	private void Start()
	{
		GameManager.Instance.DoSomething();
	}

	public void QuitGame()
	{
		//게임 종료
		Application.Quit();
	}

	 void LoadPlayChange()
	{
        SceneManager.LoadScene("Play");
		Time.timeScale = 1.0f;
	}
}
