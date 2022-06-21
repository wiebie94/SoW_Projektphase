using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartForce : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private Vector3 startForce;
    [SerializeField] private bool isRandom;
    [SerializeField] private Vector3 randomRange;
    // Start is called before the first frame update

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        if(isRandom)
            rb.AddForce(Vector3.right * Random.Range(0, this.randomRange.x) + Vector3.up * Random.Range(0, this.randomRange.y) + Vector3.forward * Random.Range(0, this.randomRange.z), ForceMode.Impulse);
        else
            rb.AddForce(startForce , ForceMode.Impulse);
    }
}
