using UnityEngine;

/// <summary>
/// 풀링된 게임오브젝트 담당
/// 플에서 가져온 게임오브젝트를 풀로 반환하는 기능
/// </summary>
public class Poolable : MonoBehaviour
{
    /// <summary>
    /// 자신 게임오브젝트가 생성된 풀
    /// </summary>
    Pool _pool;

    /// <summary>
    /// 풀을 설정하는 함수
    /// </summary>
    /// <param name="pool"></param>
    public void SetPool(Pool pool)
    {
        _pool = pool;
    }

    /// <summary>
    /// 풀로 되돌리는 함수
    /// </summary>
    public void ReturnToPool()
    {
        if (_pool != null)
        {
            _pool.Push(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
