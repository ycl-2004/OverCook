//using Microsoft.Unity.VisualStudio.Editor;
using System;
using UnityEngine;
using UnityEngine.UI;
public class CutProgressUI : MonoBehaviour
{
    [SerializeField] private GameObject hasprogress;
    private IHasProgress ihasprogress;  
    [SerializeField] private Image img;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start() {
        ihasprogress = hasprogress.GetComponent<IHasProgress>();
        if(ihasprogress == null){
            Debug.Log("null");
        }
        ihasprogress.Progresses += OnCutAction;
        img.fillAmount = 0;
        Hide();
    }


    private void OnCutAction(object sender, IHasProgress.onProgressesEventArgs e){
        img.fillAmount = e.progressNormalized;
        if(e.progressNormalized ==0f||e.progressNormalized==1){
            Hide();
        }else{
            Show();
        }
    }

    private void Show(){
        gameObject.SetActive(true);
    }

    private void Hide(){
        gameObject.SetActive(false);
    }
}
