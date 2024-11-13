using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> chunkPrefabList = new List<GameObject>(); 
    [SerializeField] private Transform  spawnPoint; 
    [SerializeField] private Transform  playerPosition;  //
    [SerializeField] private int        poolSize = 5; 
    [SerializeField] private float      chunkSpeed = 4.0f;
    [SerializeField] private int        chunksDisplayed = 3;

    [SerializeField] private float      chunkOffsetDeactive = 1.5f;
                     private float      chunksDeactiveDistance;    

    // Pool de chunks
    private Queue<GameObject> chunkPool    = new Queue<GameObject>(); 
    private List<GameObject>  activeChunks = new List<GameObject>();
        
    private bool activeChunksBoolMovement = false;
    private bool startedGame = false;
    private int  indexChunkList = 0;

    void Start()
    {
        CreateChunksPool();
        InitializeChunks();
    }

    void Update()
    {
        ManageChunksState();

        //Dev Test
        if (Input.GetKeyDown(KeyCode.M))
        {
            activeChunksBoolMovement = !activeChunksBoolMovement;
            SetMovementOfActiveChunks(activeChunksBoolMovement);
        } 
        
        // Change this to a method when player start game
        if (Input.GetKeyDown(KeyCode.S))
        {
            startedGame = !startedGame;
            activeChunksBoolMovement = true;
            SetMovementOfActiveChunks(activeChunksBoolMovement);
        }
    }

    private void CreateChunksPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            //int rnd = Random.Range(0, chunkPrefabList.Count);
            CreateChunk();

            indexChunkList++;
        }
    }

    private void CreateChunk()
    {
        //IndexOutofRange error del indexChunkList porque si la lista del nivel tiene menos que la poolsize
        GameObject chunk = Instantiate(chunkPrefabList[indexChunkList], spawnPoint.position, Quaternion.identity);

        chunk.GetComponent<ChunkMovement>().SetChunkSpeed(chunkSpeed);
        chunk.GetComponentInChildren<ChunkMovement>().SetChunkMovement(activeChunksBoolMovement);

        chunksDeactiveDistance = chunk.GetComponentInChildren<MeshRenderer>().bounds.size.z * chunkOffsetDeactive;

        chunk.SetActive(false);
        chunkPool.Enqueue(chunk);
    }

    private void InitializeChunks()
    {
        for (int i = 0; i < chunksDisplayed; ++i) { ActivateChunk(); }
    }

    private void ManageChunksState()
    {
        if(activeChunks.Count < chunksDisplayed)
        {
            ActivateChunk();
        }

        if(activeChunks.Count > 0 && 
            activeChunks[0].transform.position.z < playerPosition.position.z - chunksDeactiveDistance) 
        {
            DisableChunk(activeChunks[0]);
        }
    }

    // Método para activar un chunk
    private void ActivateChunk()
    {
        if (chunkPool.Count > 0)
        {
            GameObject chunk = chunkPool.Dequeue();

            if (activeChunks.Count > 0)
            {
                GameObject lastChunk = activeChunks[activeChunks.Count - 1];  
                
                MeshRenderer lastChunkRenderer = lastChunk.GetComponentInChildren<MeshRenderer>();
                
                if (lastChunkRenderer != null)
                {
                    float chunkLength = lastChunkRenderer.bounds.size.z;

                    Vector3 newPosition = lastChunk.transform.position + new Vector3(0, 0, chunkLength);
                    chunk.transform.position = newPosition;
                }
                else
                {
                    // Si no tiene MeshRenderer, usa la posición actual (caso de primer chunk generado)
                    chunk.transform.position = spawnPoint.position;
                }
            }
            else
            {
                chunk.transform.position = spawnPoint.position;
            }

            chunk.SetActive(true);
            activeChunks.Add(chunk);            
            
            if (startedGame) 
            {
                activeChunksBoolMovement = true; 
                SetMovementOfActiveChunks(activeChunksBoolMovement); 
            }
        }
    }

    // Método para desactivar y devolver a la pool el chunk
    private void DisableChunk(GameObject chunk)
    {
        activeChunks.Remove(chunk);
        Destroy(chunk);

        if (indexChunkList < chunkPrefabList.Count)
        {
            CreateChunk();

            indexChunkList++;
        }

        // Activar un nuevo chunk para mantener el número deseado de chunks activos
        if (activeChunks.Count < chunksDisplayed)
        {
            ActivateChunk();
        }
    }

    public void SetMovementOfActiveChunks(bool state)
    {        
        foreach (var chunk in activeChunks) { chunk.GetComponent<ChunkMovement>().SetChunkMovement(state); }
    }
}
