using UnityEngine;

public class SelectedCVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObject;
    private void Start() {
      Player.Instance.OnSelectCounterC += Player_OnSelectCounterChange;
    }

    private void Player_OnSelectCounterChange(object sender, Player.OnSelectCounterCEventArgs e){
      if(e.selectedCounter == baseCounter){
        Show();
      }else{
        Hide();
      }
    }

    private void Show(){
      foreach (GameObject gameObject in visualGameObject){
        gameObject.SetActive(true);
      }
    }

    private void Hide(){
      foreach (GameObject gameObject in visualGameObject){
        gameObject.SetActive(false);
      }
    }
}
