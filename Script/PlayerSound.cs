using UnityEngine;

public class PlayerSound : MonoBehaviour
{

    Player player;
    [SerializeField] float footstepTimer;
    [SerializeField] float footstepTimerMax = 0.1f;
    [SerializeField] float footstepVol = 1f;
    private void Awake() {
        player = GetComponent<Player>();
    }

    private void Update() {
        footstepTimer -= Time.deltaTime;
        if(footstepTimer<0f){
            footstepTimer = footstepTimerMax;


            if(player.isWalking()){
                SoundManager.Instance.PlayFootStepSound(player.transform.position,footstepVol);
            }
        }
    }
}
