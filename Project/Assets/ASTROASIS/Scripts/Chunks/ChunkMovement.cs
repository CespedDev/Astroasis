using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkMovement : MonoBehaviour
{
    [SerializeField] private float chunkVelocity = 3;
    [SerializeField] private ChunkManager chunkManager;

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0,0, -chunkVelocity) * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DestroyChunk"))
        {
            //Destroy(gameObject);
            chunkManager.RecycleChunk(other.gameObject);
        }
    }
}
