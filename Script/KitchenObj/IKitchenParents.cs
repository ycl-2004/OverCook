using UnityEngine;


public interface IKitchenParents
{
    public Transform GetKitchenObjFollowTrans();

    public void SetKitchenObj(KitchenObj kitchenObj);

    public KitchenObj GetKitchenObj();

    public void ClearKitchenObj();

    public bool HasKitchenObj();
}
