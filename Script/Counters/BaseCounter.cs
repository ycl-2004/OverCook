using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseCounter : MonoBehaviour,IKitchenParents
{
    public static event EventHandler OnAnyObjPlace;

    public static void ResetStatic(){
        OnAnyObjPlace = null;
    }
    [SerializeField] private Transform topPos;
    private KitchenObj kitchenObj;
    public virtual void Interact(Player player){
        Debug.LogError("Base Counter Interact");
    }

    public virtual void InteractCut(Player player){
        //Debug.LogError("Base Counter Interact");
    }
    public Transform GetKitchenObjFollowTrans(){
        return topPos;
    }

    public void SetKitchenObj(KitchenObj kitchenObj){
        this.kitchenObj = kitchenObj;

        if(kitchenObj!=null){
            OnAnyObjPlace?.Invoke(this,EventArgs.Empty);
        }
    }

    public KitchenObj GetKitchenObj(){
        return kitchenObj;
    }

    public void ClearKitchenObj(){
        kitchenObj = null;
    }

    public bool HasKitchenObj(){
        return kitchenObj !=null;
    }
}
