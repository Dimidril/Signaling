using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Detection : MonoBehaviour
{
    [SerializeField] private UnityEvent _alarm;
    [SerializeField] private UnityEvent _noAlarm;

    private bool _isActive;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Rogue rogue))
        {
            if (_isActive)
            {
                _noAlarm?.Invoke();
                _isActive = false;
            }
            else
            {
                _alarm?.Invoke();
                _isActive = true;
            }
        }
    }
}