using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class TextFX : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI targetText; // TMP text to animate
    [SerializeField] private float animationDuration = 2f; // How long the animation will play
    [SerializeField] private float intensity = 1f; // Intensity for the animations
    [SerializeField] private AnimationType animationType = AnimationType.None; // Type of animation to play

    public enum AnimationType
    {
        None,
        Bouncing,
        Wiggle,
        Rotating,
        Wavy
    }

    private void Start()
    {
        // Play the selected animation on start
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        if (targetText == null) return;

        // Use the text's transform for animations (except for "Wavy")
        Transform targetTransform = targetText.transform;

        switch (animationType)
        {
            case AnimationType.None:
                break;

            case AnimationType.Bouncing:
                targetTransform.DOJump(targetTransform.position, intensity, 1, animationDuration);
                break;

            case AnimationType.Wiggle:
                targetTransform.DOShakePosition(animationDuration, new Vector3(intensity, intensity, 0f), vibrato: 10, randomness: 90);
                break;

            case AnimationType.Rotating:
                targetTransform.DORotate(new Vector3(0f, 0f, 360f), animationDuration, RotateMode.FastBeyond360);
                break;

            case AnimationType.Wavy:
                PlayWavyEffect();
                break;
        }
    }

    private void PlayWavyEffect()
    {
        if (targetText == null) return;

        // Get the text's information
        TMP_TextInfo textInfo = targetText.textInfo;
        targetText.ForceMeshUpdate();

        int characterCount = textInfo.characterCount;

        if (characterCount == 0) return;

        // Apply a wavy effect to each character individually
        for (int i = 0; i < characterCount; i++)
        {
            if (!textInfo.characterInfo[i].isVisible) continue;

            // Get the character's transform
            int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;
            int vertexIndex = textInfo.characterInfo[i].vertexIndex;

            Vector3[] vertices = textInfo.meshInfo[materialIndex].vertices;

            // Animate each character bouncing on the Y-axis
            Vector3 originalPosition = vertices[vertexIndex];
            float delay = i * 0.05f; // Offset each character slightly for a wave effect
            DOTween.To(() => vertices[vertexIndex],
                       pos => {
                           vertices[vertexIndex] = pos;
                           vertices[vertexIndex + 1] = pos + new Vector3(0, intensity, 0);
                           vertices[vertexIndex + 2] = pos + new Vector3(0, intensity, 0);
                           vertices[vertexIndex + 3] = pos;
                       },
                       originalPosition + new Vector3(0, intensity, 0), animationDuration
            ).SetEase(Ease.InOutSine).SetLoops(1, LoopType.Restart)
                       ;
        }
    }
}
