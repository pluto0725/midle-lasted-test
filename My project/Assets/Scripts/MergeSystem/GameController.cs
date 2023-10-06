using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Slot[] slots;        //게임 컨트롤러에서는 Slot 배열을 관리

    private Vector3 _target;
    private ItemInfo carryingItem;      //잡고 있는 아이템 정보 값 관리

    //Slot id, Slot class 관리하기위한 자료구조 
    private Dictionary<int, Slot> slotDictionary; 

    private void Start()
    {
        slotDictionary = new Dictionary<int, Slot>(); //초기화

        for(int i = 0; i < slots.Length; i++) 
        {//각 슬롯의 ID를 설정하고 딕셔너리에 추가
            slots[i].id = i;
            slotDictionary.Add(i, slots[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) //마우스 누를 때
        {
            SendRayCast();
        }

        if (Input.GetMouseButton(0) && carryingItem)    //잡고 이동시킬 때
        {
            OnItemSelected();
        }

        if (Input.GetMouseButtonUp(0))  //마우스 버튼을 놓을게
        {
             SendRayCast();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlaceRandomItem();
        }
    }

    void SendRayCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            var slot = hit.transform.GetComponent<Slot>();  //RayCast를 통해 나온 Slot 칸
            if(slot.state == Slot.SLOTSTATE.FULL && carryingItem == null)
            {//선택한 슬롯에서 아이템을 잡는다.
                string itemPath = "Prefabs/Item_Graddbed_" + slot.itemObject.id.ToString("000");
                var itemGo = (GameObject)Instantiate(Resources.Load(itemPath));     //아이템 생성
                itemGo.transform.SetParent(this.transform);
                itemGo.transform.localPosition = Vector3.zero;
                itemGo.transform.localScale = Vector3.one * 2;

                carryingItem = itemGo.GetComponent<ItemInfo>();         //슬롯 정보 입력
                carryingItem.InitDummy(slot.id, slot.itemObject.id);

                slot.ItemGrabbed();
            }
            else if(slot.state == Slot.SLOTSTATE.EMPTY && carryingItem != null)
            {//빈 슬롯에 아이템을 배치
                slot.CreateItem(carryingItem.itemId);   //잡고 있는것 슬롯 위치에 생성
                Destroy(carryingItem.gameObject);      //잡고 있던것 파괴
            }
            else if(slot.state == Slot.SLOTSTATE.FULL && carryingItem != null)
            {//Checking 후 병합
                if(slot.itemObject.id == carryingItem.itemId)
                {
                    OnItemMergedWithTarget(slot.id);    //병합 함수 호출
                }
                else
                {
                    OnItemCarryFail();  //아이템 배치 실패
                }
            }
        }
        else
        {
            if (!carryingItem) return;
            OnItemCarryFail();  //아이템 배치 실패
        }
    }

    void OnItemMergedWithTarget(int targetSlotId)
    {//병합 함수
        var slot = GetSlotById(targetSlotId);
        Destroy(slot.itemObject.gameObject);            //slot에 있는 물체 파괴
        slot.CreateItem(carryingItem.itemId + 1);       //슬롯에 다음 번호 물체 생성
        Destroy(carryingItem.gameObject);               //잡고 있는 물체 파괴
    }

    void OnItemSelected()
    {   //아이템을 선택하고 마우스 위치로 이동 
        _target = Camera.main.ScreenToWorldPoint(Input.mousePosition);  //좌표변환
        _target.z = 0;
        var delta = 10 * Time.deltaTime;
        delta *= Vector3.Distance(transform.position, _target);
        carryingItem.transform.position = Vector3.MoveTowards(carryingItem.transform.position, _target, delta);
    }

   

    void OnItemCarryFail()
    {//아이템 배치 실패 시 실행
        var slot = GetSlotById(carryingItem.slotId);        //슬롯 위치 확인
        slot.CreateItem(carryingItem.itemId);               //해당 슬롯에 다시 생성
        Destroy(carryingItem.gameObject);                   //잡고 있는 물체 파괴
    }

    void PlaceRandomItem()
    {//랜덤한 슬롯에 아이템 배치
        if(AllSlotsOccupied())
        {
            return;
        }
        var rand = UnityEngine.Random.Range(0 , slots.Length); // 유니티 랜덤함수를 가져와서 0 ~ 배열 크기 사이 값
        var slot = GetSlotById(rand);
        while(slot.state == Slot.SLOTSTATE.FULL) 
        {
            rand = UnityEngine.Random.Range(0 , slots.Length);
            slot = GetSlotById(rand);
        }
        slot.CreateItem(0);
    }

    bool AllSlotsOccupied()
    {//모든 슬롯이 채워져 있는지 확인
        foreach(var slot in slots)              //foreach문을 통해서 Slots 배열을 검사후
        {
            if(slot.state == Slot.SLOTSTATE.EMPTY)  //비어있는지 확인
            {
                return false;
            }
        }
        return true;
    }

    Slot GetSlotById(int id)
    {//슬롯 ID로 딕셔너리에서 Slot 클래스를 리턴 
        return slotDictionary[id];
    }
}
