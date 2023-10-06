using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int id;
    public Slot parentSlot;

    public void Init(int id, Slot slot)
    {//아이템 정보값 입력하는 함수
        this.id = id;
        this.parentSlot = slot;
    }

}
