using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericContainerExample : MonoBehaviour
{
    private GenericContainer<int> intContainer;
    private GenericContainer<string> stringContainer;
    // Start is called before the first frame update
    void Start()
    {
        intContainer = new GenericContainer<int>(10);                   //Int형 컨테이너 선언 10칸
        stringContainer = new GenericContainer<string>(15);         //string형 컨테이너 선언 15칸
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))                       //키보드 1버튼
        {
            intContainer.Add(Random.Range(0, 100));                 //0 ~ 100 랜덤 (int)
            DisplayContainerItems(intContainer);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))                       //키보드 2버튼
        {
            string randomString = " item " + Random.Range(0, 100);      //0 ~ 100 랜덤 (string)
            stringContainer.Add(randomString);                                      // 문자열 -> item
            DisplayContainerItems(stringContainer);
        }
    }
    private void DisplayContainerItems<T>(GenericContainer<T> container)
    {//생성한 컨테이너를 Debug.Log에서 볼 수 있게 만든 함수
        T[] items = container.GetItems();
        string temp = "";
        for(int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
            {
                temp += items[i].ToString() + " -  ";
            }
            else
            {
                temp += "Empty -";
            }
        }
        Debug.Log(temp);
    }
}
