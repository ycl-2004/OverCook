using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
//using UnityEngine.Rendering.VirtualTexturing;

public class Player : MonoBehaviour,IKitchenParents
{
    [Header("Movement Control")]
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float rotSpeed = 2f;

    [Header("Animator")]
    [SerializeField] Animator PlayerAnimator;
    
    [Header("Input Action")]
    private InputSystem_Actions inputActions;
    [SerializeField] private GameInput gameInput;
    Vector3 movedirection;

    [Header("Layer Mask")]
    [SerializeField] private LayerMask layerMask;

    private BaseCounter selectedOne;

    public event EventHandler<OnSelectCounterCEventArgs> OnSelectCounterC;
    public class OnSelectCounterCEventArgs: EventArgs{
        public BaseCounter selectedCounter;
    }

    public event EventHandler OnPickAny;
    public static Player Instance{get; private set;}

    private KitchenObj kitchenObj1;
    [SerializeField] Transform kitchenObjHoldPos;

    private void Awake() {
        if(Instance !=null){
            Debug.LogError("MORE Player");
        }
        Instance = this;
    }

    private void Start() {
        gameInput.OnInteractAction+= GameInput_InteractionAction;
        gameInput.OnInteractCutAction+= GameInput_InteractionCutAction;
    }
    private void Update() {
        Movement();
        Interaction();
    }

    private void Movement()
    {
    
        Vector2 inputVector = gameInput.GetNormalizedMovementVector();

        movedirection = new Vector3(inputVector.x, 0f, inputVector.y);

        movedirection = CheckCollision(movedirection);

        transform.forward = Vector3.Slerp(transform.forward, movedirection, Time.deltaTime * rotSpeed);
    }

    private Vector3 CheckCollision(Vector3 movedirection)
    {
        float moveDis = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f, playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, movedirection, moveDis);

        if (!canMove)
        {
            Vector3 movedirX = new Vector3(movedirection.x, 0, 0).normalized;
            canMove = movedirection.x!=0&&!Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, movedirX, moveDis);

            if (canMove)
            {
                movedirection = movedirX;
            }
            else
            {
                Vector3 movedirZ = new Vector3(0, 0, movedirection.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, movedirZ, moveDis);

                if (canMove)
                {
                    movedirection = movedirZ;
                }
            }
        }

        if (canMove)
        {
            transform.position += movedirection * (moveSpeed * Time.deltaTime);
            CheckMovement(movedirection);
        }

        return movedirection;
    }

    private void CheckMovement(Vector3 movedirection)
    {
        if (movedirection != Vector3.zero)
        {
            PlayerAnimator.SetBool("isWalking", true);
        }
        else
        {
            PlayerAnimator.SetBool("isWalking", false);
        }
    }

    public bool isWalking(){
        return movedirection != Vector3.zero;
    }

    private void Interaction()
    {
        // Get input vector and normalize it
        Vector2 inputVector = gameInput.GetNormalizedMovementVector();

        // Calculate movement direction
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        Vector3 lastDir = Vector3.zero;

        // Update lastDir only if moveDir is not zero
        if (moveDir != Vector3.zero)
        {
            lastDir = moveDir;
        }

        // Set interaction distance to a reasonable finite value
        float interactDis = 2f; // Adjust as needed

        // Perform raycast
        if (Physics.Raycast(transform.position+new Vector3(0f,transform.position.y,0f), transform.forward, out RaycastHit raycastHit, interactDis,layerMask))
        {
            if(raycastHit.transform.TryGetComponent(out BaseCounter baseCounter)){
                // has Clear Counter
                //clearCounter.Interact();
                if(baseCounter!=selectedOne)
                {
                    SetSelectCounter(baseCounter);
                }
            }
            else{
                SetSelectCounter(null);
            }
        }else{
            SetSelectCounter(null);
        }

        //Debug.Log(selectedOne);
    }

    private void SetSelectCounter(BaseCounter baseCounter)
    {
        selectedOne = baseCounter;

        OnSelectCounterC?.Invoke(this, new OnSelectCounterCEventArgs
        {
            selectedCounter = selectedOne
            
        });
        
    }

    private void GameInput_InteractionAction(object sender,System.EventArgs e){

        if(!GameManager.Instance.GamePlaying()){return;}

        if(selectedOne !=null){
            selectedOne.Interact(this);
        }
        
    }

    private void GameInput_InteractionCutAction(object sender,System.EventArgs e){

        if(!GameManager.Instance.GamePlaying()){return;}
        
        if(selectedOne !=null){
            selectedOne.InteractCut(this);
        }
    }
    public Transform GetKitchenObjFollowTrans()
    {
        return kitchenObjHoldPos;
    }

    public void SetKitchenObj(KitchenObj kitchenObj)
    {
        kitchenObj1 = kitchenObj;

        if(kitchenObj!=null){
            OnPickAny?.Invoke(this,EventArgs.Empty);
        }
    }

    public KitchenObj GetKitchenObj()
    {
        return kitchenObj1;
    }

    public void ClearKitchenObj()
    {
        kitchenObj1 = null;
    }

    public bool HasKitchenObj()
    {
        return kitchenObj1 !=null;
    }
}
