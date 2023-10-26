using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn : MonoBehaviour
{
    private Animator _animator;
    private int _isShowHash = Animator.StringToHash("IsShow");
    private bool _isShow = false;
    private AudioSource _audioSource;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (_isShow)
        {
            _audioSource.Play();
            UIManager.Instance.UIActive(true, (int)UIType.FAIL);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        
        if (!_isShow)
        {
            _audioSource.Play();
            _isShow = true;
            _animator.SetTrigger(_isShowHash);
        }
    }
}
