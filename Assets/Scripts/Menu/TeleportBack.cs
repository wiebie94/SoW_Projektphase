using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportBack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(SceneChangeStart), 0.5f);
    }

    private IEnumerator TeleportPlayerBack()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Halle 2.0", LoadSceneMode.Single);
        operation.priority = 1;

        yield return new WaitForSeconds(2.7f);
        while (operation.progress < 0.9f)
        {
            yield return new WaitForSeconds(0.05f);
            Debug.Log(operation.progress);
        }
        operation.allowSceneActivation = true;
    }

    void SceneChangeStart()
    {
        StartCoroutine(TeleportPlayerBack());
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
