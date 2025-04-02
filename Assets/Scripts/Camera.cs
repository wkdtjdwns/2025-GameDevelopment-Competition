using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public static Camera instance;

    private void Awake()
    {
        instance = this;
    }

    private Vector3 initialOffset;
    private float shakeMagnitude;
    private int shakeTime;

    private void Start()
    {
        initialOffset = transform.position - transform.parent.position;
    }

    private void Update()
    {
        if (shakeTime > 0)
        {
            shakeTime--;
            transform.position = transform.parent.position + initialOffset + Random.insideUnitSphere * shakeMagnitude;
        }
        else
        {
            shakeTime = 0;
            transform.position = transform.parent.position + initialOffset;
        }
    }

    // 진동을 시작하는 메소드
    public void Shake(float _shakeMagnitude, int _shakeTime)
    {
        shakeMagnitude = _shakeMagnitude;
        shakeTime = _shakeTime;
    }
}
