using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventorySetup : MonoBehaviour
{
    public PlayerData playerData;
    public InventoryType inventoryType;
    public GameObject itemPrefab;

    public Camera mainCamera;
    public GameObject itemInfoPanel;
    public Transform parentWhileDragging;
    public enum InventoryType
    {
        EQ,
        Inventory
    }

    public List<ItemDataSS> itemList;
    public List<InventorySlot> inventorySlots;
    public List<Sprite> itemSprites;

    public int indexOfInventorySlots = 0;
    private void Start()
    {
        itemList = new List<ItemDataSS>();
        GetAllItemSlotsInInventory();
        itemSprites.AddRange(playerData.itemsSprites);
        if (inventoryType == InventoryType.Inventory)
        {
            itemList.AddRange(playerData.itemsInInventory);
            SetupInventoryItems();
        }
        else
        {
            itemList.AddRange(playerData.itemsInEQ);
            SetupEQItems();
        }
            
    }

    private void GetAllItemSlotsInInventory()
    {
        inventorySlots = new List<InventorySlot>();
        foreach (Transform child in transform)
        {
            inventorySlots.Add(child.GetComponent<InventorySlot>());
        }
    }

    private void SetupInventoryItems()
    {    
        foreach (ItemDataSS item in itemList)
        {
            GameObject newItem = Instantiate(itemPrefab, itemPrefab.transform.position, Quaternion.identity,
                                            inventorySlots[indexOfInventorySlots].transform);
            indexOfInventorySlots++;
            int indexOfSprite = item.itemImageId;
            newItem.GetComponent<Image>().sprite = itemSprites[indexOfSprite];
            newItem.GetComponent<DraggableItem>().mainCamera = mainCamera;
            newItem.GetComponent<DraggableItem>().itemInfoPanel = itemInfoPanel;
            newItem.GetComponent<DraggableItem>().parentWhileDragging = parentWhileDragging;
            SetupItemInformation(newItem.GetComponent<Item>(), item);
        }
    }
    private void SetupEQItems()
    {
        foreach (ItemDataSS item in itemList)
        {
            GameObject newItem = new GameObject();
            switch (item.type)
            {
                case ItemType.Boots:
                    newItem = Instantiate(itemPrefab, itemPrefab.transform.position, Quaternion.identity,
                                            inventorySlots[0].transform); break;
                case ItemType.OneHandWeapon:
                    newItem = Instantiate(itemPrefab, itemPrefab.transform.position, Quaternion.identity,
                                           inventorySlots[1].transform); break;
                case ItemType.TwoHandWeapon:
                    newItem = Instantiate(itemPrefab, itemPrefab.transform.position, Quaternion.identity,
                                           inventorySlots[1].transform); break;
                case ItemType.Helmet:
                    newItem = Instantiate(itemPrefab, itemPrefab.transform.position, Quaternion.identity,
                                           inventorySlots[2].transform); break;
                case ItemType.Necklace:
                    newItem = Instantiate(itemPrefab, itemPrefab.transform.position, Quaternion.identity,
                                           inventorySlots[3].transform); break;
                case ItemType.Shield:
                    newItem = Instantiate(itemPrefab, itemPrefab.transform.position, Quaternion.identity,
                                           inventorySlots[4].transform); break;
                case ItemType.Armor:
                    newItem = Instantiate(itemPrefab, itemPrefab.transform.position, Quaternion.identity,
                                           inventorySlots[5].transform); break;
            }

            int indexOfSprite = item.itemImageId;
            newItem.GetComponent<Image>().sprite = itemSprites[indexOfSprite];
            newItem.GetComponent<DraggableItem>().mainCamera = mainCamera;
            newItem.GetComponent<DraggableItem>().itemInfoPanel = itemInfoPanel;
            newItem.GetComponent<DraggableItem>().parentWhileDragging = parentWhileDragging;
            SetupItemInformation(newItem.GetComponent<Item>(), item);
        }
    }

    private void SetupItemInformation(Item newObj, ItemDataSS itemFromList)
    {
        newObj.itemName = itemFromList.itemName;
        newObj.type = itemFromList.type;
        newObj.rarity = itemFromList.rarity;
        newObj.minDamage = itemFromList.minDamage;
        newObj.maxDamage = itemFromList.maxDamage;
        newObj.damageType = itemFromList.damageType;
        newObj.physicalDefense = itemFromList.physicalDefense;
        newObj.magicDefense = itemFromList.magicDefense;
        newObj.rangeDefense = itemFromList.rangeDefense;
        newObj.weaponBonuses = itemFromList.weaponBonuses;
        newObj.requirements = itemFromList.requirements;
        newObj.itemImageId = itemFromList.itemImageId;
        itemFromList.itemRef = newObj;
    }
}
