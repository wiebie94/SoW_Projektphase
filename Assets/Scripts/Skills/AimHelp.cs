using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimHelp : MonoBehaviour
{
    // Start is called before the first frame update
    public float range;
    public GameObject leftHandRef;
    public GameObject rightHandRef;
    public ShowSkillMenu skillMenu;
    void Start()
    {

        this.gameObject.transform.localScale = new Vector3(0.1f, 10.0f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (skillMenu.menuHandLeft)
        {
            this.transform.position = leftHandRef.transform.forward * range;
        }
        else
        {
            this.transform.position = rightHandRef.transform.forward * range;
        }
        
    }
}
