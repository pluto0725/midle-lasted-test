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
        intContainer = new GenericContainer<int>(10);                   //Int�� �����̳� ���� 10ĭ
        stringContainer = new GenericContainer<string>(15);         //string�� �����̳� ���� 15ĭ
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))                       //Ű���� 1��ư
        {
            intContainer.Add(Random.Range(0, 100));                 //0 ~ 100 ���� (int)
            DisplayContainerItems(intContainer);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))                       //Ű���� 2��ư
        {
            string randomString = " item " + Random.Range(0, 100);      //0 ~ 100 ���� (string)
            stringContainer.Add(randomString);                                      // ���ڿ� -> item
            DisplayContainerItems(stringContainer);
        }
    }
    private void DisplayContainerItems<T>(GenericContainer<T> container)
    {//������ �����̳ʸ� Debug.Log���� �� �� �ְ� ���� �Լ�
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
