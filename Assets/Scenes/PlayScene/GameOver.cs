using UnityEngine;

public class GameOver : MonoBehaviour
{
    [Header("----- ÄÄÆ÷³ÍÆ® -----")]
    [SerializeField] GameObject _deadPanel;

    public void GameOverPanel()
    {
        _deadPanel.SetActive(true);
		Time.timeScale = 0;
	}
}
