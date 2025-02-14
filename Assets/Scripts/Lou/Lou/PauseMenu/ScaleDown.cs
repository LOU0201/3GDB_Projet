using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class ScaleDown : MonoBehaviour
{
    [SerializeField] private float scaleDuration = 1f; 
    [SerializeField] private float delay = 0.5f; 
    [SerializeField] private bool scaleDownOnX = true; 
    private void Start()
    {
        DOVirtual.DelayedCall(delay, StartScaling);
    }

    private void StartScaling()
    {
        if (scaleDownOnX)
        {
            // Scale down on both X and Y axes
            transform.DOScale(Vector3.zero, scaleDuration).SetEase(Ease.Linear);
        }
        else
        {
            // Scale down only on the Y axis
            transform.DOScaleY(0f, scaleDuration).SetEase(Ease.Linear);
        }
    }
}
