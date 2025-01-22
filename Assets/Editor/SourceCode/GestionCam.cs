using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GestionCam : EditorWindow
{
    GameObject pos1;
    GameObject pos2;
    GameObject pos3;
    GameObject pos4;
    GameObject fond;
    Material texture;

    private float x1, y1, z1;
    private float x2, y2, z2;
    private float x3, y3, z3;
    private float x4, y4, z4;

    private bool FOV1;
    private bool FOV2;
    private bool FOV3;
    private bool FOV4;

    [MenuItem("Outils/Gestionnaire de Caméras")]
    public static void ShowWindow()
    {
        GetWindow(typeof(GestionCam));
    }

    private void OnGUI()
    {
        GUILayout.Label("Positions de la caméra", EditorStyles.boldLabel);

        pos1 = EditorGUILayout.ObjectField("position 1 :", pos1, typeof(GameObject), true) as GameObject;
        if (pos1 != null)
        {
            var cameraFOVGizmo1 = pos1.GetComponent<CameraFOVGizmo>();
            if (cameraFOVGizmo1 != null)
            {
                FOV1 = EditorGUILayout.Toggle("FOV :", cameraFOVGizmo1.affichage);
                if (FOV1 != cameraFOVGizmo1.affichage)
                {
                    cameraFOVGizmo1.affichage = FOV1;
                    EditorUtility.SetDirty(cameraFOVGizmo1); // Marquer le composant comme modifié pour que Unity prenne en compte le changement
                }
            }
            HandleObjectPosition(ref pos1, ref x1, ref y1, ref z1);
        }

        pos2 = EditorGUILayout.ObjectField("position 2 :", pos2, typeof(GameObject), true) as GameObject;
        if (pos2 != null)
        {
            var cameraFOVGizmo2 = pos2.GetComponent<CameraFOVGizmo>();
            if (cameraFOVGizmo2 != null)
            {
                FOV2 = EditorGUILayout.Toggle("FOV :", cameraFOVGizmo2.affichage);
                if (FOV2 != cameraFOVGizmo2.affichage)
                {
                    cameraFOVGizmo2.affichage = FOV2;
                    EditorUtility.SetDirty(cameraFOVGizmo2);
                }
            }
            HandleObjectPosition(ref pos2, ref x2, ref y2, ref z2);
        }

        pos3 = EditorGUILayout.ObjectField("position 3 :", pos3, typeof(GameObject), true) as GameObject;
        if (pos3 != null)
        {
            var cameraFOVGizmo3 = pos3.GetComponent<CameraFOVGizmo>();
            if (cameraFOVGizmo3 != null)
            {
                FOV3 = EditorGUILayout.Toggle("FOV :", cameraFOVGizmo3.affichage);
                if (FOV3 != cameraFOVGizmo3.affichage)
                {
                    cameraFOVGizmo3.affichage = FOV3;
                    EditorUtility.SetDirty(cameraFOVGizmo3);
                }
            }
            HandleObjectPosition(ref pos3, ref x3, ref y3, ref z3);
        }

        pos4 = EditorGUILayout.ObjectField("position 4 :", pos4, typeof(GameObject), true) as GameObject;
        if (pos4 != null)
        {
            var cameraFOVGizmo4 = pos4.GetComponent<CameraFOVGizmo>();
            if (cameraFOVGizmo4 != null)
            {
                FOV4 = EditorGUILayout.Toggle("FOV :", cameraFOVGizmo4.affichage);
                if (FOV4 != cameraFOVGizmo4.affichage)
                {
                    cameraFOVGizmo4.affichage = FOV4;
                    EditorUtility.SetDirty(cameraFOVGizmo4);
                }
            }
            HandleObjectPosition(ref pos4, ref x4, ref y4, ref z4);
        }

        fond = EditorGUILayout.ObjectField("fond du niveau :", fond, typeof(GameObject), true) as GameObject;
        texture = EditorGUILayout.ObjectField("texture du fond :", texture, typeof(Material), true) as Material;

        if (GUILayout.Button("appliquer texture"))
        {
            ChangerFond();
        }

        if (GUILayout.Button("Défaire"))
        {
            // PerformUndo();
        }
    }

    private void HandleObjectPosition(ref GameObject obj, ref float x, ref float y, ref float z)
    {
        x = EditorGUILayout.FloatField("Position x :", obj.transform.position.x);
        y = EditorGUILayout.FloatField("Position y :", obj.transform.position.y);
        z = EditorGUILayout.FloatField("Position z :", obj.transform.position.z);

        if (GUI.changed)
        {
            // Enregistrer l'état de l'objet pour l'undo
            Undo.RecordObject(obj.transform, "Move Object");

            // Appliquer la nouvelle position
            obj.transform.position = new Vector3(x, y, z);

            // Marquer la scène comme ayant été modifiée
            EditorUtility.SetDirty(obj);
        }
    }

    private void ChangerFond()
    {
        fond.GetComponent<Renderer>().material = texture;

        if (GUI.changed)
        {
            // Enregistrer l'état de l'objet pour l'undo
            Undo.RecordObject(fond.GetComponent<Renderer>().material= texture, "Change Background");

            // Marquer la scène comme ayant été modifiée
            EditorUtility.SetDirty(texture);
        }
    }
}
