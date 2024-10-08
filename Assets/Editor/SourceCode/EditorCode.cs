using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorCode : EditorWindow
{
    string Nom = "";
    GameObject objet;
    float x = 0;
    float y = 0;
    float z = 0;
    Material texture;
    [MenuItem("Outils/Editeur de niveau")]
    // Start is called before the first frame update
    public static void ShowWindow()
    {
        GetWindow(typeof(EditorCode));
    }

    // Update is called once per frame
    private void OnGUI()
    {
        GUILayout.Label("Créateur d'objets", EditorStyles.boldLabel);

        Nom = EditorGUILayout.TextField("Nom", Nom);
        objet = EditorGUILayout.ObjectField("Type d'objet", objet, typeof(GameObject), false) as GameObject;
        x = EditorGUILayout.FloatField("Position x", x);
        y = EditorGUILayout.FloatField("Position y", y);
        z = EditorGUILayout.FloatField("Position z", z);

        if (GUILayout.Button("Créer objet"))
        {
            SpawnObject();
        }
    }
    private void SpawnObject()
    {
        if (objet == null)
        {
            Debug.LogError("Erreur : Aucun objet référencé");
            return;
        }
        
        if (Nom == null)
        {
            Debug.LogError("Erreur : Nom de l'objet requis");
            return;
        }
        Vector3 SpawnPos = new Vector3(x, y, z);

        GameObject NewObject = Instantiate(objet, SpawnPos, Quaternion.identity);
        NewObject.name = Nom;
    }
}
