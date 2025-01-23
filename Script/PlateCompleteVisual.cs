using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }

    [SerializeField] private PlateKitchenObj plateKitchenObj;
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjectList;

    private void Start() {
        if (plateKitchenObj == null) {
            Debug.LogError("PlateKitchenObj not assigned in PlateCompleteVisual!");
            return;
        }
        plateKitchenObj.OnItemIn += PlateKitchenObj_OnItemIn;
        Debug.Log("PlateCompleteVisual started and subscribed to events");
        
        // Hide all ingredients at start
        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectSOGameObjectList) {
            kitchenObjectSOGameObject.gameObject.SetActive(false);
        }
    }

    private void PlateKitchenObj_OnItemIn(object sender, PlateKitchenObj.OnItemInEventArgs e) {
        Debug.Log($"PlateCompleteVisual received OnItemIn event for {e.kitchenObjectSO1.name}");
        bool foundMatch = false;
        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectSOGameObjectList) {
            if (kitchenObjectSOGameObject.kitchenObjectSO == e.kitchenObjectSO1) {
                kitchenObjectSOGameObject.gameObject.SetActive(true);
                foundMatch = true;
                Debug.Log($"Visual activated for {e.kitchenObjectSO1.name}");
                break;
            }
        }
        if (!foundMatch) {
            Debug.LogWarning($"No matching visual found for {e.kitchenObjectSO1.name}");
        }
    }

    private void OnDestroy() {
        if (plateKitchenObj != null) {
            plateKitchenObj.OnItemIn -= PlateKitchenObj_OnItemIn;
        }
    }
} 