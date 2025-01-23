using System;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance{ get; private set;}

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeDone;
    public event EventHandler OnTimeAdded;

    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipefailed;

    private List<RecipeSO> waitingRecipe;
    [SerializeField] RecipeListSO recipeListSO;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 5f;
    private int waitRecipeMax = 4;
    private int deliveredAmount = 0;

    private float addedtime = 0;
    private void Awake(){
        Instance = this;
        
        waitingRecipe = new List<RecipeSO>();
    }
    private void Update() {
        spawnRecipeTimer -= Time.deltaTime;

        if(spawnRecipeTimer<=0f){
            spawnRecipeTimer = spawnRecipeTimerMax;

            if(waitingRecipe.Count<waitRecipeMax &&GameManager.Instance.GamePlaying()){
                RecipeSO recipeSO = recipeListSO.recipeSOsList[Random.Range(0,recipeListSO.recipeSOsList.Count)];
                //Debug.Log(recipeSO.name);
                waitingRecipe.Add(recipeSO);

                OnRecipeSpawned?.Invoke(this,EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObj plateKitchenObj){
        for(int i=0;i<waitingRecipe.Count;i++){
            RecipeSO waitingrecipeSO = waitingRecipe[i];

            if(waitingrecipeSO.kitchenObjectSOList.Count == plateKitchenObj.GetKitchenObjectSOList().Count){
                //has same amount of ingredient
                bool plateContentMatchesRecipe = true;

                foreach(KitchenObjectSO recipeKitchenObjectsSO in waitingrecipeSO.kitchenObjectSOList){
                    //cycling through all ingredient in recipe
                    bool ingredientFound = false;

                    foreach(KitchenObjectSO plateKitchenObjectsSO in plateKitchenObj.GetKitchenObjectSOList()){
                    //cycling through all ingredient in recipe
                        if(plateKitchenObjectsSO == recipeKitchenObjectsSO){
                            ingredientFound = true;
                            break;
                        }
                    }

                    if(!ingredientFound){
                        //this recipe ingredient is not found 
                        plateContentMatchesRecipe = false;
                    }
                }

                if(plateContentMatchesRecipe){
                    //player delivered the correct recipe!
                    Debug.Log("Right Recipe");
                    deliveredAmount++;
                    GameManager.Instance.AddtoGameTimer(GameManager.Instance.GetMaxTimeAdd()*plateKitchenObj.GetAddedTimeRatio()/plateKitchenObj.GetAddTimeOriRatio());
                    addedtime = 100*plateKitchenObj.GetAddedTimeRatio();
                    waitingRecipe.RemoveAt(i);

                    OnRecipeDone?.Invoke(this,EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this,EventArgs.Empty);
                    OnTimeAdded?.Invoke(this,EventArgs.Empty);


                    return;
                }
            }
        }

        //no matches found, not correct recipe
        OnRecipefailed?.Invoke(this,EventArgs.Empty);
        Debug.Log("wrong");
    }


    public List<RecipeSO> GetWaitingRecipeSOList(){
        return waitingRecipe;
    }

    public int DeliveredAmout(){
        return deliveredAmount;
    }

    public float getAddedTime(){
        return addedtime;
    }
}
