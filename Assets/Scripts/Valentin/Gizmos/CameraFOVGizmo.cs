using UnityEngine;

public class CameraFOVGizmo : MonoBehaviour
{
    public Camera targetCamera; // Caméra dont le FOV sera représenté
    [HideInInspector]
    public bool affichage;

    void OnDrawGizmos()
    {
        if (targetCamera == null)
        {
            //Debug.LogWarning("Aucune caméra référencée !");
            return;
        }

        // Utiliser la position de l'objet portant le script et la rotation Y
        Vector3 position = transform.position;
        Quaternion rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

        // Ajouter la rotation X et Z de la caméra référencée à l'objet
        rotation *= Quaternion.Euler(targetCamera.transform.eulerAngles.x, 0, targetCamera.transform.eulerAngles.z);

        if (affichage == true)
        {
            DrawCameraFOV(targetCamera, position, rotation);
        }
    }

    void DrawCameraFOV(Camera cam, Vector3 position, Quaternion rotation)
    {
        Gizmos.color = Color.cyan;

        // Obtenir les paramètres de la caméra
        float fov = cam.fieldOfView;
        float aspectRatio = cam.aspect;
        float nearClip = cam.nearClipPlane;
        float farClip = cam.farClipPlane;

        // Calculer les dimensions du FOV
        float halfHeightNear = Mathf.Tan(fov * 0.5f * Mathf.Deg2Rad) * nearClip;
        float halfWidthNear = halfHeightNear * aspectRatio;
        float halfHeightFar = Mathf.Tan(fov * 0.5f * Mathf.Deg2Rad) * farClip;
        float halfWidthFar = halfHeightFar * aspectRatio;

        // Calculer les coins des plans de coupe
        Vector3 forward = rotation * Vector3.forward;
        Vector3 right = rotation * Vector3.right;
        Vector3 up = rotation * Vector3.up;

        Vector3 topLeftNear = position + forward * nearClip - right * halfWidthNear + up * halfHeightNear;
        Vector3 topRightNear = position + forward * nearClip + right * halfWidthNear + up * halfHeightNear;
        Vector3 bottomLeftNear = position + forward * nearClip - right * halfWidthNear - up * halfHeightNear;
        Vector3 bottomRightNear = position + forward * nearClip + right * halfWidthNear - up * halfHeightNear;

        Vector3 topLeftFar = position + forward * farClip - right * halfWidthFar + up * halfHeightFar;
        Vector3 topRightFar = position + forward * farClip + right * halfWidthFar + up * halfHeightFar;
        Vector3 bottomLeftFar = position + forward * farClip - right * halfWidthFar - up * halfHeightFar;
        Vector3 bottomRightFar = position + forward * farClip + right * halfWidthFar - up * halfHeightFar;

        // Dessiner les lignes du FOV
        Gizmos.DrawLine(topLeftNear, topRightNear);
        Gizmos.DrawLine(topRightNear, bottomRightNear);
        Gizmos.DrawLine(bottomRightNear, bottomLeftNear);
        Gizmos.DrawLine(bottomLeftNear, topLeftNear);

        Gizmos.DrawLine(topLeftFar, topRightFar);
        Gizmos.DrawLine(topRightFar, bottomRightFar);
        Gizmos.DrawLine(bottomRightFar, bottomLeftFar);
        Gizmos.DrawLine(bottomLeftFar, topLeftFar);

        Gizmos.DrawLine(topLeftNear, topLeftFar);
        Gizmos.DrawLine(topRightNear, topRightFar);
        Gizmos.DrawLine(bottomLeftNear, bottomLeftFar);
        Gizmos.DrawLine(bottomRightNear, bottomRightFar);
    }
}
