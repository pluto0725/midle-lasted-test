using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTower : MonoBehaviour
{
    private Tower thisTower;            //같은 오브젝트 안에있는 컨포넌트 Tower에 접근

    public GameObject projectile;           //발사체 프리팹
    public Transform firePoint;             //발사 위치 Transfrom 값
    public float timeBetweenShot = 1.0f;    //발사 시간 간격

    private float shotCounter;              //시간값 설정

    private Transform target;               //타겟 위치값
    public Transform launcherModel;         //런처 모델 Transfrom 값  

    // Start is called before the first frame update
    void Start()
    {
        thisTower = GetComponent<Tower>();
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)  //타겟이 있을 경우
        {
            launcherModel.rotation =
                Quaternion.Slerp(launcherModel.rotation,    //쿼터니언값
                Quaternion.LookRotation(target.position - transform.position), //추적 (벡터 빼기)
                5f * Time.deltaTime);

            launcherModel.rotation = Quaternion.Euler(
                0.0f,       // x
                launcherModel.rotation.eulerAngles.y,
                0.0f);  // z

            //적을 회전해서 바라보게 하는 구문
        }

        shotCounter -= Time.deltaTime;

        if(shotCounter <= 0.0f && target != null)
        {
            shotCounter = thisTower.fireRate;       //Tower에 있는 발사 시간을 가져와서 입력

            firePoint.LookAt(target);               //firePoint는 target을 향해 바라봄

            Instantiate(projectile, firePoint.position, firePoint.rotation);    //firePoint에 발사체 생성
        }

        if(thisTower.enemiesUpdate)
        {
            if(thisTower.enemiesInRange.Count > 0) 
            {
                float minDistance = thisTower.range + 1f;
                foreach(EnemyController enemy in thisTower.enemiesInRange)
                {
                    if(enemy != null)
                    {
                        float distance = Vector3.Distance(transform.position, enemy.transform.position);
                        
                        if(distance < minDistance)  //비교했을때 
                        {
                            minDistance  = distance; //비교했을 때 최소값일 경우 갱신
                            target = enemy.transform;
                        }
                    }
                }
            }
            else
            {
                target = null;  //거리상에 타겟이 없음
            }
        }
    }
}
