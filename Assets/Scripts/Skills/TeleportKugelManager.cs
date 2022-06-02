using System.Collections;
using System.Collections.Generic;
using UnityEngine;


interface IKugelDestroy
{
    void isDestroyed();
}
public class TeleportKugelManager : MonoBehaviour, IKugelDestroy
{

    [SerializeField] private GameObject myPrefab;
    [SerializeField] private bool test;
    [SerializeField] private int maxCounter = 1;
    private int counter = 0;

    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;

    // Start is called before the first frame update
    void Start()
    {
        if (test) this.creatKugel(this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(test) this.creatKugel(this.transform.position); // testen
    }
    public bool isCreatable()
    {
        return this.counter < this.maxCounter;
    }

    //ToDo überarbeiten für Menü später
    public GameObject creatKugel(Vector3 position) {
        return this.kugelFactory(position);
    }

    public GameObject creatKugel()
    {
        return this.kugelFactory(this.transform.position);
    }

    private GameObject kugelFactory(Vector3 position)
    {
        if (!this.isCreatable()) return null;
        counter++;
        GameObject gameObject = Instantiate(myPrefab, position, Quaternion.identity);
        return gameObject;
    }

    public void isDestroyed()
    {
        counter--;
    }
}
