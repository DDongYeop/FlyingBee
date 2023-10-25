using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class NaturalDisasterManager : MonoBehaviour
{
    public static NaturalDisasterManager Instance;
    
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
    [SerializeField] private float _rotationTime = 0.5f;

    [Header("Sound")] 
    [SerializeField] private AudioSource _typhoonSound;

    private bool _isTyphoon = false;
    private int _cameraDutch = 0;
    
    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("Multiple NaturalDisasterManager is running");
        Instance = this;
    }

    [ContextMenu("Typhoon")]
    public void Typhoon()
    {
        if (_isTyphoon)
            return;

        _isTyphoon = true;
        StartCoroutine(TyphoonCo());
    }

    private IEnumerator TyphoonCo()
    {
        UIManager.Instance.TyhoonWarning();
        CameraManager.Instance.CameraShake(25,2.25f);
        _typhoonSound.Play();
        
        yield return new WaitForSeconds(1.2f);

        float startIndex = 0;
        float endIndex = 180;
        float currentTime = 0;

        while (currentTime < _rotationTime)
        {
            yield return null;
            currentTime += Time.deltaTime;

            float time = currentTime / _rotationTime;
            _cinemachineVirtualCamera.m_Lens.Dutch = Mathf.Lerp(startIndex, endIndex, time);
        }
        
        InputManager.Instance.IsReverse = !InputManager.Instance.IsReverse;
        yield return new WaitForSeconds(0.75f);
        _typhoonSound.Stop();
    }
    
    
}
