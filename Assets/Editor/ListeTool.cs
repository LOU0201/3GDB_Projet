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
        
        bool EnableBlocks = true;
        bool ToggleBlockType = false;
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
            
            GUILayout.Label("Toggles", EditorStyles.boldLabel);
            
            EnableBlocks = EditorGUILayout.Toggle("Enable Blocks", EnableBlocks);
            
            EnableHoles = EditorGUILayout.Toggle("Enable Holes", EnableHoles);
            
            ToggleBlockType = EditorGUILayout.Toggle("Toggle Block Type", ToggleBlockType);
                        if (!ToggleBlockType)
                        {
                            EditorGUILayout.LabelField("Active blocks : Blocking blocks");
                        }
                        else
                        {
                            EditorGUILayout.LabelField("Active blocks : Normal blocks");
                        }
                        
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            
            GUILayout.Label("Cleanup", EditorStyles.boldLabel);
            
            #region BlockingToggleOLD
            // ScriptListe = (Grille_3d)EditorGUILayout.ObjectField("Target Script", ScriptListe, typeof(Grille_3d), true);
            //
            // if (ScriptListe != null)
            // {
            //     targetInstanceID = ScriptListe.GetInstanceID();
            //     // Display a toggle to modify the bool
            //     EnableBlockBlocs = EditorGUILayout.Toggle("Enable bloqueur blocs", ScriptListe.Non_Blockeur);
            //     
            //     if (EnableBlockBlocs != ScriptListe.Non_Blockeur)
            //     {
            //         Undo.RecordObject(ScriptListe, "Modify My Bool"); // Add to Undo
            //         ScriptListe.Non_Blockeur = EnableBlockBlocs;               // Update the bool
            //         EditorUtility.SetDirty(ScriptListe);             // Mark as dirty for saving
            //     }
            // }
            #endregion

            #region BlockingToggle
            GameObject grille = GameObject.FindGameObjectWithTag("grille");

            if (grille != null)
            {
                Grille_3d scriptGrille = grille.GetComponent<Grille_3d>();
                if (scriptGrille != null)
                {
                    scriptGrille.Non_Blockeur = ToggleBlockType;
                }
            }
            #endregion
            
            #region ToggleHoles
            
            GameObject baseObject = GameObject.FindGameObjectWithTag("base");

            if (baseObject != null)
            {
                // Iterate through all children of the "base" object
                foreach (Transform child in baseObject.transform)
                {
                    // Check if the child has the desired script
                    sol childScript = child.GetComponent<sol>();
                    if (childScript != null)
                    {
                        // Set the bool in the child's script based on this script's toggleValue
                        childScript.enableHoles = EnableHoles;
                    }
                }
            }
            else
            {
                Debug.LogWarning("No object tagged 'base' found in the scene.");
            }
            
            #endregion

            #region EnableBlocks

            GameObject grilleblocks = GameObject.FindGameObjectWithTag("grille");

            if (grilleblocks != null)
            {
                Grille_3d scriptGrille = grilleblocks.GetComponent<Grille_3d>();
                if (scriptGrille != null)
                {
                    scriptGrille.EnableBoites = EnableBlocks;
                }
            }

            #endregion

            #region ClearBlocksHolesButton
            if (GUILayout.Button("Clear Everything"))
            {
               
            }
            #endregion

            #region ClearNormalBlocksButton
            if (GUILayout.Button("Clear Normal Blocks"))
            {
                GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("boite");

                foreach (GameObject obj in taggedObjects)
                {
                    // Check if this GameObject has a child with white material
                    Transform whiteChild = FindWhiteMaterialChild(obj.transform);

                    if (whiteChild != null)
                    {
                        // Modify the script attached to the parent
                        EditBlockBools(obj);

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
            #endregion

            #region ClearBlockingBlocksButton
            if (GUILayout.Button("Clear Blocking Blocks"))
            {
                GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("boite");

                foreach (GameObject obj in taggedObjects)
                {
                    // Check if this GameObject has a child with yellow material
                    Transform yellowChild = FindYellowMaterialChild(obj.transform);

                    if (yellowChild != null)
                    {
                        // Modify the script attached to the parent
                        EditBlockBools(obj);

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
            #endregion

            #region ClearHolesButton
            if (GUILayout.Button("Clear Holes"))
            {
                GameObject ground = GameObject.FindGameObjectWithTag("base");

                if (ground != null)
                {
                    // Activate all children of the "base" object
                    foreach (Transform child in ground.transform)
                    {
                        child.gameObject.SetActive(true);
                    }
                }
                else
                {
                    Debug.LogWarning("No object tagged 'base' found in the scene.");
                }
            }
            #endregion

            #region RestartLevelButton
            if (GUILayout.Button("Restart level"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            #endregion
            
            void EditBlockBools(GameObject obj)
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