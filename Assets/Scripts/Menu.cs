using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private Animator _animator = null;

    public void StartNewGame()
    {
        StartCoroutine(LoadNewScene("MainScene", LoadSceneMode.Single));
        _animator.Play("Fading");
    }

    public void OpenSettings()
    {
        StartCoroutine(LoadNewScene("Settings", LoadSceneMode.Additive));
    }


    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    private IEnumerator LoadNewScene(string sceneName, LoadSceneMode loadSceneMode)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName, loadSceneMode);
    }
}
