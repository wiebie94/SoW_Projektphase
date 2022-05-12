using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMenuFollowHand : MonoBehaviour
{
    [SerializeField]
    GameObject leftHandRef;
    [SerializeField]
    GameObject rightHandRef;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = leftHandRef.transform.position;
        this.transform.rotation = leftHandRef.transform.rotation;
        
    }



}
