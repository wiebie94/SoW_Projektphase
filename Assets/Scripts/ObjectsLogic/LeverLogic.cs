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

    bool open = false;

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
            hingeJoint.spring = CreateSpring(0.0f);

        StartCoroutine(LeverStateCheck());
    }

    IEnumerator LeverStateCheck()
    {
        while (true)
        {
            if (hingeJoint.angle > 30f)
            {
                if(!open)
                {
                    open = true;
                    eventUp.Invoke();                    
                }
                if (open)
                {
                    hingeJoint.spring = CreateSpring(maxLimit);
                    yield return new WaitForSeconds(0.3f);
                }                                
            }

            else if (hingeJoint.angle < -30f)
            {
                if(open)
                {
                    open = false;
                    eventDown.Invoke();
                }
                if (!open)
                {
                    hingeJoint.spring = CreateSpring(minLimit);
                    yield return new WaitForSeconds(checkUpdateTime);
                }                    
            }

            else
            {
                hingeJoint.spring = CreateSpring(0.0f);
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
    public void SetToClosed()
    {
        if (!this.gameObject.activeSelf)
        {
            return;
        }
        hingeJoint.spring = CreateSpring(minLimit);
        open = false;
    }
}
