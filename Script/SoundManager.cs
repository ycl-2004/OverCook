using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const string Player_Pref_SoundEffecet_Vol = "SoundEffectVolume";
    public static SoundManager Instance{get; private set;}
    [SerializeField] SoundSO soundSO;

    private float volume = 1f;
    private void Awake() {
        Instance = this;

        volume = PlayerPrefs.GetFloat(Player_Pref_SoundEffecet_Vol,1f);
    }
    private void Start() {
        DeliveryManager.Instance.OnRecipeSuccess += Instace_SuccessSound;
        DeliveryManager.Instance.OnRecipefailed += Instace_FailedSound;
        CuttingCounter.OnAnyCut += Instance_OnAnyCut;
        Player.Instance.OnPickAny += Instance_OnPickAny;
        BaseCounter.OnAnyObjPlace += Instance_OnAnyObjPlace;
        TrashCounter.OnAnyTrash += Instance_OnAnyTrash;
    }

    private void Instace_SuccessSound(object sender, System.EventArgs e){
        //PlaySound();
        DeliverCounter deliverCounter = DeliverCounter.Instance;
        PlaySound(soundSO.deliverySuccess,deliverCounter.transform.position);
    }

    private void Instace_FailedSound(object sender, System.EventArgs e){
        //PlaySound();
        DeliverCounter deliverCounter = DeliverCounter.Instance;
        PlaySound(soundSO.deliveryFailed,deliverCounter.transform.position);
    }

    private void Instance_OnAnyCut(object sender, System.EventArgs e){
        //PlaySound();
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(soundSO.chop,cuttingCounter.transform.position);
    }

    private void Instance_OnPickAny(object sender, System.EventArgs e){
        //PlaySound();
        PlaySound(soundSO.objecetPickup,Player.Instance.transform.position);
    }

    
    private void Instance_OnAnyObjPlace(object sender, System.EventArgs e){
        //PlaySound();
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(soundSO.objectDrop,baseCounter.transform.position);
    }

    private void Instance_OnAnyTrash(object sender, System.EventArgs e){
        //PlaySound();
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(soundSO.trash,trashCounter.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float vol = 1f){
       AudioSource.PlayClipAtPoint(audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)],position,vol*volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float vol = 1f){
        AudioSource.PlayClipAtPoint(audioClip,position,vol*volume);
    }

    public void PlayFootStepSound(Vector3 pos,float vol=1f){
        PlaySound(soundSO.footstep,pos,vol*volume);
    }
    public void PlayCountDownSound(){
        PlaySound(soundSO.warning,Vector3.zero);
    }

    public void PlayWarningSound(Vector3 pos){
        PlaySound(soundSO.warning,pos);
    }

    public void ChangeVol(){
        volume +=0.1f;

        if(volume>1f){
            volume = 0;
        }

        PlayerPrefs.SetFloat(Player_Pref_SoundEffecet_Vol,volume);
        PlayerPrefs.Save();
    }

    public float GetVol(){
        return volume;
    }
}
