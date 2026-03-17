using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// PlayScene 상태 UI 표시(경험치 바, 레벨, 킬 수, 남은 시간)
/// </summary>
public class StatusView : MonoBehaviour
{
    [Header("----- 컴포넌트 -----")]
    [SerializeField] Image _expBar;                     //경험치 바
    [SerializeField] TMP_Text _levelText;               //레벨 텍스트
    [SerializeField] TMP_Text _killCount;               //킬 수 텍스트
    [SerializeField] TMP_Text _remainingTimeText;       //남은 시간 텍스트

    /// <summary>
    /// 경험치 UI를 갱신하는 함수
    /// </summary>
    /// <param name="currentExp"></param>
    /// <param name="maxExp"></param>
    public void UpdateExp(float currentExp, float maxExp)
    {
        _expBar.fillAmount = currentExp / maxExp;
    }

    /// <summary>
    /// 레벨 UI를 갱신하는 함수
    /// </summary>
    /// <param name="level"></param>
    public void UpdateLevel(int level)
    {
        _levelText.text = (level + 1).ToString();
    }

    public void UpdateKillCount(int killCount)
    {
        _killCount.text = killCount.ToString();
    }

    /// <summary>
    /// 남은 시간 UI를 갱신하는 함수
    /// </summary>
    /// <param name="time"></param>
    public void UpdateRemainingTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        _remainingTimeText.text = $"{minutes}:{seconds:00}";
    }
}