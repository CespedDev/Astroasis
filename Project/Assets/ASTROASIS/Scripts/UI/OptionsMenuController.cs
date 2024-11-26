using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenuController : MonoBehaviour
{    
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private RectTransform maskVolume;
    [SerializeField] private RectTransform maskFullVolume;
    private float initialVolumeWidth;

    [Range(0,1)]
    public float volume = 0.5f;

    [SerializeField] private TMP_Dropdown languageDropdown;    
    //[SerializeField] private AudioSource backgroundMusic;

    private void Start()
    {
        initialVolumeWidth = maskFullVolume.sizeDelta.x;
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 0.5f);     
        
        int savedLenguage = PlayerPrefs.GetInt("Language", 0);
        
        languageDropdown.value = savedLenguage;
        languageDropdown.RefreshShownValue();

        SetLanguage(savedLenguage);

        // Suscribcion evento de cambio de idioma
        languageDropdown.onValueChanged.AddListener(SetLanguage);
    }

    public void SetVolume(float value)
    {
        //backgroundMusic.volume = volumeSlider.value;
        // Create Mask for image by value        
        volume = Mathf.Clamp01(volume);
        maskVolume.sizeDelta = new Vector2(initialVolumeWidth * volume, maskVolume.sizeDelta.y);
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void SetLanguage(int languageIndex)
    {
        PlayerPrefs.SetInt("SelectedLanguage", languageIndex);        
        //Debug.Log("Language changed to: " + languageDropdown.options[languageIndex].text);

        switch (languageIndex)
        {
            case 0: // Español                
                break;
            case 1: // Inglés                
                break;
            case 2: // Chino                
                break;
            default:
                Debug.LogWarning("Idioma no reconocido.");
                break;
        }
    }

    public void BackToMainMenu() { SceneManager.UnloadSceneAsync(MenusEnum.OptionsMenu.ToString()); }

    private void OnDestroy()
    {
        languageDropdown.onValueChanged.RemoveListener(SetLanguage);
    }
}
