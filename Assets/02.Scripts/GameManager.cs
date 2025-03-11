using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public Transform[] points;
    public List<Transform> points = new List<Transform>();
    public GameObject monster;
    public float createTime = 3.0f;
    private bool isGameOver;
    public bool IsGameOver
    {
        get{return isGameOver;}
        set
        {
            isGameOver = value;
            if(isGameOver)
            {
                CancelInvoke("CreateMonster");
            }
        }
    }
    #region  Singleton
    public static GameManager instance = null;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion
    void Start()
    {
        Transform spawnPointGroup = GameObject.Find("SpawnPointGroup")?.transform;
        //points = spawnPointGroup?.GetComponentsInChildren<Transform>();
        //spawnPointGroup?.GetComponentsInChildren<Transform>(points);
        foreach (Transform point in spawnPointGroup)
        {
            points.Add(point);
        }
        InvokeRepeating("CreateMonster", 2.0f, createTime);
    }
    private void CreateMonster()
    {
        // 몬스터의 불규칙한 생성 위치 선출
        int idx = Random.Range(0, points.Count);
        // 몬스터의 프리펩 생성
        Instantiate(monster, points[idx].position, points[idx].rotation);
    }
}
