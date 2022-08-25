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

    [SerializeField] bool isForPlayerHeight = false;
    [SerializeField] PlayerController player;

    private float playerHeigthRange = 0.65f;

    [SerializeField] GameSave gameSave;

    // Start is called before the first frame update
    void Start()
    {
        cj = GetComponent<ConfigurableJoint>();
        sliderZLimit = cj.linearLimit.limit + sliderZLimitCorrection;
    }

    private void OnEnable()
    {
        StartCoroutine(LateOnEnable());
    }

    IEnumerator LateOnEnable()
    {
        yield return new WaitForSeconds(0.1f);

        float valueForSlider;

        if (isForPlayerHeight)
        {
            Debug.Log("player height : " + GetConvertedValueFromPlayerHeight());
            float valueFromPlayerHeight = GetConvertedValueFromPlayerHeight();
            valueForSlider = ConvertToSliderValue(valueFromPlayerHeight);
        }
        else
        {
            Debug.Log("mixer val: " + GetConvertedValueFromMixer());

            float valueFromMixer = GetConvertedValueFromMixer();
            valueForSlider = ConvertToSliderValue(valueFromMixer);
        }

        SetLocalZ(valueForSlider);
    }

    private void OnDisable()
    {
        Reset();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (isForPlayerHeight)
            SetPlayerHeight(GetSliderConvertedValue(transform.localPosition.z));
        else
            SetMasterVolume(GetSliderConvertedValue(transform.localPosition.z));
    }

    private float GetSliderConvertedValue(float localX)
    {
        float result = (localX * -1) / sliderZLimit;

        return result;
    }

    private void SetPlayerHeight(float value)
    {
        if (player == null)
        {
            Debug.LogError("No player set!");
            return;
        }

        float valueForHeight;

        valueForHeight = value * playerHeigthRange;

        player.SetPlayerHeight(valueForHeight);
        gameSave.getGameData().playerHeight = valueForHeight;
    }

    private void SetMasterVolume(float value)
    {
        if (mixer == null || gameSave == null)
        {
            Debug.LogError("mixer or gameSave is null!");
            return;
        }

        float valueForMixer;

        valueForMixer = (value - 1) * 15; 

        mixer.SetFloat("MasterVolume", valueForMixer);
        gameSave.getGameData().volume = valueForMixer;
    }

    private float GetConvertedValueFromMixer()
    {
        if (mixer == null)
        {
            Debug.LogError("mixer not set!");
            return 0;
        }

        float value;
        mixer.GetFloat("MasterVolume", out value);

        return value / 15 + 1;
    }

    private float GetConvertedValueFromPlayerHeight()
    {
        if (player == null)
        {
            Debug.LogError("player not set!");
            return 0;
        }

        return player.GetPlayerHeight() / playerHeigthRange;
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
