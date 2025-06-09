using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton<T> : MonoBehaviour where T : Component
{
    bool isInitialize = false;
    static bool isShutdown = false;
    static T instance = null;
    public static T Instance
    {
        get
        {
            if(isShutdown)  // 종료처리에 들어간 상황이면
            {
                Debug.LogWarning("싱글톤이 삭제 중에 요구받음.");     // 경고 출력
                return null;                                        // null 리턴
            }

            if(instance == null)
            {
                T singleton = FindAnyObjectByType<T>();     // 일단 씬에 싱글톤이 있는지 찾음
                if(singleton == null )
                {
                    // 씬에 싱글톤이 없음
                    GameObject obj = new GameObject();      // Transform 하나만 들어있는 빈 게임 오브젝트 만들기
                    obj.name = $"{typeof(T)}_Singleton";    // 싱글톤 게임 오브젝트의 이름 지정하기
                    singleton = obj.AddComponent<T>();      // 싱글톤 게임 오브젝트에 싱글톤 용 컴포넌트 추가
                }
                instance = singleton;       // 찾은 것이나 만든 것을 저장하기
                DontDestroyOnLoad(instance.gameObject);     // 씬이 닫히더라도 싱글톤용 게임오브젝트는 유지하기(삭제되지 않게 하기)
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            // 첫번째로 만들어진 싱글톤
            instance = this as T;       // this를 T타입으로 캐스팅. 캐스팅이 안되면 null
            DontDestroyOnLoad(instance.gameObject);
        }
        else
        {
            // 이미 만들어진 싱글톤이 있는 상황
            if (instance != this)           // 이미 만들어진 것이 자신이 아니면
            { 
                Destroy(this.gameObject);   // 스스로 없어지기
            }
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;      // 씬이 로딩되었을 때 OnSceneLoaded함수를 실행하도록 등록
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;      // 등록 해제
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(!isInitialize)
        {
            // 한번도 초기화 되지 않았으면
            OnPreInitialize();      // 한번만 실행하는 초기화 함수 실행
        }
        if(mode != LoadSceneMode.Additive)
        {
            // Additive모드로 씬이 로딩되지 않았다면
            OnInitialize();         // 반복 초기화 함수 실행
        }
    }

    /// <summary>
    /// 싱글톤이 만들어졌을 때 단 한번만 호출되는 함수
    /// </summary>
    protected virtual void OnPreInitialize()
    {
        isInitialize = true;
    }

    /// <summary>
    /// 싱글톤이 만들어지고 씬이 변경될 때마다 호출될 함수(Additive는 안됨)(빈함수)
    /// </summary>
    protected virtual void OnInitialize()
    {
    }

    private void OnApplicationQuit()
    {
        isShutdown = true;
    }
}