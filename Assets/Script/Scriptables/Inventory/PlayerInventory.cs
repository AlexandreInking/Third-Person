using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(PlayerInventory), menuName = "SO/Inventory/Player Inventory")]
public class PlayerInventory : Inventory
{
    #region QuickSlotEquipped

    public delegate void QuickSlotEquipped(KeyValuePair<Slot, Item> EquippedEntry);

    public event QuickSlotEquipped OnQuickSlotEquipped;

    public void TriggerQuickSlotEquipped(KeyValuePair<Slot, Item> EquippedEntry)
    {
        OnQuickSlotEquipped?.Invoke(EquippedEntry);
    }

    #endregion

    #region QuickSlotUnEquipped

    public delegate void QuickSlotUnEquipped(KeyValuePair<Slot, Item> UnEquippedEntry);

    public event QuickSlotUnEquipped OnQuickSlotUnEquipped;

    public void TriggerQuickSlotUnEquipped(KeyValuePair<Slot, Item> UnEquippedEntry)
    {
        OnQuickSlotUnEquipped?.Invoke(UnEquippedEntry);
    }

    #endregion


    #region ItemCloned

    public delegate void ItemCloned(Adapter adapter);

    public event ItemCloned OnItemCloned;

    public void TriggerItemCloned(GameObject obj)
    {

        obj.TryGetComponent(out Adapter adapter);

        if (adapter)
            OnItemCloned?.Invoke(adapter);
    }

    #endregion


    public Item testBow;

    #region Containers

    [HideInInspector] public Transform rangedWeapon;
    [HideInInspector] public Transform leftHand;
    [HideInInspector] public Transform rightHand;

    #endregion

    public KeyValuePair<Slot, Item> ActiveEntry { get; private set; }

    public Dictionary<Slot, Item> HotBar = new Dictionary<Slot, Item>();

    private void OnEnable()
    {
        //Init Hot Bar With {i} Slots
        for (int i = 0; i < 5; i++)
        {
            Slot slot = new Slot()
            {
                index = i,
                isEquipped = false
            };

            HotBar.Add(slot, null);
        }

        //Init Library With {i} Slots
        //for (int i = 0; i < 5; i++)
        //{
        //    Slot slot = new Slot()
        //    {
        //        index = i,
        //        isEquipped = false
        //    };

        //    Library.Add(slot, null);
        //}

        //Init ActiveEntry
        ActiveEntry = GetHotBarEntry(0);

        //Add Test Bow
        Slot bowSlot = new Slot()
        {
            index = 2,
            isEquipped = false
        };

        Library.Add(bowSlot, testBow);
    }

    /// <summary>
    /// Occupies a Quick Slot in the Hot Bar
    /// </summary>
    /// <param name="inventoryEntry">Entry Occupying the Hot Bar Quick Slot</param>
    /// <param name="slotIndex">Index Of Slot to be Occupied</param>
    public void HoldQuickSlot(KeyValuePair<Slot, Item> inventoryEntry, int slotIndex)
    {
        HotBar[GetHotBarEntry(slotIndex).Key] = inventoryEntry.Value;

        GameObject obj = Instantiate(inventoryEntry.Value.itemPrefab);

        TriggerItemCloned(obj);
    }

    /// <summary>
    /// Releases a Quick Slot in the Hot Bar
    /// </summary>
    /// <param name="slotIndex">Index Of Slot to be Released</param>
    public void ReleaseQuickSlot(int slotIndex)
    {
        KeyValuePair<Slot, Item> keyPair = GetHotBarEntry(slotIndex);

        if (keyPair.Value == null)
        {
            Debug.LogError($"Slot {slotIndex} Already Released");
            return;
        }

        HotBar[GetHotBarEntry(slotIndex).Key] = null;

        //TODO: More Implementation For Non Ranged
        //Destroy Instance of Item
    }

    /// <summary>
    /// Equip Quick Slot
    /// </summary>
    /// <param name="slotIndex">Index of Quick Slot to Equip</param>
    public void EquipQuickSlot(int slotIndex)
    {
        //Already Equipped
        if (GetHotBarEntry(slotIndex).Key.isEquipped)
        {
            Debug.LogError($"Hot Slot {slotIndex} : Already Equipped");
            return;
        }

        ActiveEntry = GetHotBarEntry(slotIndex);

        HotBar.Remove(ActiveEntry.Key);

        ActiveEntry.Key.isEquipped = true;

        HotBar.Add(ActiveEntry.Key, ActiveEntry.Value);

        if (GetHotBarEntry(slotIndex).Value == null)
        {
            Debug.LogError($"Can't Equip Slot {slotIndex} : Empty Slot");
            return;
        }

        TriggerQuickSlotEquipped(ActiveEntry);
    }

    /// <summary>
    /// UnEquip Quick Slot
    /// </summary>
    /// <param name="slotIndex">Index of Quick Slot to UnEquip</param>
    public void UnEquipQuickSlot(int slotIndex)
    {
        //Already UnEquipped
        if (!GetHotBarEntry(slotIndex).Key.isEquipped)
        {
            Debug.LogError($"Hot Slot {slotIndex} : Already UnEquipped");
            return;
        }

        ActiveEntry = GetHotBarEntry(slotIndex);

        HotBar.Remove(ActiveEntry.Key);

        ActiveEntry.Key.isEquipped = false;

        HotBar.Add(ActiveEntry.Key, ActiveEntry.Value);

        if (GetHotBarEntry(slotIndex).Value == null)
        {
            Debug.LogError($"Can't UnEquip Slot {slotIndex} : Empty Slot");
            return;
        }

        TriggerQuickSlotUnEquipped(ActiveEntry);
    }

    /// <summary>
    /// Equip / UnEquip to Desired Slot
    /// </summary>
    /// <param name="slotIndex">Desired Slot Index</param>
    public void ChangeItem(int slotIndex)
    {
        //Longer For Readability
        if (ActiveEntry.Key.index.Equals(slotIndex))
        {
            if (ActiveEntry.Key.isEquipped)
                UnEquipQuickSlot(slotIndex);

            else
                EquipQuickSlot(slotIndex);
        }

        else
        {
            if (ActiveEntry.Key.isEquipped)
                UnEquipQuickSlot(ActiveEntry.Key.index);

                EquipQuickSlot(slotIndex);
        }
    }

    /// <summary>
    /// Changes Made to The returned instance Will Not Apply
    /// </summary>
    /// <param name="slotIndex">Index of Slot Entry</param>
    /// <returns></returns>
    public KeyValuePair<Slot, Item> GetInventoryEntry(int slotIndex)
    {
        KeyValuePair<Slot, Item> keyPair = Library.FirstOrDefault(pair => pair.Key.index.Equals(slotIndex));

        if (keyPair.Equals(null))
            Debug.LogError($"Library Slot {slotIndex} Not Initialized : Out Of Bounds");

        return keyPair;
    }

    /// <summary>
    /// Changes Made to The returned instance Will Not Apply
    /// </summary>
    /// <param name="slotIndex">Index of Slot Entry</param>
    /// <returns></returns>
    public KeyValuePair<Slot, Item> GetHotBarEntry(int slotIndex)
    {
        KeyValuePair<Slot, Item> keyPair = HotBar.FirstOrDefault(pair => pair.Key.index.Equals(slotIndex));

        if (keyPair.Equals(null))
            Debug.LogError($"Hot Bar Slot {slotIndex} Not Initialized : Out Of Bounds");

        return keyPair;
    }
}
