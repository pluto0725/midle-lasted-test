using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;

    private EnemyPath thePath;          //몬스터가 가지고 있는 path 값
    private int currentPoint;           //지금 몇 번째 point를 향하고 있는지 변수
    private bool reacheEnd;             //도달 완료 체크 
        
    void Start()
    {
        if(thePath == null)
        {
            thePath = FindObjectOfType<EnemyPath>();    //모든 오브젝트를 검사해서 EnemyPath가 있는 컴포넌트를 가저온다.
        }
    }
   
    void Update()
    {
        if(reacheEnd == false)  // if(!reacheEnd) 도달 이전
        {
            transform.LookAt(thePath.points[currentPoint]); //몬스터는 지금 방향을 향해서 본다. (LookAt 함수)

            //MoveToward 함수 (내위치 , 타겟 위치, 속도값) 
            transform.position =
                Vector3.MoveTowards(transform.position, thePath.points[currentPoint].position, moveSpeed * Time.deltaTime);

            //Vector3.Distance (A,B) 벡터의 거리 => 거리가 0.01이하 일 경우 도착했다고 간주
            if (Vector3.Distance(transform.position , thePath.points[currentPoint].position) < 0.01f)    
            {
                currentPoint += 1;          //다음 포인트로 변경
                if(currentPoint >= thePath.points.Length)   //포인트 배열수보다 높을경우에는 도착 완료
                {
                    reacheEnd = true;
                }
            }
        }
    }
}
