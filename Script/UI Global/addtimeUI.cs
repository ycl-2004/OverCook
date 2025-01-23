using TMPro;
using UnityEngine;

public class addtimeUI : MonoBehaviour
{
    Animator animator;

    const string addtime_string = "addtime";

    [SerializeField] TextMeshProUGUI addtimeTimeText;
    private void Awake() {
        animator = GetComponent<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DeliveryManager.Instance.OnTimeAdded += timeAddedVisual;
        Hide();
    }

    private void timeAddedVisual(object sender, System.EventArgs e){
        Show();
        addtimeTimeText.text = "+ " + DeliveryManager.Instance.getAddedTime() + " sec";
        animator.SetTrigger(addtime_string);
    }

    public void Show(){
        gameObject.SetActive(true);
    }

    public void Hide(){
        gameObject.SetActive(false);
    }
}
