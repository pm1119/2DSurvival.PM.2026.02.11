using UnityEngine;

public static class Util
{
    public const float Epsilon = 0.01f;

    /// <summary>
    /// 게임오브젝트가 오브젝트 풀링을 사용하면 풀로 되돌리고, 
    /// 풀링을 하지 않는 게임오브젝트면 파괴하는 함수
    /// </summary>
    /// <param name="go"></param>
    public static void DestroyOrReturnPool(GameObject go)
    {
        Poolable poolable = go.GetComponent<Poolable>();
        if (poolable != null)
        {
            poolable.ReturnToPool();
        }
        else
        {
            Object.Destroy(go);
        }
    }
}
