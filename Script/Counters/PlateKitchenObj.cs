using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlateKitchenObj : KitchenObj
{
    private List<KitchenObjectSO> kitchenObjectSOs;
    [SerializeField] List<KitchenObjectSO> validitems;
    int singleElementScore = 10;
    int fullElementScore = 50;
    [SerializeField] float addedTimeRatio = 0.05f;

    public event EventHandler<OnItemInEventArgs> OnItemIn;
    public class OnItemInEventArgs: EventArgs{
        public KitchenObjectSO kitchenObjectSO1;
    }
    private void Awake() {
        kitchenObjectSOs = new List<KitchenObjectSO>();
    }
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO){
        if(!validitems.Contains(kitchenObjectSO)){
            Debug.LogError($"Invalid item: {kitchenObjectSO.name} not in valid items list. Valid items count: {validitems.Count}");
            return false;
        }
        if(kitchenObjectSOs.Contains(kitchenObjectSO)){
            Debug.Log($"Item {kitchenObjectSO.name} already on plate");
            return false;
        }else{
            kitchenObjectSOs.Add(kitchenObjectSO);
            
            if (OnItemIn != null) {
                OnItemIn.Invoke(this, new OnItemInEventArgs{
                    kitchenObjectSO1 = kitchenObjectSO
                });
                Debug.Log($"Successfully added {kitchenObjectSO.name} to plate. Total items: {kitchenObjectSOs.Count}");
            } else {
                Debug.LogError("OnItemIn event is null!");
            }
            
            return true;
        }
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList(){
        return kitchenObjectSOs;
    }
    public int ReturnScore(int score){
        return kitchenObjectSOs.Count*score;
    }

    public float GetAddedTimeRatio(){
        return ((float)ReturnScore(singleElementScore)/(float)fullElementScore)*addedTimeRatio;
    }
}
