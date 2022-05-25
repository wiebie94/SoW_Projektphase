using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMenuFollowHand : MonoBehaviour
{
    [SerializeField]
    GameObject leftHandRef;
    [SerializeField]
    GameObject rightHandRef;
    [SerializeField]
    ShowSkillMenu sSkillMenu;

    // Update is called once per frame
    void Update()
    {
        if(sSkillMenu.menuHandLeft == true)
        {
            this.transform.position = leftHandRef.transform.position;
            this.transform.rotation = leftHandRef.transform.rotation;
        }
        else
        {
            this.transform.position = rightHandRef.transform.position;
            this.transform.rotation = rightHandRef.transform.rotation;
        }
        
    }



}
