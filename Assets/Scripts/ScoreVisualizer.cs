using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreVisualizer : MonoBehaviour
{   
    public Image OneKo;
    public Image TwoKo;
    public Image ThreeKo;
    public void SetScore(int score){
        switch (score)
        {
            case 1:
                OneKo.enabled = true;
                break;
            case 2:
                TwoKo.enabled = true;
                break;
            case 3:
                ThreeKo.enabled = true;
                break;
            case 0:
                ThreeKo.enabled = false;
                ThreeKo.enabled = false;
                ThreeKo.enabled = false;
                break;
            default:
                break;
        }
    }

    GameObject GetChildWithName(GameObject obj, string name) {
        Transform trans = obj.transform;
        Transform childTrans = trans. Find(name);
        if (childTrans != null) {
            return childTrans.gameObject;
        } else {
            return null;
        }
    }

    void Start(){
        OneKo.enabled = false;
        TwoKo.enabled = false;
        ThreeKo.enabled = false;
    }
}
