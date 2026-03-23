using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    [Header("----- ÄÄÆ÷³ÍÆ® -----")]
    [SerializeField] Button _startButton;
    [SerializeField] Button _quitButton;

	private void Start()
	{
		HandleSceneChange();
	}

	public void HandleSceneChange()
    {
        _startButton.onClick.AddListener(OnPlay);
    }

	 void OnPlay()
	{
        SceneManager.LoadScene("Play");
	}
}
