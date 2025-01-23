using UnityEngine;
using UnityEngine.UI;
public class PlateIconUI : MonoBehaviour
{
    [SerializeField] private Image image;
    public void SetKitchenObjSO(KitchenObjectSO kitchenObjectSO){
        image.sprite = kitchenObjectSO.sprite;
    }
}
