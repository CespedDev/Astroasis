using GameplayData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour
{
    [SerializeField]
    private ObjectDataListSO data;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Collider dummyCollider;
    [SerializeField]
    private Transform dummyTrigger;
    [SerializeField]
    private Transform dummyBody;
    [SerializeField]
    private GameObject explosion;

    private Transform player;

    private void Start()
    {
        dummyTrigger.position = new Vector3(0, 0, transform.position.z);
    }

    private void Update()
    {
        if (player) dummyBody.LookAt(player.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!player && other.tag == "Player")
        {
            animator.SetBool("Open", true);
            dummyCollider.enabled = true;
            player = other.transform;
        }
    }

    public void Killed()
    {
        if (GameManager.Instance)
            GameManager.Instance.IncreaseScore(data);

        animator.SetBool("Died", true);
        explosion.SetActive(true);
    }
}
