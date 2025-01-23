using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private const string Player_Pref_Music_Vol = "MusicVolume";
    public static MusicManager Instance{get; private set;}
    private float volume = 0.3f;

    private AudioSource audioSource;

    private void Awake() {
        Instance = this;
        audioSource = GetComponent<AudioSource>();

        volume = PlayerPrefs.GetFloat(Player_Pref_Music_Vol,0.3f);
        audioSource.volume = volume;
    }
    private void Update() {
        
    }
    public void ChangeVol(){
        volume +=0.1f;

        if(volume>1f){
            volume = 0;
        }

        audioSource.volume = volume;

        PlayerPrefs.SetFloat(Player_Pref_Music_Vol,volume);
        PlayerPrefs.Save();
    }

    public float GetVol(){
        return volume;
    }
}
