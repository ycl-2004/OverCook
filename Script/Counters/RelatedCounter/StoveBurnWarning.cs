using UnityEngine;

public class StoveBurnWarning : MonoBehaviour
{
    [SerializeField] CookCounter cookCounter;

    private void Start() {
        cookCounter.Progresses += ProgressChange;
        Hide();
    }

    private void ProgressChange(object sender, IHasProgress.onProgressesEventArgs e){
        float BurnShowProgressAmount = 0.5f;
        bool show = e.progressNormalized >=BurnShowProgressAmount && cookCounter.isCooked();

        if(show){
            Show();
        }else{
            Hide();
        }
    }

    private void Show(){
        gameObject.SetActive(true);
    }

    private void Hide(){
        gameObject.SetActive(false);
    }
}
