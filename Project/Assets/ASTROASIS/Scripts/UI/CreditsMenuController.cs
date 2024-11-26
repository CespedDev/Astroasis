using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsMenuController : MonoBehaviour
{
    public void BackToMainMenu() { SceneManager.UnloadSceneAsync(MenusEnum.CreditsMenu.ToString()); }
}

