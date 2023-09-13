using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private Vector2 _parallaxRatio;

    private Transform _mainCamTrm;
    private Vector3 _lastCamPosition;
    private bool _isActive = false;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _mainCamTrm = CameraManager.MainCam.transform;
        _lastCamPosition = _mainCamTrm.position;
        _isActive = true;
    }

    private void LateUpdate()
    {
        if (!_isActive)
            return;
        
        Vector3 deltaMove = _mainCamTrm.position - _lastCamPosition;
        transform.position += new Vector3(deltaMove.x * _parallaxRatio.x, deltaMove.y * _parallaxRatio.y);
        _lastCamPosition = _mainCamTrm.position;
    }
}