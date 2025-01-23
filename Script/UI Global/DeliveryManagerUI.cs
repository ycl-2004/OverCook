using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] Transform container;
    [SerializeField] Transform recipeTemp;

    private void Awake() {
        recipeTemp.gameObject.SetActive(false);
    }

    private void Start() {
        DeliveryManager.Instance.OnRecipeDone += Instance_DoneRes;
        DeliveryManager.Instance.OnRecipeSpawned += Instance_Spawned; 

        updateVisual();
    }

    private void Instance_DoneRes(object sender, System.EventArgs e){
        updateVisual();
    }

    private void Instance_Spawned(object sender, System.EventArgs e){
        updateVisual();
    }
    private void updateVisual(){
        foreach(Transform child in container){
            if(child == recipeTemp) continue;
            Destroy(child.gameObject);
        }

        foreach(RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList()){
            Transform recipeTrans = Instantiate(recipeTemp,container);
            recipeTrans.gameObject.SetActive(true);
            recipeTrans.GetComponent<DeliveryManagerSingle>().SetRecipeSO(recipeSO);
        }
    }
}
