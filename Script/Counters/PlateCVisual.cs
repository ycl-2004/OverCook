using System.Collections.Generic;
using UnityEngine;

public class PlateCVisual : MonoBehaviour
{
    [SerializeField] Transform topPos;
    [SerializeField] Transform plateVisual;

    [SerializeField] PlateCounter plateCounter;

    private List<GameObject> plateVisualList;

    private void Awake() {
        plateVisualList = new List<GameObject>();
    }
    private void Start() {
        plateCounter.OnPlateSpawned += PlateSpawned;
        plateCounter.OnPlateTaken += PlateRemoved;
    }

    private void PlateSpawned(object sender, System.EventArgs e){
        Transform plateVisualTrans  = Instantiate(plateVisual,topPos);

        float plateOffsetY = 0.1f;
        plateVisualTrans.localPosition = new Vector3(0,plateOffsetY*plateVisualList.Count,0);

        plateVisualList.Add(plateVisualTrans.gameObject);
    }

    private void PlateRemoved(object sender, System.EventArgs e){
        GameObject plataGameobject = plateVisualList[plateVisualList.Count -1];
        plateVisualList.Remove(plataGameobject);

        Destroy(plataGameobject);
    }
}
