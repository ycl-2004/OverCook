using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI recipesDeliveredText;

    private void Start() {
        GameManager.Instance.OnStateChangedd += Instance_StateChange;
        Hide();
    }

    private void Instance_StateChange(object sender, System.EventArgs e){
        if(GameManager.Instance.isGameOver()){
            Show();
            recipesDeliveredText.text = DeliveryManager.Instance.DeliveredAmout().ToString();
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
