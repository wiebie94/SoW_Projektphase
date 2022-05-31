using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public ParticleSystem[] particleSystems;

    public void Update() {
        if(Input.GetKey("a")){
            this.activateParticles();
        }
        if(Input.GetKey("d")){
            this.deactivateParticles();
        }
    }

    public void activateParticles() {
        foreach(ParticleSystem ps in particleSystems) {
            var main = ps.main;
            main.loop = true;
            ps.Play();
        }
    }

    public void deactivateParticles() {
        foreach(ParticleSystem ps in particleSystems) {
            var main = ps.main;
            main.loop = false;
        }
    }
}
