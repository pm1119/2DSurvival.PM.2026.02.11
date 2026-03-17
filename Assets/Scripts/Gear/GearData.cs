using UnityEngine;

/// <summary>
/// 장비 설정 데이터
/// </summary>
[CreateAssetMenu(menuName = "GameSettings/GearData")]
public class GearData : ScriptableObject
{
    [SerializeField] string _gearName;      //장비 이름
    [SerializeField] float[] _levelValues;  //레벨별 스탯 배열
    [TextArea(3, 5)][SerializeField] string _desc;          //설명
    [SerializeField] Sprite _iconSprite;    //아이콘 스프라이트

    public string GearName => _gearName;

    public string Desc => _desc;

    public Sprite IconSprite => _iconSprite;

    public int MaxLevel => _levelValues.Length - 1;

    /// <summary>
    /// 레벨에 따른 보너스 수치를 반환하는 함수
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public float GetValue(int level)
    {
        if (level < 0)
            return 0;
        if (level > MaxLevel)
            level = MaxLevel;
        return _levelValues[level];
    }
}
