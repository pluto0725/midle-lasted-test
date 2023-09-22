using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public enum SLOTSTATE       //슬롯 상태값
    {
        EMPTY,
        FULL
    }
    public int id;                            //슬롯 번호 ID
    public Item itemObject;                   //선언한 커스텀 Class Item
    public SLOTSTATE state = SLOTSTATE.EMPTY; //Enum 값 선언
    private void ChangeStateTo(SLOTSTATE targetState)
    {//해당 슬롯의 상태값을 변환 시켜주는 함수
        state = targetState;
    }
    public void ItemGrabbed()
    {//RayCast를 통해서 아이템을 잡았을 때 
        Destroy(itemObject.gameObject); //기존 아이템을 삭제
        ChangeStateTo(SLOTSTATE.EMPTY); //슬롯은 빈 상태
    }
    public void CreateItem(int id)
    {
        //아이템 경로는 (Resources/Prefabs/Item_000)
        string itemPath = "Prefabs/Item_" + id.ToString("000");
        var itemGo = (GameObject)Instantiate(Resources.Load(itemPath));
        itemGo.transform.SetParent(this.transform);
        itemGo.transform.localPosition = Vector3.zero;
        itemGo.transform.localScale = Vector3.one;
        //아이템 값 정보를 입력
        itemObject = itemGo.GetComponent<Item>();
        itemObject.Init(id, this);  //함수를 통한 값 입력(this -> Slot Class)
        ChangeStateTo(SLOTSTATE.FULL); //슬롯 찬 상태
    }
}
