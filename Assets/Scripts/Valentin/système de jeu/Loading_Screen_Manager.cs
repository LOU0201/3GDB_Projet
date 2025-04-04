using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class Loading_Screen_Manager : MonoBehaviour
{
    [Header("Loading Visuals")]
    public Image loadingIcon;
    public Image loadingDoneIcon;
    public TMP_Text loadingText;
    public Image progressBar;
    public Image fadeOverlay;
    public Image background;

    [Header("Timing Settings")]
    public float waitOnLoadEnd = 0.25f;
    public float fadeDuration = 0.25f;

    [Header("Loading Settings")]
    public LoadSceneMode loadSceneMode = LoadSceneMode.Single;
    public ThreadPriority loadThreadPriority;

    [Header("Other")]
    public AudioListener audioListener;

    private AsyncOperation operation;
    private Scene currentScene;

    public static string sceneToLoadName = "";
    public static string loadingSceneName = "Chargement";

    public static void LoadScene(string sceneName)
    {
        if (FindObjectOfType<Loading_Screen_Manager>() == null)
        {
            SceneManager.LoadScene(loadingSceneName);
        }
        sceneToLoadName = sceneName;
    }

    void Awake()
    {
        // If another instance exists, destroy this one
        if (FindObjectsOfType<Loading_Screen_Manager>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        // Only persist if we're the loading scene
        if (SceneManager.GetActiveScene().name == loadingSceneName)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        if (string.IsNullOrEmpty(sceneToLoadName))
            return;

        currentScene = SceneManager.GetActiveScene();
        StartCoroutine(LoadAsync(sceneToLoadName));
    }

    private IEnumerator LoadAsync(string sceneName)
    {
        // Initialize
        ShowLoadingVisuals();
        fadeOverlay.gameObject.SetActive(true);
        fadeOverlay.canvasRenderer.SetAlpha(1f);

        // Fade in
        FadeIn();
        yield return new WaitForSeconds(fadeDuration);

        // Start loading
        StartOperation(sceneName);
        float lastProgress = 0f;

        // Update progress bar
        while (!DoneLoading())
        {
            yield return null;
            if (!Mathf.Approximately(operation.progress, lastProgress))
            {
                progressBar.fillAmount = operation.progress;
                lastProgress = operation.progress;
            }
        }

        // Finalize progress bar
        progressBar.fillAmount = 1f;

        // Show completion visuals
        ShowCompletionVisuals();
        yield return new WaitForSeconds(waitOnLoadEnd);

        // Fade out
        FadeOut();
        yield return new WaitForSeconds(fadeDuration);

        // Hide all loading UI elements
        HideAllLoadingElements();

        // Complete the scene transition
        if (loadSceneMode == LoadSceneMode.Additive)
        {
            audioListener.enabled = false;
            yield return SceneManager.UnloadSceneAsync(currentScene.name);
        }
        else
        {
            operation.allowSceneActivation = true;
        }

        // Destroy the loading screen if we're not in the loading scene anymore
        if (SceneManager.GetActiveScene().name != loadingSceneName)
        {
            Destroy(gameObject);
        }
    }

    private void StartOperation(string sceneName)
    {
        Application.backgroundLoadingPriority = loadThreadPriority;
        operation = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);

        if (loadSceneMode == LoadSceneMode.Single)
        {
            operation.allowSceneActivation = false;
        }
    }

    private bool DoneLoading()
    {
        return (loadSceneMode == LoadSceneMode.Additive && operation.isDone) ||
               (loadSceneMode == LoadSceneMode.Single && operation.progress >= 0.9f);
    }

    void FadeIn()
    {
        fadeOverlay.CrossFadeAlpha(0, fadeDuration, true);
    }

    void FadeOut()
    {
        fadeOverlay.CrossFadeAlpha(1, fadeDuration, true);
    }

    void ShowLoadingVisuals()
    {
        loadingIcon.gameObject.SetActive(true);
        loadingDoneIcon.gameObject.SetActive(false);
        progressBar.fillAmount = 0f;
        loadingText.text = "Materialized world...";
        background.gameObject.SetActive(true);
    }

    void ShowCompletionVisuals()
    {
        loadingIcon.gameObject.SetActive(false);
        loadingDoneIcon.gameObject.SetActive(true);
        loadingText.text = "LOADING DONE";
    }

    void HideAllLoadingElements()
    {
        loadingIcon.gameObject.SetActive(false);
        loadingDoneIcon.gameObject.SetActive(false);
        loadingText.gameObject.SetActive(false);
        progressBar.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
        fadeOverlay.gameObject.SetActive(false);
    }
}
