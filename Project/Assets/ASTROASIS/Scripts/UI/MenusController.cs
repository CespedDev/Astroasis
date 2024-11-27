using GameplayEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenusController : MonoBehaviour
{
    [SerializeField]
    private Utils.SerializableDictionary<MenusEnum, GameObject> menus;
    
    [SerializeField]
    private GameEventSO loadingEvent;

    private MenusEnum? currentMenu =  null;

    // ____ INTRO MENU ____
    public void OpenMainMenu() { LoadMenu(MenusEnum.MainMenu); }

    // ____ MAIN MENU ____
    public void StartGame() 
    {
        //LoadMenu(MenusEnum.GameLevel);

        loadingEvent.Raise();

        Debug.Log("UNLOADING menu");
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("MainMenu"));

        Debug.Log("LOADING level");
    }

    public void OpenOptions() { LoadMenu(MenusEnum.OptionsMenu); }

    public void OpenCredits() { LoadMenu(MenusEnum.CreditsMenu); }

    public void QuitGame() { Application.Quit(); }

    // ____ MENU MANAGE ____
    private void LoadMenu(MenusEnum menu)
    {
        if (currentMenu.HasValue) UnloadMenu(currentMenu.Value);

        menus[menu].SetActive(true);
        currentMenu = menu;
    }

    private void UnloadMenu(MenusEnum menu)
    {
        menus[menu].SetActive(false);
    }
}
