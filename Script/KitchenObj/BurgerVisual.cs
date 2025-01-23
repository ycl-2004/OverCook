using System;
using System.Collections.Generic;
using UnityEngine;

public class BurgerVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObj{
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }
    [SerializeField] PlateKitchenObj plateKitchenObj;

    [SerializeField] List<KitchenObjectSO_GameObj> kitchenObjectSO_GameObjs;

    private void Awake() {
        plateKitchenObj = GetComponent<PlateKitchenObj>();  // Get reference automatically
        if (plateKitchenObj == null) {
            plateKitchenObj = GetComponentInParent<PlateKitchenObj>();  // Try parent if not on same object
        }
        
        if (plateKitchenObj == null) {
            Debug.LogError($"PlateKitchenObj not found for BurgerVisual on {gameObject.name}!");
            return;
        }

        // Subscribe immediately in Awake
        plateKitchenObj.OnItemIn += OnItemInEvent;
        
        // Hide all ingredients at start
        foreach(KitchenObjectSO_GameObj kitchenObjectSO_GameObj in kitchenObjectSO_GameObjs){
            kitchenObjectSO_GameObj.gameObject.SetActive(false);
        }
    }

    private void OnItemInEvent(object sender, PlateKitchenObj.OnItemInEventArgs e){
        Debug.Log($"BurgerVisual received ingredient: {e.kitchenObjectSO1.name}");
        foreach(KitchenObjectSO_GameObj kitchenObjectSO_GameObj in kitchenObjectSO_GameObjs){
            if(kitchenObjectSO_GameObj.kitchenObjectSO == e.kitchenObjectSO1){
                kitchenObjectSO_GameObj.gameObject.SetActive(true);
                Debug.Log($"Visual activated for {e.kitchenObjectSO1.name}");
                break;
            }
        }
    }

    private void OnDestroy() {
        if (plateKitchenObj != null) {
            plateKitchenObj.OnItemIn -= OnItemInEvent;
        }
    }

    public void SetPlateKitchenObj(PlateKitchenObj newPlateKitchenObj) {
        // Unsubscribe from old plate if exists
        if (plateKitchenObj != null) {
            plateKitchenObj.OnItemIn -= OnItemInEvent;
        }
        
        plateKitchenObj = newPlateKitchenObj;
        
        // Subscribe to new plate
        if (plateKitchenObj != null) {
            plateKitchenObj.OnItemIn += OnItemInEvent;
            Debug.Log($"BurgerVisual on {gameObject.name} connected to new PlateKitchenObj");
        }
    }
}
