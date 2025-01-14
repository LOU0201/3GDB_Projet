using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor
{
    public class ListeTool : EditorWindow
    {
        Grille_3d ScriptListe;
        
        private int targetInstanceID;
        
        bool ListeToolMode = false;
        bool EnableNormalBlocs = true;
        bool EnableBlockBlocs = false;
        bool EnableHoles = true;


        private void Update()
        {
            
            
            
            // if (EnableBlockBlocs)
            // {
            //     ScriptListe.Non_Blockeur = true;
            // }
        }
        

        [MenuItem("Outils/ListeTool")]
        private static void ShowWindow()
        {
            var window = GetWindow<ListeTool>();
            window.titleContent = new GUIContent("ListeTool");
            window.Show();
        }
        
        private void OnEnable()
        {
            // Restore the target object after play mode
            if (targetInstanceID != 0)
            {
                ScriptListe = EditorUtility.InstanceIDToObject(targetInstanceID) as Grille_3d;
            }
        }
        
        private void OnDisable()
        {
            // Save the target's instance ID
            if (ScriptListe != null)
            {
                targetInstanceID = ScriptListe.GetInstanceID();
            }
        }
        
        private void OnGUI()
        {
            
            EditorGUILayout.LabelField("Toggles");
            ListeToolMode = EditorGUILayout.Toggle("Enable liste tool mode", ListeToolMode);
            EnableNormalBlocs = EditorGUILayout.Toggle("Enable normal blocs", EnableNormalBlocs);
            //ScriptListe.Non_Blockeur = EditorGUILayout.Toggle("Enable bloqueur blocs", ScriptListe.Non_Blockeur);
            EnableHoles = EditorGUILayout.Toggle("Enable trous", EnableHoles);
            
            ScriptListe = (Grille_3d)EditorGUILayout.ObjectField("Target Script", ScriptListe, typeof(Grille_3d), true);

            if (ScriptListe != null)
            {
                targetInstanceID = ScriptListe.GetInstanceID();
                // Display a toggle to modify the bool
                EnableBlockBlocs = EditorGUILayout.Toggle("Enable bloqueur blocs", ScriptListe.Non_Blockeur);
                
                if (EnableBlockBlocs != ScriptListe.Non_Blockeur)
                {
                    Undo.RecordObject(ScriptListe, "Modify My Bool"); // Add to Undo
                    ScriptListe.Non_Blockeur = EnableBlockBlocs;               // Update the bool
                    EditorUtility.SetDirty(ScriptListe);             // Mark as dirty for saving
                }
            }

            if (GUILayout.Button("Clear all blocs & holes"))
            {
                GameObject[] boxes = GameObject.FindGameObjectsWithTag("boite");

                foreach (GameObject box in boxes)
                {
                    // Get the Renderer component
                    Renderer renderer = box.GetComponent<Renderer>();

                    // Check if the Renderer exists and its material color is yellow
                    if (renderer != null && renderer.material.color == Color.yellow)
                    {
                        Destroy(box); // Destroy the GameObject
                    }
                }
            }

            if (GUILayout.Button("Clear normal blocs"))
            {
                GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("boite");

                foreach (GameObject obj in taggedObjects)
                {
                    // Check if this GameObject has a child with white material
                    Transform whiteChild = FindWhiteMaterialChild(obj.transform);

                    if (whiteChild != null)
                    {
                        // Modify the script attached to the parent
                        ModifyScript(obj);

                        // Set the white child to inactive
                        whiteChild.gameObject.SetActive(false);
                    }
                }
            }

            Transform FindWhiteMaterialChild(Transform parent)
            {
                // Check each child recursively
                foreach (Transform child in parent)
                {
                    Renderer childRenderer = child.GetComponent<Renderer>();
                    if (childRenderer != null && childRenderer.material.color == Color.white)
                    {
                        return child; // Return the child with white material
                    }

                    Transform foundInChild = FindWhiteMaterialChild(child); // Recursive call
                    if (foundInChild != null)
                    {
                        return foundInChild;
                    }
                }

                return null;
            }

            if (GUILayout.Button("Clear blocking blocs"))
            {
                GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("boite");

                foreach (GameObject obj in taggedObjects)
                {
                    // Check if this GameObject has a child with yellow material
                    Transform yellowChild = FindYellowMaterialChild(obj.transform);

                    if (yellowChild != null)
                    {
                        // Modify the script attached to the parent
                        ModifyScript(obj);

                        // Set the yellow child to inactive
                        yellowChild.gameObject.SetActive(false);
                    }
                }
            }

            Transform FindYellowMaterialChild(Transform parent)
            {
                // Check each child recursively
                foreach (Transform child in parent)
                {
                    Renderer childRenderer = child.GetComponent<Renderer>();
                    if (childRenderer != null && childRenderer.material.color == Color.yellow)
                    {
                        return child; // Return the child with yellow material
                    }

                    Transform foundInChild = FindYellowMaterialChild(child); // Recursive call
                    if (foundInChild != null)
                    {
                        return foundInChild;
                    }
                }

                return null;
            
            }

            if (GUILayout.Button("Clear holes blocs"))
            {
                
            }

            if (GUILayout.Button("Restart level"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            
            void ModifyScript(GameObject obj)
            {
                // Get the custom script attached to the object
                var scriptBoite = obj.GetComponent<Boite>();
                if (scriptBoite != null)
                {
                    scriptBoite.libre = true; // Enable one bool
                    scriptBoite.Stop = false; // Disable another bool
                }
            }
        }
    }
}