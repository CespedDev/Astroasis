using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;
using GameplayEvents;

public class EndMenuController : MonoBehaviour
{
    public TMP_Text score;

    [SerializeField] private GameEventSO loadingEvent;

    private void OnEnable()
    {
        if(GameManager.Instance != null)
            score.text = $"Score: {GameManager.Instance.gameData.score}";
    }

    public void OnRestart()
    {
        loadingEvent.Raise();

        Debug.Log("UNLOADING level");
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Nivel"));

        Debug.Log("LOADING level");
        SceneManager.LoadSceneAsync("Nivel", LoadSceneMode.Additive);
                        
        Destroy(gameObject);
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
