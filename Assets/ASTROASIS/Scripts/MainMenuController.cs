using GameplayEvents;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameEventSO loadingEvent;

    public void OnStart()
    {
        loadingEvent.Raise();

        Debug.Log("UNLOADING main menu");
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("MainMenu"));

        Debug.Log("LOADING level");
        SceneManager.LoadScene("Nivel", LoadSceneMode.Additive);
        
        
    }

    public void Options()
    {

    }

    public void OnExit()
    {
        Application.Quit();
    }
}
