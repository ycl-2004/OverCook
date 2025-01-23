using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private const string Player_Pref_Binding = "Inputbinding";
    public enum Binding{
        Moveup,
        Movedown,
        Moveleft,
        Moveright,
        Interaction,
        InteractAlt,
        Pausion,
    }
    private ActionMap inputActions;
    public event EventHandler OnInteractAction,OnInteractCutAction;
    public event EventHandler OnPauseAction;
    public event EventHandler OnRebindAction;

    public static GameInput Instance{ get; private set;}

    private void Awake() {
        Instance = this;
        inputActions = new ActionMap();

        if(PlayerPrefs.HasKey(Player_Pref_Binding)){
            inputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(Player_Pref_Binding));
        }
    
        inputActions.Player.Enable();

        inputActions.Player.Interact.performed +=  Interact_performed;
        inputActions.Player.InteractNew.performed +=  InteractNew_performed;
        inputActions.Player.Pause.performed += Pause_performed;


        //Debug.Log(GetBindingText(Binding.Interaction));
    }

    private void OnDestroy() {
        inputActions.Player.Interact.performed -=  Interact_performed;
        inputActions.Player.InteractNew.performed -=  InteractNew_performed;
        inputActions.Player.Pause.performed -= Pause_performed;

        inputActions.Dispose();
    }
    public Vector2 GetNormalizedMovementVector(){
        Vector2 inputVector = inputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        //Debug.Log(obj);

        OnInteractAction?.Invoke(this,EventArgs.Empty);
        
    }
    private void InteractNew_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        //Debug.Log(obj);

        OnInteractCutAction?.Invoke(this,EventArgs.Empty);
        
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        OnPauseAction?.Invoke(this,EventArgs.Empty);
    }

    public string GetBindingText(Binding binding){
        switch(binding){
            default:
            case Binding.Moveup:
                return inputActions.Player.Move.bindings[1].ToDisplayString();
            case Binding.Movedown:
                return inputActions.Player.Move.bindings[2].ToDisplayString();
            case Binding.Moveleft:
                return inputActions.Player.Move.bindings[3].ToDisplayString();
            case Binding.Moveright:
                return inputActions.Player.Move.bindings[4].ToDisplayString();
            case Binding.Interaction:
                return inputActions.Player.Interact.bindings[0].ToDisplayString();
            case Binding.InteractAlt:
                return inputActions.Player.InteractNew.bindings[0].ToDisplayString();
            case Binding.Pausion:
                return inputActions.Player.Pause.bindings[0].ToDisplayString();
                
        }
    }

    public void RebindBinding(Binding binding, Action onActionRebound){
        inputActions.Player.Disable();

        InputAction currentInputAction;
        int bindingIndex;
        switch(binding){
            case Binding.Moveup:
                currentInputAction = inputActions.Player.Move;
                bindingIndex = 1;
                break;
            case Binding.Movedown:
                currentInputAction = inputActions.Player.Move;
                bindingIndex = 2;
                break;
            case Binding.Moveleft:
                currentInputAction = inputActions.Player.Move;
                bindingIndex = 3;
                break;
            case Binding.Moveright:
                currentInputAction = inputActions.Player.Move;
                bindingIndex = 4;
                break;
            case Binding.Interaction:
                currentInputAction = inputActions.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.InteractAlt:
                currentInputAction = inputActions.Player.InteractNew;
                bindingIndex = 0;
                break;
            case Binding.Pausion:
                currentInputAction = inputActions.Player.Pause;
                bindingIndex = 0;
                break;
            default:
                currentInputAction = null;
                bindingIndex = 0;
                break;
        }
        currentInputAction.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(callback=>{
                callback.Dispose();
                inputActions.Player.Enable();
                onActionRebound();
                
                PlayerPrefs.SetString(Player_Pref_Binding,inputActions.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();

                OnRebindAction?.Invoke(this,EventArgs.Empty);
            })
            .Start();
    }
}
