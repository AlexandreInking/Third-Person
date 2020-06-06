using UnityEngine;

public class InventoryActionPack : ActionPack
{
    public override void OnInitialize()
    {
        InitializeAction<InitContainersAction>();

        InitializeAction<EquipAction>();

        InitializeAction<UnEquipAction>();

        InitializeAction<ChangeAction>();
    }
}
