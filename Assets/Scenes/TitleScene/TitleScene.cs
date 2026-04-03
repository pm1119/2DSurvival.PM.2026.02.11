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
	[SerializeField] Button _settingButton;
	[SerializeField] HeroSelecter _heroSelecter;
	[SerializeField] GameObject _settingPanel;
	[SerializeField] SettingView _settingView;

	private void Awake()
	{
		_startButton.onClick.AddListener(BeginHeroSelection);
		_quitButton.onClick.AddListener(QuitGame);
		_settingButton.onClick.AddListener(BeginSetting);
	}

	private void Start()
	{
		GameManager.Instance.DoSomething();

		//히어로 셀렉터 초기화
		_heroSelecter.Initialize();

		//타이틀 씬 배경음악 재생
		GameManager.Instance.SoundManager.PlayBgm(Bgm.Title);
	}

	public void BeginHeroSelection()
	{
		_heroSelecter.OpenHeroSelectionPanel();
	}

	public void BeginSetting()
	{
		_settingPanel.SetActive(true);
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
