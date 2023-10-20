using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class NaturalDisasterManager : MonoBehaviour
{
    public static NaturalDisasterManager Instance;
    
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;

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
        if (_cameraDutch == 0)
            _cameraDutch = 180;
        else
            _cameraDutch = 0;
        InputManager.Instance.IsReverse = !InputManager.Instance.IsReverse;

        _cinemachineVirtualCamera.m_Lens.Dutch = _cameraDutch;
    }
}
