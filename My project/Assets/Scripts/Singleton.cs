using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour      //1개로 싱클톤 클래스 유지
{
    public static Singleton Instance { get; private set; } //싱글톤 인스턴스 전역에 생성

    private void Awake()                   //Awake 시점에서 인스턴스 검사
    {
        if(Instance == null)                 //인스터스가 없을경우
        {
            Instance = this;        //지금 인스턴스를 Static 에 입력
            DontDestroyOnLoad(gameObject); //DontDestroyOnLoad 파괴되지 않는 오브젝트로 설정
        }
        else
        {
            Destroy(gameObject);       //기존에 인스턴스가 있는 경우 파괴 시킨다.
        }
    }

    public int playerScore = 0;            //유저 스코어 int 선언

    public void IncreaseScore(int amount)     //유저 스코어 증가 함수 선언
    {
        playerScore += amount;               //더해준다.
    }
}
