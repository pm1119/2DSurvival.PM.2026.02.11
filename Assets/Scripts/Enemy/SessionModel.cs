using UnityEngine;

public class SessionModel : MonoBehaviour
{
    [Header("----- 런타임 데이터 -----")]
    [SerializeField] int _killCount;

    public void AddKillCount()
    {
        _killCount++;
    }
}
