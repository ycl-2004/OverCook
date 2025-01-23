using System;
using Unity.VisualScripting;
using UnityEngine;

public class CuttingCounter : BaseCounter,IHasProgress
{
    [SerializeField] private CutSO[] cutKitchenObjSO;

    public event EventHandler<IHasProgress.onProgressesEventArgs> Progresses;
    public event EventHandler OnCut;
    public static event EventHandler OnAnyCut;

    new public static void ResetStatic(){
        OnAnyCut = null;
    }
    private int cuttingTime = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Interact(Player player)
    {
        if(!HasKitchenObj()){
            if(player.HasKitchenObj()){
                //drop obj
                if(HasOutput(player.GetKitchenObj().getkitchenObjectSO())){
                    //check we out slicable object
                    player.GetKitchenObj().SetKitchenObjectParents(this);
                    cuttingTime = 0;

                    CutSO cutSO = getCutInput(GetKitchenObj().getkitchenObjectSO());
                    Progresses?.Invoke(this, new IHasProgress.onProgressesEventArgs{
                        progressNormalized = (float)cuttingTime/(float)cutSO.cutAmount
                    });
                }
            }else{

            }
        }else{
            if(player.HasKitchenObj()){
                if(player.GetKitchenObj().TryGetPlate(out PlateKitchenObj plateKitchenObj)){
                    if(plateKitchenObj.TryAddIngredient(GetKitchenObj().getkitchenObjectSO())){
                        GetKitchenObj().DestorySelf();
                    }
                }
                
            }else{
                GetKitchenObj().SetKitchenObjectParents(player);
            }
        }
    }

    public override void InteractCut(Player player)
    {
        if(HasKitchenObj()&& HasOutput(GetKitchenObj().getkitchenObjectSO())){
            cuttingTime++;
            OnCut?.Invoke(this,EventArgs.Empty);
            OnAnyCut?.Invoke(this,EventArgs.Empty);
            CutSO cutSO = getCutInput(GetKitchenObj().getkitchenObjectSO());

            Progresses?.Invoke(this, new IHasProgress.onProgressesEventArgs{
                progressNormalized = (float)cuttingTime/(float)cutSO.cutAmount
            });

            if(cuttingTime>=cutSO.cutAmount){
                KitchenObjectSO outputKit = getSlices(GetKitchenObj().getkitchenObjectSO());
                GetKitchenObj().DestorySelf();

                KitchenObj kt = KitchenObj.SpawnKitchenObj(outputKit,this); 
                kt.gameObject.SetActive(true);

            }
        }
    }

    private KitchenObjectSO getSlices(KitchenObjectSO kitchenObjectSO){
        CutSO cut = getCutInput(kitchenObjectSO);
        if(cut){
            return cut.output;
        }else{
            return null;
        }
    }

    private bool HasOutput(KitchenObjectSO kitchenObjectSOinput){
        CutSO cut = getCutInput(kitchenObjectSOinput);
        return cut!=null;
       
    }

    private CutSO getCutInput(KitchenObjectSO inputKitchenSO){
        foreach(CutSO cutSO in cutKitchenObjSO){
            if(cutSO.input == inputKitchenSO){
                return cutSO;
            }
        }
        return null;
    }
}
