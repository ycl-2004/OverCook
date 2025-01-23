using UnityEngine;

public class DeliverCounter : BaseCounter
{
    public static DeliverCounter Instance{get; private set;}

    private void Awake() {
        Instance = this;
    }
    public override void Interact(Player player)
    {
        if(player.HasKitchenObj()){
            if(player.GetKitchenObj().TryGetPlate(out PlateKitchenObj plateKitchenObj)){
                //only plate 
                DeliveryManager.Instance.DeliverRecipe(plateKitchenObj);
                player.GetKitchenObj().DestorySelf();
            }
        }
    }
}
