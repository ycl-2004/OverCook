using UnityEngine.UI;
using UnityEngine;

public class GameClockUI : MonoBehaviour
{
    [SerializeField] Image timerimg;

    private void Update() {
        timerimg.fillAmount = GameManager.Instance.GetGameTimeNormalized();
    }
}
