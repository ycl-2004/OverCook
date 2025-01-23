using System;
using UnityEngine;

public class cookingCsual : MonoBehaviour
{
    [SerializeField] CookCounter   cookCounter;
    [SerializeField] GameObject stoveonGame;
    [SerializeField] GameObject particleObj;

    private void Start() {
        cookCounter.OnstateChanged += Cook_OnStateChange;
    }

    private void Cook_OnStateChange(object sender, CookCounter.OnstateChangedEventArgs e){
        bool showVisual = e.state == CookCounter.State.cook || e.state == CookCounter.State.cooked;

        stoveonGame.SetActive(showVisual);
        particleObj.SetActive(showVisual);
    }
}
