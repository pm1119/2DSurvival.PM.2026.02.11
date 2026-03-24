using UnityEngine;

public static class Extension 
{
    public static void DestroyOrReturnPool(this GameObject obj )
    {
        Util.DestroyOrReturnPool(obj);
    }
}
