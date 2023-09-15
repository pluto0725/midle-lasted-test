using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Singleton.Instance.IncreaseScore(10);
        GameManager.Instance.IncreaseScore(15);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
