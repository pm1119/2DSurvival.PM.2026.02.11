using UnityEngine;

/// <summary>
/// 주인공(플레이어)의 설정 데이터
/// </summary>
[CreateAssetMenu(menuName = "GameSettings/HeroData")]
public class HeroData : ScriptableObject
{
    [SerializeField] float _baseHp;                 //기본 체력
    [SerializeField] float _speed;                  //기본 이동 속력
    [SerializeField] float _baseExp;                //기본 경험치
    [SerializeField] float _expIncrementRate;       //경험치 증가 비율

    public float BaseHp => _baseHp;

    public float Speed => _speed;

    /// <summary>
    /// 레벨업에 필요한 경험치 양을 반환하는 함수
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public float GetExp(int level)
    {
        if (level <= 0)
            return _baseExp;
        return _baseExp * Mathf.Pow(_expIncrementRate, level);
    }
}
