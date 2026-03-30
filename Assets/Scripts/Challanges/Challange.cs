using UnityEngine;

public enum ChallangeType
{
    TotalKillCount,
    ClearCount,
    MoveCount
}

/// <summary>
/// 도전 과제(업적)
/// </summary>
[System.Serializable]
public class Challange 
{
    [SerializeField] ChallangeType _type;           //도전 과제 종류
    [SerializeField] int _target;                   //목표 수치
    [SerializeField] int _count;                    //현재 수치
    [SerializeField] bool _hasCleared;              //클리어 여부

    public ChallangeType Type => _type;

    public int Target => _target;

    public int Count => _count;

    public bool HasCleared => _hasCleared;

    public void AddCount(int count = 1)
    {
        _count += count;

        //최초로 도전과제를 클리어했을 경우
        if (_count >= _target && _hasCleared == false)
        {
            _hasCleared = true;
            Debug.Log($"{_type} 도전 과제 달성");
        }
    }
}
