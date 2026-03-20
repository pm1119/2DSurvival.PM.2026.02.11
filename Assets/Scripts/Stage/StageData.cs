using UnityEngine;

/// <summary>
/// 스테이지 설정 데이터
/// </summary>
[CreateAssetMenu(menuName = "GameSettings/StageData")]
public class StageData : ScriptableObject
{
    [SerializeField] WaveData[] _waveData;              //시작 시간 순으로 정렬된 웨이브 데이터 배열
    [SerializeField] float _playTime;                   //전체 플레이 시간(초)
    
    public float PlayTime => _playTime;

    /// <summary>
    /// 현재 플레이한 시간에 해당하는 웨이브 데이터를 반환하는 함수
    /// </summary>
    /// <param name="time">현재 플레이 시간</param>
    /// <returns>웨이브 데이터</returns>
    public WaveData GetWaveData(float time)
    {
        for (int i = _waveData.Length - 1; i >= 0; i--)
        {
            if (time > _waveData[i].InitialTime)
            {
                return _waveData[i];
            }
        }
        return _waveData[0];
    }
}
