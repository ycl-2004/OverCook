using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] Button resumeButton;
    [SerializeField] Button mainmenuButton;

    [SerializeField] Button optionsButton;

    private void Awake() {
        resumeButton.onClick.AddListener(()=>{
            GameManager.Instance.PauseGame();
        });

        mainmenuButton.onClick.AddListener(()=>{
            Loader.Load(Loader.Scene.MainMenu);
        });

        optionsButton.onClick.AddListener(()=>{
            OptionUI.Instance.Show();
        });
    }
    private void Start() {
        GameManager.Instance.OnGamePause += Instance_OnGamePause;
        GameManager.Instance.OnGameUnpause += Instance_OnGameUnpause;
        Hide();
    }

    private void Instance_OnGamePause(object sender, System.EventArgs e){
        Show();
    }

    private void Instance_OnGameUnpause(object sender, System.EventArgs e){
        Hide();
    }
    private void Show(){
        gameObject.SetActive(true);
    }

    private void Hide(){
        gameObject.SetActive(false);
    }
}
