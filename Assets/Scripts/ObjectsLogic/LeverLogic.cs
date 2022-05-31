using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeverLogic : MonoBehaviour
{
    private HingeJoint hingeJoint;

    private const float checkUpdateTime = 0.2f;

    private float minLimit;
    private float maxLimit;

    private float springDamper;
    private float springForce;

    [SerializeField] bool upOnStart;

    [SerializeField] UnityEvent eventUp;
    [SerializeField] UnityEvent eventDown;

    void Start()
    {
        hingeJoint = GetComponent<HingeJoint>();

        maxLimit = hingeJoint.limits.max;
        minLimit = hingeJoint.limits.min;

        springDamper = hingeJoint.spring.damper;
        springForce = hingeJoint.spring.spring;

        if (upOnStart)
            hingeJoint.spring = CreateSpring(maxLimit);
        else
            hingeJoint.spring = CreateSpring(minLimit);

        StartCoroutine(LeverStateCheck());
    }

    IEnumerator LeverStateCheck()
    {
        while (true)
        {
            if (hingeJoint.angle > 0 && hingeJoint.spring.targetPosition != maxLimit)
            {
                hingeJoint.spring = CreateSpring(maxLimit);
                eventUp.Invoke();
            }
                
            else if (hingeJoint.angle < 0 && hingeJoint.spring.targetPosition != minLimit)
            {
                hingeJoint.spring = CreateSpring(minLimit);
                eventDown.Invoke();
            }
                

            yield return new WaitForSeconds(checkUpdateTime);
        }
    }

    private JointSpring CreateSpring(float targetPos)
    {
        JointSpring spring;
        spring.spring = springForce;
        spring.damper = springDamper;
        spring.targetPosition = targetPos;

        return spring;
    }
}
