using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    public Camera mainCamera;
    public Transform parentWhileDragging;
    public Transform parentAfterDrag;

    public GameObject itemInfoPanel;
    private PlayerData playerData;

    private void Awake()
    {
        playerData = GameObject.FindGameObjectWithTag("PlayerData").GetComponent<PlayerData>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(parentWhileDragging);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
        itemInfoPanel.GetComponent<ItemPanel>().SetupPanel(GetComponent<Item>());
        itemInfoPanel.SetActive(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        itemInfoPanel.transform.position = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
        itemInfoPanel.SetActive(false);
        ItemsInListsUpdate();
    }

    private void ItemsInListsUpdate()
    {
        if (parentAfterDrag.GetComponent<InventorySlot>() != null)
        {
            InventorySlot slot = parentAfterDrag.GetComponent<InventorySlot>();
            Item itemComponent = GetComponent<Item>();
            if (slot.itemEnums == ItemEnums.InventorySlot)
            {
                if (playerData.itemsInEQ.Exists(itemData => itemData.itemRef == itemComponent))
                {
                    int index = playerData.itemsInEQ.FindIndex(itemData => itemData.itemRef == itemComponent);

                    if (index != -1)
                    {
                        playerData.UpdateItemsInEQList(index);
                    }
                }
            }
            else
            {
                if (playerData.itemsInInventory.Exists(itemData => itemData.itemRef == itemComponent))
                {
                    int index = playerData.itemsInInventory.FindIndex(itemData => itemData.itemRef == itemComponent);

                    if (index != -1)
                    {
                        playerData.UpdateItemsInInventoryList(index);
                    }
                }
            }
        }
    }
}
