using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caméra : MonoBehaviour
{
    public Camera cam;
    public GameObject[] Lcam;         // Liste des positions de caméra
    private int index = 0;             // Index actuel de la caméra

    public float moveSmoothTime = 0.3f;  // Temps de lissage pour le déplacement
    public float rotateSmoothTime = 0.2f; // Temps de lissage pour la rotation

    private Vector3 targetPosition;   // Position cible
    private Quaternion targetRotation; // Rotation cible
    private float fixedRotationX;     // Rotation X fixe

    private Vector3 velocity = Vector3.zero; // Vélocité pour SmoothDamp
    private float rotationVelocityY = 0f;    // Vélocité pour SmoothDampAngle (Y)
    private float rotationVelocityZ = 0f;    // Vélocité pour SmoothDampAngle (Z)

    public Transform target;  // Objet que la caméra doit observer
    public float distanceFromTarget = 5f; // Distance de la caméra par rapport à l'objet cible

    void Start()
    {
        fixedRotationX = cam.transform.rotation.eulerAngles.x;

        if (Lcam.Length > 0)
        {
            SetTarget(0);
            cam.transform.position = targetPosition;
            cam.transform.rotation = targetRotation;
        }
    }

    void Update()
    {
        if (target == null) return; // Ne rien faire si aucun objet cible n'est défini

        // Obtenir la rotation actuelle et cible
        Vector3 currentEulerAngles = cam.transform.eulerAngles;
        Vector3 targetEulerAngles = targetRotation.eulerAngles;

        // Garder la rotation X fixe
        targetEulerAngles.x = fixedRotationX;

        // Appliquer une interpolation fluide sur Y et Z
        float smoothY = Mathf.SmoothDampAngle(currentEulerAngles.y, targetEulerAngles.y, ref rotationVelocityY, rotateSmoothTime);
        float smoothZ = Mathf.SmoothDampAngle(currentEulerAngles.z, targetEulerAngles.z, ref rotationVelocityZ, rotateSmoothTime);

        // Appliquer la rotation lissée
        cam.transform.rotation = Quaternion.Euler(fixedRotationX, smoothY, smoothZ);

        // **Repositionnement immédiat** après la rotation pour que l'objet reste au centre
        Vector3 offset = cam.transform.forward * -distanceFromTarget;
        cam.transform.position = target.position + offset;

        // Changer de caméra avec Q et E
        if (Input.GetKeyDown(KeyCode.E))
        {
            index = (index - 1 + Lcam.Length) % Lcam.Length;
            SetTarget(index);
            FMODUnity.RuntimeManager.PlayOneShot("event:/V1/System/movecam");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            index = (index + 1) % Lcam.Length;
            SetTarget(index);
            FMODUnity.RuntimeManager.PlayOneShot("event:/V1/System/movecam");
        }
    }

    private void SetTarget(int targetIndex)
    {
        targetRotation = Lcam[targetIndex].transform.rotation;
    }
}
