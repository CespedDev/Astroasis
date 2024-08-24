using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkMovement : MonoBehaviour
{
    private float chunkVelocity;
    private bool  move = false;

    void Update()
    {
        if(move) transform.position += new Vector3(0,0, -chunkVelocity) * Time.deltaTime;
    }

    public void SetChunkSpeed   (float speed)   { chunkVelocity = speed; }
    public void SetChunkMovement(bool state)    { move = state;          }
}
