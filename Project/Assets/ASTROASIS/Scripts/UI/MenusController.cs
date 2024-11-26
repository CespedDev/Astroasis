using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenusController : MonoBehaviour
{
    private MenusEnum? currentMenu =  null;   

    public void StartGame() { LoadMenu(MenusEnum.GameLevel); }

    public void OpenOptions() { LoadMenu(MenusEnum.OptionsMenu); }

    public void OpenCredits() { LoadMenu(MenusEnum.CreditsMenu); }

    public void QuitGame() { Application.Quit(); }

    private void LoadMenu(MenusEnum newMenu)
    {
        if (currentMenu.HasValue) UnloadMenu(currentMenu.Value);


        SceneManager.LoadSceneAsync(newMenu.ToString(), LoadSceneMode.Additive);
        currentMenu = newMenu;
    }

    private void UnloadMenu(MenusEnum menu)
    {
        SceneManager.UnloadSceneAsync(menu.ToString());
    }
}
