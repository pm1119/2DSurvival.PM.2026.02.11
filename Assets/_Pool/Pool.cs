using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임오브젝트 풀 
/// 게임오브젝트들을 미리 생성해두었다가 게임에서 필요할 때 건네주고, 
/// 게임에서 필요없어졌을 때 다시 돌려받는 역할
/// </summary>
public class Pool 
{
    Stack<GameObject> _pool;        //복제본 게임오브젝트 스택
    GameObject _prefab;             //풀링할 원본 프리팹
    Transform _parent;              //풀링 게임오브젝트들의 부모 트랜스폼

    /// <summary>
    /// Pool 생성자
    /// </summary>
    /// <param name="prefab">풀링할 프리팹</param>
    /// <param name="transform">풀의 부모 트랜스폼</param>
    /// <param name="initialSize">초기 풀 크기</param>
    public Pool(GameObject prefab, Transform transform, int initialSize)
    {
        _prefab = prefab;
        _parent = transform;
        _pool = new Stack<GameObject>(10);

        for(int i = 0; i < initialSize; i++)
        {
            CreatePoolObj();
        }
    }

    /// <summary>
    /// 풀에 새 복제본 게임오브젝트를 추가하는 함수
    /// </summary>
    public void CreatePoolObj()
    {
        //1. 원본 프리팹 복제 후 복제본 게임오브젝트 생성
        GameObject go = Object.Instantiate(_prefab);

        //2. 복제본 게임오브젝트의 부모를 풀의 부모로 설정
        go.transform.SetParent(_parent);

        //3. 복제본 게임오브젝트 비활성화
        go.SetActive(false);

        //4. 복제본 게임오브젝트에서 Poolable 컴포넌트 가져오기
        Poolable poolable = go.GetOrAddComponent<Poolable>();
        //Poolable poolable = go.GetComponent<Poolable>();

        ////5. Poolable 컴포넌트가 없었다면 새로 추가
        //if (poolable == null)
        //{
        //    poolable = go.AddComponent<Poolable>();
        //}

		//6. Poolable 컴포넌트에 풀 등록
        poolable.SetPool(this);

        //7. 복제본 게임오브젝트를 스택에 추가
        _pool.Push(go);
	}

    /// <summary>
    /// 풀에서 게임오브젝트를 하나 꺼내오는 함수
    /// 풀이 비었으면 새로 생성해 반환한다.
    /// </summary>
    /// <returns></returns>
    public GameObject Pop()
    {
        //풀에 남은 게임오브젝트가 있는 경우
        if (_pool.Count > 0)
        {
            GameObject go = _pool.Pop();
            go.SetActive(true);
            return go;
        }

        //풀에 남은 게임오브젝트가 없는 경우
        //새로 생성해 반환
        GameObject newGo = Object.Instantiate(_prefab);

        Poolable poolable = newGo.GetOrAddComponent<Poolable>();
        //Poolable poolable = newGo.GetComponent<Poolable>();
        //if (poolable == null)
        //{
        //    poolable = newGo.AddComponent<Poolable>();
        //}
        poolable.SetPool(this);
        return newGo;
    }

    /// <summary>
    /// 게임오브젝트를 풀로 되돌리는 함수
    /// </summary>
    /// <param name="go"></param>
    public void Push(GameObject go)
    {
        go.transform.SetParent(_parent);
        go.SetActive(false);
        _pool.Push(go);
    }
}
