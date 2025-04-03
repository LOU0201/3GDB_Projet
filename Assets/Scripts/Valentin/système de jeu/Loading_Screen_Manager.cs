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
    public Image Background;

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
    public static string loadingSceneName = "Chargement"; // Nom de la scène de chargement
    public float compteur = 0f;
    public float TimeMax = 1f;

    public static void LoadScene(string sceneName)
    {
        if (FindObjectOfType<Loading_Screen_Manager>() == null)
        {
            // Charge manuellement la scène de chargement si elle n'est pas déjà active
            SceneManager.LoadScene(loadingSceneName);
        }

        sceneToLoadName = sceneName;
    }

    void Awake()
    {
        if (FindObjectsOfType<Loading_Screen_Manager>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (string.IsNullOrEmpty(sceneToLoadName))
            return;

        fadeOverlay.gameObject.SetActive(true);
        currentScene = SceneManager.GetActiveScene();
        StartCoroutine(LoadAsync(sceneToLoadName));
    }

    private IEnumerator LoadAsync(string sceneName)
    {
        ShowLoadingVisuals();
        yield return null;

        FadeIn();
        StartOperation(sceneName);

        float lastProgress = 0f;

        while (!DoneLoading())
        {
            yield return null;
            if (!Mathf.Approximately(operation.progress, lastProgress))
            {
                progressBar.fillAmount = operation.progress;
                lastProgress = operation.progress;
            }
        }

        if (loadSceneMode == LoadSceneMode.Additive)
            audioListener.enabled = false;

        ShowCompletionVisuals();
        yield return new WaitForSeconds(waitOnLoadEnd);

        FadeOut();
        yield return new WaitForSeconds(fadeDuration);

        if (loadSceneMode == LoadSceneMode.Additive)
            SceneManager.UnloadSceneAsync(currentScene.name);
        else
            operation.allowSceneActivation = true;
    }

    private void StartOperation(string sceneName)
    {
        Application.backgroundLoadingPriority = loadThreadPriority;
        operation = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
        if (loadSceneMode == LoadSceneMode.Single)
            operation.allowSceneActivation = false;
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
        loadingText.text = "LOADING...";
    }

    void ShowCompletionVisuals()
    {
        loadingIcon.gameObject.SetActive(false);
        loadingDoneIcon.gameObject.SetActive(true);
        progressBar.fillAmount = 1f;
        loadingText.text = "LOADING DONE";
        if(progressBar.fillAmount == 1f)
        {
            Hide();
        }
    }

    void Hide()
    {
        loadingText.CrossFadeAlpha(1, fadeDuration, true);
        loadingDoneIcon.CrossFadeAlpha(1, fadeDuration, true);
        progressBar.CrossFadeAlpha(1, fadeDuration, true);
        fadeOverlay.gameObject.SetActive(false);
        Background.CrossFadeAlpha(1, fadeDuration, true);
    }
}
