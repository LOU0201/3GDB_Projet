using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class ScaleDown : MonoBehaviour
{
    [SerializeField] private float scaleDuration = 1f;
    [SerializeField] private float Delay = 0.5f; // Add a delay variable

    private void Start()
    {
        // Use DOVirtual.DelayedCall to add a delay
        DOVirtual.DelayedCall(Delay, StartScaling);
    }

    private void StartScaling()
    {
        // Start the scaling animation
        transform.DOScale(Vector3.zero, scaleDuration).SetEase(Ease.Linear);
        //or
        //transform.DOScaleX(0f, scaleDuration).SetEase(Ease.Linear);
        //transform.DOScaleY(0f, scaleDuration).SetEase(Ease.Linear);
    }
}
