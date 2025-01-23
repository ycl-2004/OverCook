using TMPro;
using UnityEditor;
using UnityEngine;

public class CountDownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countDownText;

    private Animator animator;
    private int previousCountDownNum;

    private void Awake() {
        animator = GetComponent<Animator>();
    }
    private void Start() {
        GameManager.Instance.OnStateChangedd += Instance_StateChange;
        Hide();
    }


    private void Update() {
        int countdownNum = Mathf.CeilToInt(GameManager.Instance.GetCountDownTime());
        //countDownText.text = GameManager.Instance.GetCountDownTime().ToString("F1");
        countDownText.text = countdownNum.ToString();

        if(previousCountDownNum!=countdownNum){
            previousCountDownNum = countdownNum;
            animator.SetTrigger("numberPopup");
            SoundManager.Instance.PlayCountDownSound();
        }
    }
    private void Instance_StateChange(object sender, System.EventArgs e){
        if(GameManager.Instance.isCountDownToStart()){
            Show();
            //Debug.Log("SHOW");
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
