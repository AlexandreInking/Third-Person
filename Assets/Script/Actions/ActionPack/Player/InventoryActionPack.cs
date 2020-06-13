using UnityEngine;

public class InventoryActionPack : ActionPack
{
    public override void OnInitialize()
    {
        InitializeAction<InitContainersAction>();

        #region Equip | UnEquip

        InitializeAction<EquipAction>();

        InitializeAction<UnEquipAction>();

        #endregion

        InitializeAction<ChangeAction>();

        InitializeAction<ItemAdapterAction>();
    }
}
