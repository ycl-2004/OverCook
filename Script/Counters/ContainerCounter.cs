using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public event EventHandler OnPlayerGrabObj;

    public override void Interact(Player player){
        if(!player.HasKitchenObj()){
            //KitchenObj.SpawnKitchenObj(kitchenObjectSO,player); 
            KitchenObj kt = KitchenObj.SpawnKitchenObj(kitchenObjectSO,player); 
            kt.gameObject.SetActive(true);
            OnPlayerGrabObj?.Invoke(this,EventArgs.Empty);
        }else{
            //Debug.Log(kitchenObj.GetKitchenObjectParents());
            //Give the Object to Player
            //kitchenObj.SetKitchenObjectParents(player);

            //ADDDDDD THIS CODE NEW EDITION
            if(player.GetKitchenObj().TryGetPlate(out PlateKitchenObj plateKitchenObj)){
                KitchenObj kt = KitchenObj.SpawnKitchenObj(kitchenObjectSO,this); 
                kt.gameObject.SetActive(true);
            
                if(plateKitchenObj.TryAddIngredient(kt.getkitchenObjectSO())){
                    //kt.DestorySelf();
                }
                kt.DestorySelf();
            }
            
        }
    }


}
