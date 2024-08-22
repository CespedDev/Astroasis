using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkMovement : MonoBehaviour
{
    [SerializeField] private ChunkManager chunkManager;

    private float chunkVelocity;

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0,0, -chunkVelocity) * Time.deltaTime;
    }

    public void SetChunkSpeed(float speed)
    {
        chunkVelocity = speed;
        //Debug.Log($"speed del chunk creado en la pool de {chunkVelocity}");
    }
}
