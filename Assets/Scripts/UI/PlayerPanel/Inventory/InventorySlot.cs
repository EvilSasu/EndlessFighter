using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public ItemEnums itemEnums;
    public ItemType itemType;

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0 && eventData != null)
        {
            if(itemEnums == ItemEnums.InventorySlot)
            {
                GameObject dropped = eventData.pointerDrag;
                if (dropped.GetComponent<DraggableItem>() != null)
                {
                    DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
                    draggableItem.parentAfterDrag = transform;
                }
            }
            else
            {
                CheckItemType(eventData);
            }
        }
    }

    private void CheckItemType(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped.GetComponent<DraggableItem>() != null)
        {
            if(dropped.GetComponent<Item>().type == itemType)
            {
                DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
                draggableItem.parentAfterDrag = transform;               
            }          
        }
    }
}
