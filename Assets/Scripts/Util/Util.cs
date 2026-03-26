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
    /// <summary>
    /// 리스트(배열)에서 중복 없이 원하는 개수만큼 랜덤하게 선택하여 리스트에 담아 반환하는 함수
    /// </summary>
    /// <typeparam name="T">리스트 요소와 타입</typeparam>
    /// <param name="list"></param>
    /// <param name="count">선택할 개수</param>
    /// <returns></returns>
    public static List<T> ChooseRandom<T>(IReadOnlyList<T> list, int count)
    {
        //예외 처리
        if (list == null || list.Count == 0 || count <= 0)
        {
            return new List<T>();
        }

		//count가 원본 리스트의 요소 수보다 크거나 같으면 원본 리스트 복제본 전체 반환
		if (count >= list.Count)
        {
            return new List<T>(list);
        }

        //원본 이스트 보호용 복제본 리스트
        List<T> temp = new List<T>(list);

        //복제본 리스트 셔플
        Shuffle(temp);

        //앞에서부터 count개만 선택해서 리스트로 반환
        //(0번부터 count개만 선택해서 리스트 반환)
        return temp.GetRange(0, count);
    }

    /// <summary>
    /// 게임오브젝트에 T 타입 컴포넌트가 있으면 그대로 반환하고.
    /// 없으면 추가하여 반환하는 함수
    /// </summary>
    /// <typeparam name="T">컴포넌트 타입</typeparam>
    /// <param name="go"></param>
    /// <returns>추가되었거나, 찾은 컴포넌트</returns>
    public static T GetOrAddComponent<T>(GameObject go) where T : Component
    {
        //게임오브젝트에서 T 타입인 컴포넌트 가져오기
        T component = go.GetComponent<T>();

        //게임오브젝트 T 타입인 컴포넌트가 없었을 경우
        if (component == null)
        {
            component = go.AddComponent<T>();
        }
        return component;
    }

	/// <summary>
	/// 랜덤한 Vector2 방향 벡터를 반환하는 함수
	/// </summary>
	/// <returns></returns>
	public static Vector2 GetRandomDirection2D()
    {
        float angle = Random.Range(0.0f, Mathf.PI * 2);
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }
}
