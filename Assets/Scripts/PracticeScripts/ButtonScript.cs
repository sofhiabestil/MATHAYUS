using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour{
    
    public GameObject HintPanel;
   
    public void OpenHintPanel(){

        if(HintPanel != null){
            
            bool isActive  = HintPanel.activeSelf;
            
            HintPanel.SetActive(!isActive);
        }
    }

}
