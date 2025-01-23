using UnityEngine;

public class KitchenObj : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenParents kitchenObjectParents;
    public KitchenObjectSO getkitchenObjectSO(){
        return kitchenObjectSO;
    }

    public void SetKitchenObjectParents(IKitchenParents ikitchenparent){
        if(this.kitchenObjectParents!=null){
            this.kitchenObjectParents.ClearKitchenObj();
        }

        this.kitchenObjectParents = ikitchenparent;

        if(ikitchenparent.HasKitchenObj()){
            Debug.LogError("Has kitchen Obj");
        }
        ikitchenparent.SetKitchenObj(this);

        transform.parent = ikitchenparent.GetKitchenObjFollowTrans();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenParents GetKitchenObjectParents(){
        return kitchenObjectParents;
    }

    public void DestorySelf(){
        kitchenObjectParents.ClearKitchenObj();
        Destroy(gameObject);
    }

    public static KitchenObj SpawnKitchenObj(KitchenObjectSO kitchenObjectSO,IKitchenParents kitchenParents){
        Transform kitchenObjTransform = Instantiate(kitchenObjectSO.prefabs);
        KitchenObj kitchenObj1 = kitchenObjTransform.GetComponent<KitchenObj>();
        
        kitchenObj1.SetKitchenObjectParents(kitchenParents);
        
        return  kitchenObj1;
    }

    public bool TryGetPlate(out PlateKitchenObj plateKitchenObj){
        if(this is PlateKitchenObj){
            plateKitchenObj = this as PlateKitchenObj;
            return true;
        }
        plateKitchenObj = null;
        return false;
    }
}
