using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Extension 
{
    public static void DestroyOrReturnPool(this GameObject obj )
    {
        Util.DestroyOrReturnPool(obj);
    }

	/// <summary>
	/// 확률 리스트(배열)을 기준으로 인덱스를 하나 선택하는 함수
	/// </summary>
	/// <param name="probs">각 인덱스 선택 확률값 리스폰</param>
	/// <returns>선택된 인덱스</returns>
	public static int Choose(this IReadOnlyList<float> probs)
	{
		return Util.Choose(probs);
	}

	//리스트(배열)에서 요소를 하나 랜덤하게 선택하는 함수
	/// <summary>
	/// 리스트(배열)에서 요소를 하나 랜덤하게 선택하는 함수
	/// </summary>
	/// <typeparam name="T">리스트 요소의 타입</typeparam>
	/// <param name="list">리스트(배열)</param>
	/// <returns>선택된 요소. 리스트가 비어있으면 default 반환</returns>
	public static T ChooseRandom<T>(this IReadOnlyList<T> list)
	{
		return Util.ChooseRandom<T>(list);
	}

	//리스트(배열)의 요소 순서를 무작위로 섞는 함수(셔플 함수)
	/// <summary>
	/// 리스트(배열)의 요소 순서를 무작위로 섞는 함수
	/// </summary>
	/// <typeparam name="T">리스트 요소의 타입</typeparam>
	/// <param name="list"></param>
	public static void Shuffle<T>(this IList<T> list)
	{
		Util.Shuffle(list);
	}

	/// <summary>
	/// 리스트(배열)에서 중복 없이 원하는 개수만큼 랜덤하게 선택하여 리스트에 담아 반환하는 함수
	/// </summary>
	/// <typeparam name="T">리스트 요소와 타입</typeparam>
	/// <param name="list"></param>
	/// <param name="count">선택할 개수</param>
	/// <returns></returns>
	public static List<T> ChooseRandom<T>(this IReadOnlyList<T> list, int count)
	{
		return Util.ChooseRandom(list, count);
	}

	/// <summary>
	/// 게임오브젝트에 T 타입 컴포넌트가 있으면 그대로 반환하고.
	/// 없으면 추가하여 반환하는 함수
	/// </summary>
	/// <typeparam name="T">컴포넌트 타입</typeparam>
	/// <param name="go"></param>
	/// <returns>추가되었거나, 찾은 컴포넌트</returns>
	public static T GetOrAddComponent<T>(this GameObject go) where T : Component
	{
		return Util.GetOrAddComponent<T>(go);
	}
}
