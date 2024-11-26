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


    private bool opened = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!opened && other.tag == "Player")
        {
            animator.SetBool("Open", true);
            dummyCollider.enabled = true;
            opened = true;
        }
    }

    public void Killed()
    {
        if (GameManager.Instance)
            GameManager.Instance.IncreaseScore(data);

        animator.SetBool("Died", true);
    }
}
