using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [SerializeField] private CinemachineVirtualCamera _cmVcam;

    private CinemachineBasicMultiChannelPerlin _noise = null;

    private void Awake() 
    {
        if (Instance == null)
            Instance = this;
        else 
            Destroy(gameObject);
            
        _noise = _cmVcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>(); 
    } 
    
    public void CameraShake(float amplitude, float duration)
    {
        StopAllCoroutines();
        StartCoroutine(ShakeCoroutine(amplitude, duration));
    }

    private IEnumerator ShakeCoroutine(float amplitude, float duration)
    {
        float time = duration;
        while (time > 0)
        {
            _noise.m_AmplitudeGain = Mathf.Lerp(0, amplitude, time / duration);

            yield return null;
            time -= Time.deltaTime;
        }
        _noise.m_AmplitudeGain = 0;
    }
}