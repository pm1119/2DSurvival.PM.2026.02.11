using System.Collections.Generic;
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

    //확률 리스트(배열)가 주어졌을 때 확률에 따라 인덱스를 선택해 주는 함수
    /// <summary>
    /// 확률 리스트(배열)을 기준으로 인덱스를 하나 선택하는 함수
    /// </summary>
    /// <param name="probs">각 인덱스 선택 확률값 리스폰</param>
    /// <returns>선택된 인덱스</returns>
    public static int Choose(IReadOnlyList<float> probs)
    {
        //확률합 변수
        float total = 0;

        //확률합 계산
        foreach (var probsItem in probs)
        {
            if (probsItem > 0)
            {
                total += probsItem;
            }
        }

        //랜덤값 생성
        float randPoint = Random.value * total;

        //각 인덱스 확률 구간 확인
        for (int i = 0; i < probs.Count; i++)
        {
            //0 이하 확률은 스킵
            if (probs[i] <= 0) continue;

            if (randPoint < probs[i])
                return i;
            else
                randPoint -= probs[i];
        }

        return probs.Count - 1;
    }

    //리스트(배열)에서 요소를 하나 랜덤하게 선택하는 함수
    /// <summary>
    /// 리스트(배열)에서 요소를 하나 랜덤하게 선택하는 함수
    /// </summary>
    /// <typeparam name="T">리스트 요소의 타입</typeparam>
    /// <param name="list">리스트(배열)</param>
    /// <returns>선택된 요소. 리스트가 비어있으면 default 반환</returns>
    public static T ChooseRandom<T>(IReadOnlyList<T> list)
    {
        //null이거나 빈 리스트일 경우
        if (list == null || list.Count == 0)
            return default;

        int randomIndex = Random.Range(0, list.Count);
        return list[randomIndex];
    }

    //리스트(배열)의 요소 순서를 무작위로 섞는 함수(셔플 함수)
    /// <summary>
    /// 리스트(배열)의 요소 순서를 무작위로 섞는 함수
    /// </summary>
    /// <typeparam name="T">리스트 요소의 타입</typeparam>
    /// <param name="list"></param>
    public static void Shuffle<T>(IList<T> list)
    {
        //null이거나 빈 리스트인 경우
        if (list == null || list.Count == 0)
            return;

        //피셔(Fisher) - 에이츠(Yates) 셔플 알고리즘
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);

            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }

    //리스트(배열)에서 중복 없이 원하는 개수만큼 랜덤하게 선택하는 함수
}
