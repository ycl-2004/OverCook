using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private void Awake() {
        playButton.onClick.AddListener(() =>{
            //click
            //SceneManager.LoadScene(1);
            Loader.Load(Loader.Scene.GameScenes);
        });

        quitButton.onClick.AddListener(() =>{
            //click
            Application.Quit();
        });

        Time.timeScale = 1;
    }
}
