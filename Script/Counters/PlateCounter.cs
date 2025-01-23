using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Analytics;
using Unity.VisualScripting;

public class PlateCounter : BaseCounter
{
    private float spawnPlatetimer;
    [SerializeField] float spawnTime = 4f;
    [SerializeField] int maxNum = 5;
    int counter = 0;

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateTaken;

    [SerializeField] KitchenObjectSO platekitchenObjectSO;
    [SerializeField] List<KitchenObjectSO> validitems = new List<KitchenObjectSO>();

    // Update is called once per frame
    void Update()
    {
        spawnPlatetimer += Time.deltaTime;

        if(spawnPlatetimer >=spawnTime&&counter<maxNum&&GameManager.Instance.GamePlaying()){
            
            spawnPlatetimer =0f;
            counter++;

            OnPlateSpawned?.Invoke(this,EventArgs.Empty);
        }
    }

    public override void Interact(Player player)
    {
        if(!player.HasKitchenObj()){
            // Player has empty hands - give them a plate
            if(counter>0){
                // Spawn the plate and get its reference
                KitchenObj plateKO = KitchenObj.SpawnKitchenObj(platekitchenObjectSO,player);  
                counter--;
                OnPlateTaken?.Invoke(this,EventArgs.Empty);

                // Get the BurgerVisual component and connect it to the PlateKitchenObj
                if (plateKO.TryGetComponent<PlateKitchenObj>(out PlateKitchenObj plateKitchenObj)) {
                    BurgerVisual burgerVisual = plateKO.GetComponentInChildren<BurgerVisual>();
                    if (burgerVisual != null) {
                        // Set the reference in BurgerVisual through code
                        burgerVisual.SetPlateKitchenObj(plateKitchenObj);
                    }
                }
            }
        }else{
            KitchenObj kt = player.GetKitchenObj();
            
            // Check if player is holding a valid ingredient for a plate
            if(counter>0 && validitems.Contains(kt.getkitchenObjectSO())){
                // First add ingredient to plate, then spawn plate
                KitchenObjectSO ingredientSO = kt.getkitchenObjectSO();
                kt.DestorySelf(); // Remove ingredient first

                // Now spawn plate
                KitchenObj plateKO = KitchenObj.SpawnKitchenObj(platekitchenObjectSO,player);
                counter--;
                OnPlateTaken?.Invoke(this,EventArgs.Empty);

                // Add ingredient to plate
                if (plateKO.TryGetComponent<PlateKitchenObj>(out PlateKitchenObj plateKitchenObj)) {
                    plateKitchenObj.TryAddIngredient(ingredientSO);
                }
            } else if(player.GetKitchenObj().TryGetPlate(out PlateKitchenObj plateKitchenObj)){  
                // Player is holding a plate - handle existing plate logic
                // do not do anything
            } else {
                // Player is holding something that can't interact with plates
                kt.SetKitchenObjectParents(this);
            }
        }
    }
}
