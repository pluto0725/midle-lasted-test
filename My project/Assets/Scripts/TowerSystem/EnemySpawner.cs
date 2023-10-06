using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyController[] enemiesToSpawn;            //적 생성 배열
    public Transform spawnPoint;        //스폰 위치 설정
    public float timeBetweenSpawns = 0.5f;     //스폰 <- 시간 -> 스폰
    public float spawnCounter;                  //시간을 흐르게해서 스폰되는 시간 설정
    public int amountToSpawn = 15;              //스폰 그룹 숫자 
   
    void Start()
    {
        spawnCounter = timeBetweenSpawns;           //Counter에 시간을 입력
    }
   
    void Update()
    {
        if(amountToSpawn > 0)                   //소환할 몬스터 그룹이 남아있으면
        {
            spawnCounter -= Time.deltaTime;     //timeBetweenSpawns설정한 시간에서 -> 0.0초로 간다.
            if(spawnCounter <= 0 )
            {
                spawnCounter = timeBetweenSpawns; //0.0초 이하인 카운터에 다시 설정한 시간을 입력한다. 
                //enemiesToSpawn 배열에서 랜덤값으로 몬스터 생성
                Instantiate(enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)], spawnPoint.position, spawnPoint.rotation);
                amountToSpawn -= 1;         //소환후에 숫자를 1 빼준다.
            }
        }
    }
}
