using UnityEngine;

public class ResetStatic : MonoBehaviour
{
    private void Awake() {
        CuttingCounter.ResetStatic();
        BaseCounter.ResetStatic();
        TrashCounter.ResetStatic();
    }
}
