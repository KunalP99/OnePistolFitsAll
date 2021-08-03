using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    // Can use this script in any other script without having to reference it
    public static CameraShake Instance { get; private set; }

    private CinemachineVirtualCamera vCamera;
    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntensity;

    void Awake()
    {
        Instance = this;
        vCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        // Get the cinemachine componenet of type CinemachineBasicMultiChannelPerlin and store it in a varaible
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = vCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        // Change the intensity of shake by modifying AmplitudeGain variable value
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;

        startingIntensity = intensity;
        shakeTimerTotal = time;
        shakeTimer = time;
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;

            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = vCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            // Goes from starting intensity to 0 to smoothly go from the camera shake to idle 
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, shakeTimer / shakeTimerTotal);
        }
    }
}
