using UnityEngine;

public class PlateIcon : MonoBehaviour
{
    [SerializeField] PlateKitchenObj plateKitchenObj;
    [SerializeField] Transform iconTemp;

    private void Awake() {
        iconTemp.gameObject.SetActive(false);
    }
    private void Start() {
        plateKitchenObj.OnItemIn += onItemadded;
    }

    private void onItemadded(object sender, PlateKitchenObj.OnItemInEventArgs e){
        updateVisual();
    }

    private void updateVisual(){

        foreach(Transform child in transform){
            if(child == iconTemp) continue;
            Destroy(child.gameObject);
        }

        foreach(KitchenObjectSO kitchenObjectSO in plateKitchenObj.GetKitchenObjectSOList()){
            Transform icon_img = Instantiate(iconTemp,transform);
            icon_img.gameObject.SetActive(true);
            icon_img.GetComponent<PlateIconUI>().SetKitchenObjSO(kitchenObjectSO);
        }
    }
}

