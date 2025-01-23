using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingle : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI recipeNameText;
    [SerializeField] Transform iconContainer;
    [SerializeField] Transform iconTemp;


    private void Awake() {
        iconTemp.gameObject.SetActive(false);
    }
    public void SetRecipeSO(RecipeSO recipeSO){
        recipeNameText.text = recipeSO.recipeName;

        foreach(Transform child in iconContainer){
            if(child == iconTemp) continue;
            Destroy(child.gameObject);
        }

        foreach(KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList){
            Transform iconTrans = Instantiate(iconTemp,iconContainer);
            iconTrans.gameObject.SetActive(true);
            iconTrans.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }   
    }
}
