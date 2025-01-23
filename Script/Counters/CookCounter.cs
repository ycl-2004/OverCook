using System;
using NUnit.Framework;
using UnityEngine;

public class CookCounter : BaseCounter,IHasProgress
{
    public event EventHandler<IHasProgress.onProgressesEventArgs> Progresses;
    [SerializeField] private CookSO[] cookKitchenObjSO;
    [SerializeField] private OvercookSO[] overcookKitchenObjSO;
    private CookSO cookSO;
    private OvercookSO overcookSO;
    private float cookingTimer;
    private float overcookingTimer;

    private State state;
    public enum State{
        idle,
        cook,
        cooked,
        overcook,
    }

    public event EventHandler<OnstateChangedEventArgs> OnstateChanged;
    public class OnstateChangedEventArgs: EventArgs{
        public State state;
    }
    private void Start() {
        state = State.idle;
    }
    private void Update() {
        if(HasKitchenObj()){
            switch(state){
                case State.idle:
                    break;
                case State.cook:

                    Progresses?.Invoke(this, new IHasProgress.onProgressesEventArgs{
                        progressNormalized = (float)cookingTimer/(float)cookSO.cookTimer
                    });

                    cookingTimer+=Time.deltaTime;

                    if(cookingTimer>cookSO.cookTimer){
                        //fried;
                        GetKitchenObj().DestorySelf();
                        KitchenObj kt = KitchenObj.SpawnKitchenObj(cookSO.output,this); 
                        kt.gameObject.SetActive(true);

                        state = State.cooked;
                        overcookingTimer = 0;
                        overcookSO = getOverCookInput(GetKitchenObj().getkitchenObjectSO());
                        OnstateChanged?.Invoke(this, new OnstateChangedEventArgs{
                            state = state
                        });
                    }
                    break;
                case State.cooked:
                    overcookingTimer+=Time.deltaTime;

                    Progresses?.Invoke(this, new IHasProgress.onProgressesEventArgs{
                        progressNormalized = (float)overcookingTimer/(float)overcookSO.overcookTimer
                    });

                    

                    if(overcookingTimer>overcookSO.overcookTimer){
                        //fried;
                        GetKitchenObj().DestorySelf();
                        KitchenObj.SpawnKitchenObj(overcookSO.output,this);
                        state = State.overcook;

                        OnstateChanged?.Invoke(this, new OnstateChangedEventArgs{
                            state = state
                        });

                        Progresses?.Invoke(this, new IHasProgress.onProgressesEventArgs{
                            progressNormalized = 0f
                        });
                    }

                    

                    break;
                case State.overcook:
                    break;
            }
            //Debug.Log(state);
        }
    }
    public override void Interact(Player player)
    {
        if(!HasKitchenObj()){
            if(player.HasKitchenObj()){
                //drop obj
                if(HasOutput(player.GetKitchenObj().getkitchenObjectSO())){
                    //check we out slicable object
                    player.GetKitchenObj().SetKitchenObjectParents(this);
                    
                    cookSO = getCookInput(GetKitchenObj().getkitchenObjectSO());
                    state = State.cook;

                    OnstateChanged?.Invoke(this, new OnstateChangedEventArgs{
                            state = state
                    });
                    cookingTimer = 0;

                    Progresses?.Invoke(this, new IHasProgress.onProgressesEventArgs{
                        progressNormalized = (float)cookingTimer/(float)cookSO.cookTimer
                    });
                }
            }else{

            }
        }else{
            if(player.HasKitchenObj()){
                if(player.GetKitchenObj().TryGetPlate(out PlateKitchenObj plateKitchenObj)){
                    if(plateKitchenObj.TryAddIngredient(GetKitchenObj().getkitchenObjectSO())){
                        GetKitchenObj().DestorySelf();

                        state = State.idle;
                        OnstateChanged?.Invoke(this, new OnstateChangedEventArgs{
                            state = state
                        });

                        Progresses?.Invoke(this, new IHasProgress.onProgressesEventArgs{
                                progressNormalized = 0f
                        });
                    }
                }
            }else{
                GetKitchenObj().SetKitchenObjectParents(player);

                state = State.idle;
                OnstateChanged?.Invoke(this, new OnstateChangedEventArgs{
                    state = state
                });

                Progresses?.Invoke(this, new IHasProgress.onProgressesEventArgs{
                        progressNormalized = 0f
                });
            }
        }
    }

    private KitchenObjectSO getSlices(KitchenObjectSO kitchenObjectSO){
        CookSO cook = getCookInput(kitchenObjectSO);
        if(cook){
            return cook.output;
        }else{
            return null;
        }
    }

    private bool HasOutput(KitchenObjectSO kitchenObjectSOinput){
        CookSO cook = getCookInput(kitchenObjectSOinput);
        return cook!=null;
       
    }

    private CookSO getCookInput(KitchenObjectSO inputKitchenSO){
        foreach(CookSO cookSO in cookKitchenObjSO){
            if(cookSO.input == inputKitchenSO){
                return cookSO;
            }
        }
        return null;
    }

    private OvercookSO getOverCookInput(KitchenObjectSO inputKitchenSO){
        foreach(OvercookSO overcookSO in overcookKitchenObjSO){
            if(overcookSO.input == inputKitchenSO){
                return overcookSO;
            }
        }
        return null;
    }

    public bool isCooked(){
        return state == State.cooked;
    }   
}
