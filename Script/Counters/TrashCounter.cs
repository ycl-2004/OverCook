using System;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnAnyTrash;

    new public static void ResetStatic(){
        OnAnyTrash = null;
    }
    public override void Interact(Player player)
    {
        if(player.HasKitchenObj()){
            player.GetKitchenObj().DestorySelf();

            OnAnyTrash?.Invoke(this,EventArgs.Empty);
        }
    }
}
