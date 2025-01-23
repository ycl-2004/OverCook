using UnityEngine;

public class StoveBurnBarUi : MonoBehaviour
{
    [SerializeField] CookCounter cookCounter;

    Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }
    private void Start() {
        cookCounter.Progresses += ProgressChange;
        animator.SetBool("isFlash",false);
    }

    private void ProgressChange(object sender, IHasProgress.onProgressesEventArgs e){
        float BurnShowProgressAmount = 0.5f;
        bool show = e.progressNormalized >=BurnShowProgressAmount && cookCounter.isCooked();

        animator.SetBool("isFlash",show);
    }

}
