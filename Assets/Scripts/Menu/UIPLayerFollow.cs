using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPLayerFollow : MonoBehaviour
{
    [SerializeField] Transform player;

    [SerializeField] float updateInterval = 0.1f;
    Coroutine updateCoroutine;

    [SerializeField] Vector3 offset = new Vector3(5, 0, 0);

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player is null!");
        }
    }

    private IEnumerator PositionUpdate()
    {
        while(true)
        {
            transform.position = player.position + offset;

            yield return new WaitForSeconds(updateInterval);
        }
    }

    public void StartFollowingPlayer()
    {
        if (updateCoroutine != null)
            StopCoroutine(updateCoroutine);

        updateCoroutine = StartCoroutine(PositionUpdate());
    }

   public void StopFollowingPlayer()
    {
        if (updateCoroutine != null)
            StopCoroutine(updateCoroutine);

        updateCoroutine = null;
    }
}
