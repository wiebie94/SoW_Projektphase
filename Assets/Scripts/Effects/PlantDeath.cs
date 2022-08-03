using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantDeath : MonoBehaviour
{
    Animator animator;
    public float deathDuration;
    public GameObject fireParticles;
    public GameObject firePosition;
    bool burning;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        firePosition = this.gameObject.transform.GetChild(2).gameObject;
        burning = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Fireball" && !burning)
        {
            animator.SetBool("Dying", true);
            StartCoroutine("PlantDeathAnimation");
            // particle effect start
            GameObject fire = Instantiate(fireParticles, firePosition.transform, false);
            fire.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            burning = true;
        }
    }

    IEnumerator PlantDeathAnimation()
    {
        yield return new WaitForSeconds(deathDuration);
        // dissolve shader start
        this.gameObject.transform.GetChild(1).gameObject.GetComponent<PlantDissolve>().DissolvePlant();
        yield return new WaitForSeconds(1f);
        this.gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Stop();
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
    }
}
