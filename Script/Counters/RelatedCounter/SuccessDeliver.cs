using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SuccessDeliver : MonoBehaviour
{
    const string POP_STRING = "Popup";
    [Header("Components")]
    [SerializeField] private Image backgroundimg;
    [SerializeField] private Image iconimg;
    [SerializeField] private TextMeshProUGUI deliverText;
    [SerializeField] Color successColor;
    [SerializeField] Color failColor;
    [SerializeField] Sprite successSprite;
    [SerializeField] Sprite failSprite;

    [Header("Animator")]
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }
    private void Start() {
        DeliveryManager.Instance.OnRecipeSuccess += Success;
        DeliveryManager.Instance.OnRecipefailed += Failed;

        gameObject.SetActive(false);
    }

    private void Success(object sender, System.EventArgs e){
        gameObject.SetActive(true);
        animator.SetTrigger(POP_STRING);
        backgroundimg.color = successColor;
        iconimg.sprite = successSprite;
        deliverText.text = "DELIVERY\nSuccess";
    }

    private void Failed(object sender, System.EventArgs e){
        gameObject.SetActive(true);
        animator.SetTrigger(POP_STRING);
        backgroundimg.color = failColor;
        iconimg.sprite = failSprite;
        deliverText.text = "DELIVERY\nFAILED";
    }
}

