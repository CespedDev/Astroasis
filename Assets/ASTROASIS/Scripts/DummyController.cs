using GameplayData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour
{
    [SerializeField]
    private ObjectDataListSO data;
    [SerializeField]
    private GameObject dummyUndestroyed;
    [SerializeField]
    private GameObject dummyDestroyed;
    [SerializeField]
    private Collider dummyCollider;


    private bool spawned = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!spawned && other.tag == "Player")
        {
            dummyUndestroyed.SetActive(true);
            dummyCollider.enabled = true;
            spawned = true;
        }
    }

    public void Killed()
    {
        GameManager.Instance.IncreaseScore(data);

        AudioSource source = dummyDestroyed.GetComponent<AudioSource>();
        source.pitch = -1;
        source.timeSamples = source.clip.samples -1;
        source.Play();
    }
}
