using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject cubePrefab; // The cube prefab to spawn
    public float spawnInterval = 2f; // Time between spawns
    public int gridSize = 10; // The size of the spawning area

    public List<Transform> prePlannedCubes; // List of pre-planned cubes

    private List<Vector3> occupiedPositions = new List<Vector3>(); // List to keep track of occupied positions

    void Start()
    {
        // Add all pre-planned cube positions to the occupied list
        foreach (Transform prePlannedCube in prePlannedCubes)
        {
            if (prePlannedCube != null)
            {
                occupiedPositions.Add(prePlannedCube.position);
            }
        }

        // Start the spawning process
        StartCoroutine(SpawnCube());
    }

    IEnumerator SpawnCube()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Try to find a valid position to spawn the cube
            Vector3 spawnPosition = FindValidSpawnPosition();

            if (spawnPosition != Vector3.zero)
            {
                // Spawn the cube and mark the position as occupied
                GameObject newCube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);
                occupiedPositions.Add(spawnPosition);

                // Notify CubeDestroyer about the new cube
                FindObjectOfType<CubeDestroy>().AddCube(newCube);

            }
        }
    }

    Vector3 FindValidSpawnPosition()
    {
        List<Vector3> potentialPositions = new List<Vector3>();

        // Start from all pre-planned cubes (or any other occupied cube)
        foreach (Vector3 occupiedPosition in occupiedPositions)
        {
            // Check possible positions next to the occupied cube (on X and Z axes)
            Vector3[] nearbyPositions = new Vector3[]
            {
                occupiedPosition + new Vector3(1, 0, 0), // +X
                occupiedPosition + new Vector3(-1, 0, 0), // -X
                occupiedPosition + new Vector3(0, 0, 1), // +Z
                occupiedPosition + new Vector3(0, 0, -1), // -Z
                occupiedPosition + new Vector3(0, 1, 0) // Stack on top (Y axis)
            };

            foreach (Vector3 pos in nearbyPositions)
            {
                // Only consider positions above ground level, not occupied, and within the grid size
                if (!occupiedPositions.Contains(pos) && pos.y >= 0)
                {
                    potentialPositions.Add(pos);
                }
            }
        }

        // If we found valid positions, pick one at random
        if (potentialPositions.Count > 0)
        {
            return potentialPositions[Random.Range(0, potentialPositions.Count)];
        }

        // If no valid positions were found, return Vector3.zero (indicating no spawn)
        return Vector3.zero;
    }
}
