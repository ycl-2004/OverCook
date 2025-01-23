using UnityEngine;

public class ContainerCVisual : MonoBehaviour
{   
    private const string OPEN_CLOSE = "OpenClose";
    private Animator animator;
    [SerializeField] private ContainerCounter containerCounter;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        containerCounter.OnPlayerGrabObj += ContainerC_OnPlayerGrab;
    }

    private void ContainerC_OnPlayerGrab(object sender, System.EventArgs e){
        animator.SetTrigger(OPEN_CLOSE);
    }
}
