using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using TMPro;
using Mono.Cecil.Cil;
using UnityEditor;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //public Transform[] points;
    public const int MAX_SCORE = 99999;
    public const string KEY_SCORE = "TOT_SCORE";
    public List<Transform> points = new List<Transform>();
    // 몬스터를 미리 생성하고 저장할 리스트
    public List<GameObject> monsterPool = new List<GameObject>();
    // 오브젝트풀에 생성할 몬스터의 최대 개수
    public int maxMonsters = 10; 
    public GameObject monster;
    public float createTime = 3.0f;
    public TMP_Text scoreText;
    public GameObject panelGameOver;
    public TMP_Text killText;
    private bool isGameOver;
    private int totScore = 0;
    private int killcount;
    public int KillCount
    {
        get {return killcount;}
        set {killcount = value;
            if(killcount > 99)
            {
                killcount = 99;
            }
            DisplayKillCount();
        }
    }
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
        panelGameOver.SetActive(false);
        // 몬스터 오브젝트 풀 생성
        CreateMonsterPool();
        Transform spawnPointGroup = GameObject.Find("SpawnPointGroup")?.transform;
        //points = spawnPointGroup?.GetComponentsInChildren<Transform>();
        //spawnPointGroup?.GetComponentsInChildren<Transform>(points);
        foreach (Transform point in spawnPointGroup)
        {
            points.Add(point);
        }
        // 일정한 간격으로 함수 호출
        InvokeRepeating("CreateMonster", 2.0f, createTime);
        // 점수 출력
        totScore = PlayerPrefs.GetInt(KEY_SCORE, 0);
        DisplayerScore(0);
    }
    private void CreateMonster()
    {
        // 몬스터의 불규칙한 생성 위치 선출
        int idx = Random.Range(0, points.Count);
        // 몬스터의 프리펩 생성
        //Instantiate(monster, points[idx].position, points[idx].rotation);
        // 오브젝트 풀에서 몬스터 추출
        GameObject _monster = GetMonsterInPool();
        // 추출한 몬스터의 위치와 회전을 설정
        _monster?.transform.SetPositionAndRotation(points[idx].position, points[idx].rotation);
        _monster?.SetActive(true);
    }
    private void CreateMonsterPool()
    {
        for(int i = 0; i < maxMonsters; i++)
        {   
            var _monster = Instantiate<GameObject>(monster);
            _monster.name = $"Monster_{i:00}";
            _monster.SetActive(false);
            monsterPool.Add(_monster);
        }
    }
    public GameObject GetMonsterInPool()
    {
        // 오브젝트 풀의 처음부터 끝까지 순회
        foreach (var _monster in monsterPool)
        {
            // 비활성화 여부로 사용 가능한 몬스터인지 판단
            if( _monster.activeSelf == false)
            {
                return _monster;
            }
        }
        return null;
    }
    public void DisplayerScore(int score)
    {
        totScore += score;
        if(totScore > MAX_SCORE)
        {
            totScore = MAX_SCORE;
        }
        scoreText.text = $"<color=#00ff00>SCORE: </color><color=#ff0000>{totScore:#,##0}</color>";
        PlayerPrefs.SetInt(KEY_SCORE, totScore);
    }
    public void DisplayKillCount()
    {
        killText.text = $"{killcount:00}";
    }
    [MenuItem("MyMenu/SpaceShooter/Reset score")]
    public static void ResetScore()
    {
        PlayerPrefs.SetInt(KEY_SCORE, 0);
        Debug.Log("Successfully reset score.");
    }
    public void DisplayerGameOver()
    {
        panelGameOver.SetActive(true);
    }
}

