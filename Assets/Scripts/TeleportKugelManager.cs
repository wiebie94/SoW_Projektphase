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
    [SerializeField] private int maxCounter = 1;
    private int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.creatKugel(this.transform.position); // testen
    }
    public bool isCreatable()
    {
        return this.counter < this.maxCounter;
    }

    //ToDo überarbeiten für Menü später
    public bool creatKugel(Vector3 position) {
        return this.kugelFactory(position) != null;
    }

    public GameObject creatKugel()
    {
        return this.kugelFactory(new Vector3(0, 0, 0));
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
