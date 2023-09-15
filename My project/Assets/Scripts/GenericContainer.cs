using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericContainer <T>   //���ʸ� ���� Class�� ����ϱ� ���ؼ� ����
{
    private T[] items;                          //item �迭�� ����
    private int currentIndex = 0;       //item �ε���

    public GenericContainer(int capacity)           //class���� class�̸��� ���� �Լ��� ���� ������
    {
        items = new T[capacity];
    }

    public void Add(T item)                                 //�� �����̳� ���� �ִ´�
    {
        if (currentIndex < items.Length)                //�����̳� �迭 ĭ �̻� �ɰ�� ���´�.
        {
            items[currentIndex] = item;                 //���� ���� �迭�� �ִ´�
            currentIndex++;                                 //�ε����� ������Ų��
        }
    }
    public T[] GetItems()                                   //�迭�� �ִ� ���� ����
    {
        return items;
    }
}
