using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        _audio.volume = _minVolume;
        _audio.Play();
        StartCoroutine(SoundLerp(_minVolume, _maxVolume));
        _spriteRenderer.color = _alarmColor;
    }

    public void Disable()
    {
        StartCoroutine(DisableCorutine());
        _spriteRenderer.color = _defaultColor;
    }

    private IEnumerator SoundLerp(float start, float finish)
    {
        float runingTime = 0f;

        while (runingTime <= _duration)
        {
            runingTime += Time.deltaTime;
            _audio.volume = Mathf.Lerp(start, finish, runingTime / _duration);
            yield return null;
        }
    }

    private IEnumerator DisableCorutine()
    {
        yield return StartCoroutine(SoundLerp(_maxVolume, _minVolume));
        _audio.Stop();
    }

    private void OnValidate()
    {
        if(_minVolume >= _maxVolume)
        {
            _minVolume = _maxVolume - 0.01f;
        }
    }
}