using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantDeath : MonoBehaviour
{
    Animator animator;
    public float deathDuration;
    public GameObject fireParticles;
    public GameObject firePosition;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        firePosition = this.gameObject.transform.GetChild(2).gameObject;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Fireball")
        {
            animator.SetBool("Dying", true);
            StartCoroutine("PlantDeathAnimation");
            // particle effect start
            GameObject fire = Instantiate(fireParticles, firePosition.transform, false);
            //fire.transform.localScale = new Vector3(100, 100, 100);
        }
    }

    IEnumerator PlantDeathAnimation()
    {
        yield return new WaitForSeconds(deathDuration);
        // dissolve shader start
        this.gameObject.SetActive(false);
    }
}
