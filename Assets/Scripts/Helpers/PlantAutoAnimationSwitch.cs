using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantAutoAnimationSwitch : MonoBehaviour
{
    public float switchToOneAttackAfterSeconds;
    public float switchToOneAttackAfterSecondsRangeMod;

    private Animator animator;
    private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        this.animator = this.gameObject.GetComponent<Animator>();
        this.audio = this.gameObject.GetComponent<AudioSource>();
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
            this.audio.Play();
            yield return new WaitForSeconds(1.9f);
            this.audio.Stop();
            this.audio.Play();
            yield return new WaitForSeconds(0.5f);
            this.audio.Stop();
        }
    }
}
