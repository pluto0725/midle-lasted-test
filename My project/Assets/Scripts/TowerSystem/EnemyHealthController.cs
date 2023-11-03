using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public int totalHealth = 50;            //체력 초기 선언 

    public void TakeDamage(int damageAmount)
    {
        totalHealth -= damageAmount;            //파라미터로 받은 수를 체력에서 소모

        if (totalHealth <= 0)                   //0이하이면 사망 처리 
        {
            totalHealth = 0;
            Destroy(gameObject);
        }
    }
}
