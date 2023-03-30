using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelLoader : MonoBehaviour
{

    // Make sure the scene you are trying to load is in the "Scenes in Build" in the build settings :)

    public GameObject loadingScreen;
    public GameObject activateLoadedSceneButton;
    public Slider slider;
    //public TMP_Text progressText;

    AsyncOperation operation;

    

    public void LoadLevel (string sceneName)
    {
        StartCoroutine(LoadScene(sceneName));

    }

    IEnumerator LoadScene (string sceneName)
    {
        loadingScreen.SetActive(true);

        if (activateLoadedSceneButton != null)
        {
            
            activateLoadedSceneButton.SetActive(false);
        }

        // Here is where you would put the code for a random minigame/ a food pyramid game

        // Loads the scene in the background
        operation = SceneManager.LoadSceneAsync(sceneName);

        // Don't swap scene until you allow it
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            //progressText.text = progress * 100f + "%";
            
            if (operation.progress >= 0.9f)
            {

                // Show the swap scene button when the game is loaded
                if (activateLoadedSceneButton != null)
                {
                    activateLoadedSceneButton.SetActive(true);
                }
            }

            yield return null;
        }
    }

    public void ActivateLoadLevel()
    {
        // Activate the next scene
        operation.allowSceneActivation = true;
        loadingScreen.SetActive(false);
    }

    
}
