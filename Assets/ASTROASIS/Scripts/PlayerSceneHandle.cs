using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSceneHandle : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private GameObject EndMenu;

    void Awake()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
    }

    public void EndScene()
    {
        GameObject instance = Instantiate(EndMenu);
        instance.transform.position = new Vector3(Player.transform.position.x, instance.transform.position.y, Player.transform.position.z + 1.5f);
    }
}
