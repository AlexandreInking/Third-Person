using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(PlayerInventory), menuName = "SO/Inventory/Player Inventory")]
public class PlayerInventory : Inventory
{
    #region QuickSlotEquipped

    public delegate void QuickSlotEquipped(KeyValuePair<Slot, Volume> EquippedEntry);

    public event QuickSlotEquipped OnQuickSlotEquipped;

    public void TriggerQuickSlotEquipped(KeyValuePair<Slot, Volume> EquippedEntry)
    {
        OnQuickSlotEquipped?.Invoke(EquippedEntry);
    }

    #endregion

    #region QuickSlotUnEquipped

    public delegate void QuickSlotUnEquipped(KeyValuePair<Slot, Volume> UnEquippedEntry);

    public event QuickSlotUnEquipped OnQuickSlotUnEquipped;

    public void TriggerQuickSlotUnEquipped(KeyValuePair<Slot, Volume> UnEquippedEntry)
    {
        OnQuickSlotUnEquipped?.Invoke(UnEquippedEntry);
    }

    #endregion

    #region ItemCloned

    public delegate void ItemCloned(Adapter adapter);

    public event ItemCloned OnItemCloned;

    public void TriggerItemCloned(int slotIndex)
    {

        HotBar[GetHotBarEntry(slotIndex).Key].Instance
            .TryGetComponent(out Adapter adapter);

        if (adapter)
        {
            HotBar[GetHotBarEntry(slotIndex).Key].Adapter = adapter;

            OnItemCloned?.Invoke(adapter);
        }
    }

    #endregion

    //Test Items
    public Bow testBow;
    public Magazine testMag;
    public Magazine testMag2;

    #region Containers

    [HideInInspector] public Transform rangedWeapon;

    [HideInInspector] public Transform leftHand;

    [HideInInspector] public Transform rightHand;

    #endregion

    public KeyValuePair<Slot, Volume> ActiveEntry { get; private set; }

    public Dictionary<Slot, Volume> HotBar = new Dictionary<Slot, Volume>();

    protected override void OnEnable()
    {
        base.OnEnable();

        InitializeHotBar();

        //Add Test Items
        AddItem(testBow);
        AddItem(testMag);
        AddItem(testMag2);
    }

    /// <summary>
    /// Slots in <see cref="GameConstants.PlayerSlots"/>
    /// </summary>
    public void InitializeHotBar()
    {
        for (int i = 0; i < GameConstants.PlayerSlots.Count; i++)
        {
            Slot slot = new Slot()
            {
                index = i,
                isEquipped = false
            };

            HotBar.Add(slot, new Volume
            {
                Item = null,
                Instance = null,
                Adapter = null
            });
        }

        //Init ActiveEntry
        ActiveEntry = GetHotBarEntry(0);
    }

    /// <summary>
    /// Occupies a Quick Slot in the Hot Bar
    /// </summary>
    /// <param name="inventoryEntry">Entry Occupying the Hot Bar Quick Slot</param>
    /// <param name="slotIndex">Index Of Slot to be Occupied</param>
    public void HoldQuickSlot(KeyValuePair<Slot, Item> inventoryEntry, int slotIndex)
    {
        HotBar[GetHotBarEntry(slotIndex).Key].Item = inventoryEntry.Value;

        HotBar[GetHotBarEntry(slotIndex).Key].Instance = Instantiate(inventoryEntry.Value.itemPrefab);

        TriggerItemCloned(slotIndex);
    }

    /// <summary>
    /// Releases a Quick Slot in the Hot Bar
    /// </summary>
    /// <param name="slotIndex">Index Of Slot to be Released</param>
    public void ReleaseQuickSlot(int slotIndex)
    {
        KeyValuePair<Slot, Volume> keyPair = GetHotBarEntry(slotIndex);

        if (keyPair.Value.Item == null)
        {
            Debug.LogError($"Slot {slotIndex} Already Released");

            return;
        }

        //TODO: Test Release Quick Slot
        HotBar[GetHotBarEntry(slotIndex).Key].ClearVoulme();
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

        if (GetHotBarEntry(slotIndex).Value.Item == null)
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

        if (GetHotBarEntry(slotIndex).Value.Item == null)
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
    public KeyValuePair<Slot, Volume> GetHotBarEntry(int slotIndex)
    {
        KeyValuePair<Slot, Volume> keyPair = HotBar.FirstOrDefault(pair => pair.Key.index.Equals(slotIndex));

        if (keyPair.Key == null)
            Debug.LogError($"Hot Bar Slot {slotIndex} Not Initialized : Out Of Bounds");

        return keyPair;
    }
}
