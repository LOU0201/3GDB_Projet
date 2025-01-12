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

    private float x1, y1, z1;
    private float x2, y2, z2;
    private float x3, y3, z3;
    private float x4, y4, z4;

    [MenuItem("Outils/Gestionnaire de Caméras")]
    public static void ShowWindow()
    {
        GetWindow(typeof(GestionCam));
    }

    private void OnGUI()
    {
        GUILayout.Label("Positions de la caméra", EditorStyles.boldLabel);

        pos1 = EditorGUILayout.ObjectField("Objet 1", pos1, typeof(GameObject), true) as GameObject;
        if (pos1 != null)
        {
            HandleObjectPosition(ref pos1, ref x1, ref y1, ref z1);
        }

        pos2 = EditorGUILayout.ObjectField("Objet 2", pos2, typeof(GameObject), true) as GameObject;
        if (pos2 != null)
        {
            HandleObjectPosition(ref pos2, ref x2, ref y2, ref z2);
        }

        pos3 = EditorGUILayout.ObjectField("Objet 3", pos3, typeof(GameObject), true) as GameObject;
        if (pos3 != null)
        {
            HandleObjectPosition(ref pos3, ref x3, ref y3, ref z3);
        }

        pos4 = EditorGUILayout.ObjectField("Objet 4", pos4, typeof(GameObject), true) as GameObject;
        if (pos4 != null)
        {
            HandleObjectPosition(ref pos4, ref x4, ref y4, ref z4);
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
}
