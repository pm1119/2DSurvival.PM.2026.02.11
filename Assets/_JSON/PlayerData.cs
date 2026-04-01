using UnityEngine;

/// <summary>
/// 가상의 게임 세이브 데이터
/// </summary>
[System.Serializable]
public class PlayerData 
{
    public string Name; 
    public int Level; 
    public float Hp;

    public Vector3 Position;

    public int[] Scores;
}
