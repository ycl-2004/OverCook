using System;
using UnityEngine;

public class StoveSound : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] CookCounter cookCounter;

    float WarningSoundTimer;
    bool playWarningSound;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        cookCounter.OnstateChanged += StoveSoundChange;
        cookCounter.Progresses += ProgressChange;
    }

    private void Update() {

        if(playWarningSound){
            WarningSoundTimer -= Time.deltaTime;
            if(WarningSoundTimer<0f){
                WarningSoundTimer = 0.2f;

                SoundManager.Instance.PlayWarningSound(cookCounter.transform.position);
            }
        }
    }

    private void StoveSoundChange(object sender, CookCounter.OnstateChangedEventArgs e){
        bool playSound = e.state == CookCounter.State.cook || e.state == CookCounter.State.cooked;

        if(playSound){
            audioSource.Play();
        }else{
            audioSource.Pause();
        }

    }

    private void ProgressChange(object sender, IHasProgress.onProgressesEventArgs e){
        float BurnShowProgressAmount = 0.5f;
        playWarningSound = e.progressNormalized >=BurnShowProgressAmount && cookCounter.isCooked();
    }

    
}

