using System;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keyeMoveupText;
    [SerializeField] private TextMeshProUGUI keyeMovedownText;
    [SerializeField] private TextMeshProUGUI keyeMoveleftText;
    [SerializeField] private TextMeshProUGUI keyeMoverightText;
    [SerializeField] private TextMeshProUGUI keyeInteractText;
    [SerializeField] private TextMeshProUGUI keyeInteractAltText;
    [SerializeField] private TextMeshProUGUI keyePauseText;

    private void Start() {
        GameInput.Instance.OnRebindAction += Instance_Onrebind;
        GameManager.Instance.OnStateChangedd += Instance_HideTutorial;

        updateVisual();
        Show();
    }

    private void Instance_Onrebind(object sender, System.EventArgs e){
        updateVisual();
    }
    private void updateVisual(){
        
        keyeMoveupText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Moveup);
        keyeMovedownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Movedown);
        keyeMoveleftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Moveleft);
        keyeMoverightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Moveright);
        keyeInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interaction);
        keyeInteractAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlt);
        keyePauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pausion);
    }

    public void Show(){
        gameObject.SetActive(true);
    }

    public void Hide(){
        gameObject.SetActive(false);
    }

    private void Instance_HideTutorial(object sender,System.EventArgs e){
        if(GameManager.Instance.isCountDownToStart()){
            Hide();
        }
    }
}
