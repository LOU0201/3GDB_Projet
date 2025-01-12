using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class ScaleDown : MonoBehaviour
{
    [SerializeField] private float scaleDuration = 1f; // Duration of the scaling animation

    private void Start()
    {
        // Start the scaling animation
        transform.DOScaleX(0f, scaleDuration).SetEase(Ease.Linear);
        transform.DOScaleY(0f, scaleDuration).SetEase(Ease.Linear);
    }
}
