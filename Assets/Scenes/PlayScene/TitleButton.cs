using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleButton : MonoBehaviour
{
    [Header("----- ÄÄÆ÷³ÍÆ® -----")]
    [SerializeField] Button _button;

	private void Start()
	{
		HandleSceneChange();
	}

	public void HandleSceneChange()
    {
        _button.onClick.AddListener(Title);
    }

    public void Title()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
