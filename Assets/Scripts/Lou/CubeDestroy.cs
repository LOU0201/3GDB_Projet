using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDestroy : MonoBehaviour
{
    public float destroyInterval = 5f; // Time between destruction
    public Transform player; // Reference to the player
    private List<GameObject> spawnedCubes = new List<GameObject>(); // List to track all spawned cubes

    void Start()
    {
        // Start the destruction process
        StartCoroutine(DestroyRandomCube());
    }

    // This function will be called from the CubeSpawner when a new cube is spawned
    public void AddCube(GameObject cube)
    {
        spawnedCubes.Add(cube); // Add the cube to the list
    }

    IEnumerator DestroyRandomCube()
    {
        while (true)
        {
            yield return new WaitForSeconds(destroyInterval);

            // Find a cube to destroy
            GameObject cubeToDestroy = FindCubeToDestroy();

            if (cubeToDestroy != null)
            {
                // Notify CubeSpawner to remove the position of the destroyed cube
                

                spawnedCubes.Remove(cubeToDestroy); // Remove the cube from the list
                Destroy(cubeToDestroy); // Destroy the cube
            }
        }
    }

    GameObject FindCubeToDestroy()
    {
        List<GameObject> validCubes = new List<GameObject>();

        foreach (GameObject cube in spawnedCubes)
        {
            if (cube != null)
            {
                // Ensure the cube is not the one where the player is standing
                if (cube.transform.position != player.position)
                {
                    validCubes.Add(cube);
                }
            }
        }

        if (validCubes.Count > 0)
        {
            return validCubes[Random.Range(0, validCubes.Count)];
        }

        return null; // No valid cubes to destroy
    }
}
