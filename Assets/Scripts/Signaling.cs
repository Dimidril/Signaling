using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Signaling : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color _alarmColor;
    [SerializeField] [Range(0f, 1f)] private float _minVolume, _maxVolume;
    [SerializeField] private float _duration;

    private Color _defaultColor;

    private void Start()
    {
        _defaultColor = _spriteRenderer.color;
    }

    public void Enable()
    {
        StartCoroutine(EnableCorutine());
        _spriteRenderer.color = _alarmColor;
    }

    public void Disable()
    {
        StartCoroutine(DisableCorutine());
        _spriteRenderer.color = _defaultColor;
    }

    private IEnumerator EnableCorutine()
    {
        float runingTime = 0f;
        _audio.volume = _minVolume;
        _audio.Play();

        while (runingTime <= _duration)
        {
            runingTime += Time.deltaTime;
            _audio.volume = Mathf.Lerp(_minVolume, _maxVolume, runingTime / _duration);
            yield return null;
        }
    }

    private IEnumerator DisableCorutine()
    {
        float runingTime = 0f;

        while (runingTime <= _duration)
        {
            runingTime += Time.deltaTime;
            _audio.volume = Mathf.Lerp(_maxVolume, _minVolume, runingTime / _duration);
            if(runingTime >= _duration)
                _audio.Stop();
            yield return null;
        }
    }

    private void OnValidate()
    {
        if(_minVolume >= _maxVolume)
        {
            _minVolume = _maxVolume - 0.01f;
        }
    }
}