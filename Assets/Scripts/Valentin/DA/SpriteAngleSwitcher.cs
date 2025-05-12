using UnityEngine;

public class SpriteAngleSwitcher : MonoBehaviour
{
    public Sprite[] directionalSprites; // Ordre : [Front, Right, Back, Left]
    public SpriteRenderer spriteRenderer;

    public Camera mainCamera;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;

            if (mainCamera == null)
                Debug.LogWarning("Aucune caméra principale trouvée dans la scène.");
        }

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector3 directionToCamera = mainCamera.transform.position - transform.position;
        directionToCamera.y = 0; // Ignore la hauteur

        float angle = Vector3.SignedAngle(Vector3.forward, directionToCamera, Vector3.up);

        int index = GetDirectionIndex(angle);

        if (index >= 0 && index < directionalSprites.Length)
        {
            spriteRenderer.sprite = directionalSprites[index];
        }
    }

    int GetDirectionIndex(float angle)
    {
        angle = (angle + 360f) % 360f; // Normalise entre 0 et 360

        if (angle >= 315f || angle < 45f)
        {
            Debug.Log("Front");
            return 0;
        }
        else if (angle >= 45f && angle < 135f)
        {
            Debug.Log("Right");
            return 1;
        }
        else if (angle >= 135f && angle < 225f)
        {
            Debug.Log("Back");
            return 2;
        }
        else if (angle >= 225f && angle < 315f)
        {
            Debug.Log("Left");
            return 3;
        }

        return 0; // Fallback
    }
}
