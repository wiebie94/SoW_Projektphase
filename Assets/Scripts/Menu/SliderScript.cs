using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SliderScript : MonoBehaviour
{
    private ConfigurableJoint cj;

    private float sliderZLimit;
    [SerializeField] float sliderZLimitCorrection;

    [SerializeField] AudioMixer mixer;

    // Start is called before the first frame update
    void Start()
    {
        cj = GetComponent<ConfigurableJoint>();
        sliderZLimit = cj.linearLimit.limit + sliderZLimitCorrection;

        Debug.Log("limit: " + sliderZLimit);
    }

    private void OnEnable()
    {
        Debug.Log("mixer val: " + GetConvertedValueFromMixer());

        float valueFromMixer = GetConvertedValueFromMixer();

        float valueForSlider = ConvertToSliderValue(valueFromMixer);

        SetLocalZ(valueForSlider);
    }

    private void OnDisable()
    {
        Reset();
    }

    private void OnCollisionStay(Collision collision)
    {
        SetMasterVolume(GetSliderConvertedValue(transform.localPosition.z));
    }

    private float GetSliderConvertedValue(float localX)
    {
        float result = (localX * -1) / sliderZLimit;

        return result;
    }

    private void SetMasterVolume(float value)
    {
        if (mixer == null)
        {
            Debug.LogError("mixer is null!");
            return;
        }

        float valueForMixer;

        valueForMixer = (value - 1) * 15; 

        mixer.SetFloat("MasterVolume", valueForMixer);
    }

    private float GetConvertedValueFromMixer()
    {
        if (mixer == null)
        {
            Debug.LogError("mixer is null!");
            return 0;
        }

        float value;
        mixer.GetFloat("MasterVolume", out value);

        return value / 15 + 1;
    }

    private float ConvertToSliderValue(float value)
    {
        return value * -sliderZLimit;
    }

    private void Reset()
    {
        SetLocalZ(0);
    }

    private void SetLocalZ(float z)
    {
        Vector3 oldPos = transform.localPosition;
        transform.localPosition = new Vector3(oldPos.x, oldPos.y, z);
    }

}
