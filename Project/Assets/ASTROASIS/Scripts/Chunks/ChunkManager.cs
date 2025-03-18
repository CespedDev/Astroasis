using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChunkManager : MonoBehaviour
{
    [Header("Chunks Prefabs")]
    [SerializeField] private List<GameObject> initChunkPrefabList = new List<GameObject>();
    [SerializeField] private List<GameObject> levelChunks   = new List<GameObject>();

    [Header("System Settings")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int       poolSize         = 5;
    [SerializeField] private float     chunkSpeed     = 4.0f;
    [SerializeField] private int       chunksDisplayed  = 3;
    [SerializeField] private float     deleteDistance = 1.5f;

    // Chunks Pool
    private Queue<GameObject> chunkPool      = new Queue<GameObject>();
    private List<GameObject>  activeChunksList = new List<GameObject>();

    private bool chunksMoving = false;
    private int  indexLevelChunks     = 0;

    void Start()
    {
        CreateChunksPool();
    }

    void Update()
    {
        ManageChunksState();
    }

    /// <summary>
    /// Reset and start a new chunks pool, then initialize the chunks.
    /// </summary>
    private void CreateChunksPool()
    {
        DeleteChunksPool();

        for (int i = 0; i < poolSize && i < levelChunks.Count; i++)
        {
            CreateChunk();
        }

        for (int i = 0; i < chunksDisplayed; ++i)
        {
            EnableChunk();
        }
    }

    private void DeleteChunksPool()
    {
        indexLevelChunks = 0;

        while (chunkPool.Count > 0)
        {
            Destroy(chunkPool.Dequeue());
        }

        while (activeChunksList.Count > 0)
        {
            Destroy(activeChunksList[0]);
        }
    }

    private void CreateChunk()
    {
        if (indexLevelChunks < levelChunks.Count)
        {
            GameObject chunk = Instantiate(levelChunks[indexLevelChunks], spawnPoint.position, Quaternion.identity);
            SceneManager.MoveGameObjectToScene(chunk, gameObject.scene);

            chunk    .GetComponent<ChunkMovement>().SetChunkSpeed(chunkSpeed);
            chunk    .SetActive(false);
            chunkPool.Enqueue(chunk);

            indexLevelChunks++;
        }
    }

    private void ManageChunksState()
    {
        if (activeChunksList.Count > 0 &&
            activeChunksList[0].transform.position.z < spawnPoint.position.z - deleteDistance)
        {
            DisableChunk();
        }
    }

    /// <summary>
    /// Make the next chunk visible and load a new chunk to the pool.
    /// </summary>
    private void EnableChunk()
    {
        if (chunkPool.Count > 0)
        {
            GameObject chunk = chunkPool.Dequeue();
            
            CreateChunk();

            if (activeChunksList.Count > 0)
            {
                GameObject lastChunk = activeChunksList[activeChunksList.Count - 1];

                /**
                 *  C.Cabrera (13/11/2024): I really like the idea, but the problem is that we work with a modular system, so no mesh renderer
                 *  has the actual size of a chunk, which means it might not work with the real chunks. Pending confirmation.
                 */
                MeshRenderer lastChunkRenderer = lastChunk.GetComponentInChildren<MeshRenderer>();

                if (lastChunkRenderer != null)
                {
                    //float chunkLength = lastChunkRenderer.bounds.size.z;
                    float chunkLength = 32;

                    /** 
                     * C.Cabrera (30/12/2024): Thats not correct, your are only taking aware of the last Zsize but you need to know de Zsize of 
                     * the new object and add both half values.
                     */
                    Vector3 newPosition = lastChunk.transform.position + new Vector3(0, 0, chunkLength);
                    chunk.transform.position = newPosition;
                }
                else
                {
                    /**
                     * C.Cabrera (30/12/2024): This case never happen. If this happen, is only due to an error.
                     */
                    // Si no tiene MeshRenderer, usa la posición actual (caso de primer chunk generado)
                    chunk.transform.position = spawnPoint.position;
                }
            }
            else
            {
                chunk.transform.position = spawnPoint.position;
            }

            chunk.GetComponentInChildren<ChunkMovement>().SetChunkMovement(chunksMoving);
            chunk.SetActive(true);
            activeChunksList.Add(chunk);
        }
    }

    // Método para desactivar y devolver a la pool el chunk
    private void DisableChunk()
    {
        Destroy(activeChunksList[0]);
        activeChunksList.Remove(activeChunksList[0]);

        EnableChunk();
    }

    public void SetMovementOfActiveChunks(bool state)
    {
        chunksMoving = state;

        foreach (var chunk in activeChunksList)
        {
            chunk.GetComponent<ChunkMovement>().SetChunkMovement(state);
        }

        foreach (var chunk in chunkPool)
        {
            chunk.GetComponent<ChunkMovement>().SetChunkMovement(state);
        }
    }
}
