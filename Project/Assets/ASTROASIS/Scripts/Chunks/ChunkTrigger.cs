using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{
    //[SerializeField] private GameObject chunkSection;

    //private bool instantiatedFlag = false;

    [SerializeField] private ChunkManager chunkManager;
    private void Start()
    {
        //Instantiate(chunkSection, new Vector3(0, 20, 0), Quaternion.identity);
        chunkManager.OnChunkTriggerActivated();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Chunk"))// && !instantiatedFlag)
        {
            //Instantiate(chunkSection, new Vector3(0,20,5.45f),Quaternion.identity); 
            //instantiatedFlag = true;
            chunkManager.OnChunkTriggerActivated();
        }

    }
}
