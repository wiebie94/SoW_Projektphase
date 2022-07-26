using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantAutoAnimationSwitch : MonoBehaviour
{
    public float switchToOneAttackAfterSeconds;
    public float switchToOneAttackAfterSecondsRangeMod;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        this.animator = this.gameObject.GetComponent<Animator>();
        StartCoroutine("TimedPlantAnimatorSwitch");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TimedPlantAnimatorSwitch()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(this.switchToOneAttackAfterSeconds - this.switchToOneAttackAfterSecondsRangeMod, this.switchToOneAttackAfterSeconds + this.switchToOneAttackAfterSecondsRangeMod));
            this.animator.SetTrigger("TriggerBite");
        }
    }
}
