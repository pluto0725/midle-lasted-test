using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float range = 3.0f;              //타워 사거리
    public float fireRate = 1.0f;           //타워 발사 간격   
    public LayerMask IsEnemy;               //레이어 시스템으로 감지

    public Collider[] colliderInRange;      //사거리에 감지된 COllider 배열
    
    public List<EnemyController> enemiesInRange = new List<EnemyController>();  //사거리 안에 있는 Enemy 컴포넌트 List

    public float checkCounter;              //시간 체크용 float 
    public float checkTime = 0.2f;          //0.2로 마다 검출

    public bool enemiesUpdate;              //flag 값으로 체크 완료했는지 검출


    // Start is called before the first frame update
    void Start()
    {
        checkCounter = checkTime;                   //지정한 시간을 CheckCounte 입력
    }

    // Update is called once per frame
    void Update()
    {
        enemiesUpdate = false;

        checkCounter -= Time.deltaTime;             //0.2 -> 0초가 될때까지 시간을 감소

        if(checkCounter <= 0)                       //0초 이하가 되었을 때 
        {
            checkCounter = checkTime;               //0.2초로 다시 변경

            colliderInRange = Physics.OverlapSphere(transform.position, range, IsEnemy);    //자신의 위치, 범위값, 레이값을 통해서 Collider 취합

            enemiesInRange.Clear();     //List 초기화 (기존에 사용했을수도 있기때문에)

            foreach (Collider col in colliderInRange)
            {
                enemiesInRange.Add(col.GetComponent<EnemyController>());        //Collider 배열에 있는 컴포넌트를 List에 넣는다. 
            }

            enemiesUpdate = true;           
        }
    }
}
