using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportBack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(TeleportPlayerBack), 2);
    }

    private void TeleportPlayerBack()
    {
        SceneManager.LoadScene("Halle 2.0", LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
