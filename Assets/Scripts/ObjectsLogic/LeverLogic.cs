using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverLogic : MonoBehaviour
{
    private HingeJoint hingeJoint;

    private const float timeToCheckAfterCollision = 2;
    private const float checkUpdateTime = 0.05f;

    private float time;

    private Coroutine leverCheckCoroutine;

    private float minLimit;
    private float maxLimit;

    private float springDamper;
    private float springForce;

    void Start()
    {
        hingeJoint = GetComponent<HingeJoint>();

        maxLimit = hingeJoint.limits.max;
        minLimit = hingeJoint.limits.min;

        springDamper = hingeJoint.spring.damper;
        springForce = hingeJoint.spring.spring;

        Debug.Log(hingeJoint.limits.max);
    }

    IEnumerator LeverStateCheck()
    {
        time = 0;
        while (time < timeToCheckAfterCollision)
        {
            Debug.Log("Lever update");
            if (hingeJoint.angle > 0 && hingeJoint.spring.targetPosition != maxLimit)
            {
                hingeJoint.spring = CreateSpring(maxLimit);
                Debug.Log("CHange to max");
            }
                
            else if (hingeJoint.angle < 0 && hingeJoint.spring.targetPosition != minLimit)
            {
                hingeJoint.spring = CreateSpring(minLimit);
                Debug.Log("CHange to min");
            }
                

            yield return new WaitForSeconds(checkUpdateTime);
            time += checkUpdateTime;
        }

        leverCheckCoroutine = null;
    }

    private JointSpring CreateSpring(float targetPos)
    {
        JointSpring spring;
        spring.spring = springForce;
        spring.damper = springDamper;
        spring.targetPosition = targetPos;

        return spring;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (leverCheckCoroutine == null)
        {
            leverCheckCoroutine = StartCoroutine(LeverStateCheck());
        }
        else
        {
            time = 0;
        }
    }
}
