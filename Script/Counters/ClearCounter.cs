using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;


    public override void Interact(Player player){

        if(!HasKitchenObj()){
            if(player.HasKitchenObj()){
                //drop obj
                player.GetKitchenObj().SetKitchenObjectParents(this);
            }else{

            }
        }else{
            if(player.HasKitchenObj()){
                if(player.GetKitchenObj().TryGetPlate(out PlateKitchenObj plateKitchenObj)){
                    if(plateKitchenObj.TryAddIngredient(GetKitchenObj().getkitchenObjectSO())){
                        GetKitchenObj().DestorySelf();
                    }
                }else{
                    // Player not w plate but smt else, with plate on counter
                    if(GetKitchenObj().TryGetPlate(out PlateKitchenObj plateKitchenObj1)){
                        if(plateKitchenObj1.TryAddIngredient(player.GetKitchenObj().getkitchenObjectSO())){
                            player.GetKitchenObj().DestorySelf();
                        }
                    }
                    
                }
            }else{
                GetKitchenObj().SetKitchenObjectParents(player);
            }
        }
    }


}
