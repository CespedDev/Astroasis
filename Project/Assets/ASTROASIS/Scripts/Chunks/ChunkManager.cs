using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> chunkPrefabList = new List<GameObject>(); 
    [SerializeField] private Transform  spawnPoint; 
    [SerializeField] private int        poolSize = 5; 
    [SerializeField] private float      chunkSpeed = 4.0f; 

    // Pool de chunks
    private Queue<GameObject> chunkPool = new Queue<GameObject>(); 


    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            int rnd = Random.Range(0, chunkPrefabList.Count);

            GameObject chunk = Instantiate(chunkPrefabList[rnd], 
                new Vector3(spawnPoint.position.x,spawnPoint.position.y,spawnPoint.position.z + 5.45f), 
                Quaternion.identity);
            chunk.GetComponent<ChunkMovement>().SetChunkSpeed(chunkSpeed);

            chunk.SetActive(false);
            chunkPool.Enqueue(chunk);
        }
    }

    public void OnChunkTriggerActivated()
    {
        SpawnChunk();
    }

    // Método para activar un chunk en la posición de spawnPoint
    private void SpawnChunk()
    {
        if (chunkPool.Count > 0)
        {
            GameObject chunk = chunkPool.Dequeue();
            chunk.transform.position = spawnPoint.position;
            //Debug.Log($"x- {spawnPoint.position.x},y- {spawnPoint.position.y},z- {spawnPoint.position.z}");
            chunk.SetActive(true);
            chunkPool.Enqueue(chunk);
        }
    }

    // Método para desactivar y devolver a la pool el chunk
    public void RecycleChunk(GameObject chunk)
    {
        chunk.SetActive(false);
        chunk.transform.position = Vector3.zero;
    }
}
