using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{

    public static OptionUI Instance{get; private set;}

    [SerializeField] Button soundEffectButton;
    [SerializeField] TextMeshProUGUI soundEffectText;
    [SerializeField] Button musicButton;
    [SerializeField] TextMeshProUGUI musicText;
    [SerializeField] Button closeButton;

    [Header("Control Options")]
    [SerializeField] Button MoveupButton;
    [SerializeField] TextMeshProUGUI MoveupText;
    [SerializeField] Button MovedownButton;
    [SerializeField] TextMeshProUGUI MovedownText;
    [SerializeField] Button MoveleftButton;
    [SerializeField] TextMeshProUGUI MoveleftText;
    [SerializeField] Button MoverightButton;
    [SerializeField] TextMeshProUGUI MoverightText;
    [SerializeField] Button InteractButton;
    [SerializeField] TextMeshProUGUI InteractText;
    [SerializeField] Button InteractAltButton;
    [SerializeField] TextMeshProUGUI InteractAltText;
    [SerializeField] Button PauseButton;
    [SerializeField] TextMeshProUGUI PauseText;

    [Header("Rebind window")]
    [SerializeField] Transform rebindWindow;

    private void Awake() {
        Instance = this;

        soundEffectButton.onClick.AddListener(()=>{
            SoundManager.Instance.ChangeVol();
            updateVisual();
        });

        musicButton.onClick.AddListener(()=>{
            MusicManager.Instance.ChangeVol();
            updateVisual();
        });

        closeButton.onClick.AddListener(()=>{
            Hide();
        });

        MoveupButton.onClick.AddListener(()=>{
            RebindBindingFunction(GameInput.Binding.Moveup);
        });

        MovedownButton.onClick.AddListener(()=>{
            RebindBindingFunction(GameInput.Binding.Movedown);
        });

        MoveleftButton.onClick.AddListener(()=>{
            RebindBindingFunction(GameInput.Binding.Moveleft);
        });

        MoverightButton.onClick.AddListener(()=>{
            RebindBindingFunction(GameInput.Binding.Moveright);
        });

        InteractButton.onClick.AddListener(()=>{
            RebindBindingFunction(GameInput.Binding.Interaction);
        });

        InteractAltButton.onClick.AddListener(()=>{
            RebindBindingFunction(GameInput.Binding.InteractAlt);
        });

        PauseButton.onClick.AddListener(()=>{
            RebindBindingFunction(GameInput.Binding.Pausion);
        });
        
    }

    private void Start() {
        GameManager.Instance.OnGameUnpause += Instance_Unpause;
        updateVisual();

        Hide();
        HideRebind();
    }

    private void updateVisual(){
        soundEffectText.text = "Sound Effects: " +  Mathf.Round(SoundManager.Instance.GetVol()*10f);
        musicText.text = "Music: " +  Mathf.Round(MusicManager.Instance.GetVol()*10f);

        MoveupText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Moveup);
        MovedownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Movedown);
        MoveleftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Moveleft);
        MoverightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Moveright);
        InteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interaction);
        InteractAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlt);
        PauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pausion);
    }

    public void Show(){
        gameObject.SetActive(true);
    }

    public void Hide(){
        gameObject.SetActive(false);
    }

    private void Instance_Unpause(object sender, System.EventArgs e){
        Hide();
    }

    public void ShowRebind(){
        rebindWindow.gameObject.SetActive(true);
    }

    public void HideRebind(){
        rebindWindow.gameObject.SetActive(false);
    }

    private void RebindBindingFunction(GameInput.Binding binding){
        ShowRebind();

        GameInput.Instance.RebindBinding(binding,()=>{
            HideRebind();
            updateVisual();
        });
    }
}


