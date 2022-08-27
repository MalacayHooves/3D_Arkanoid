using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour, IObservable
{
    [SerializeField] private Animator _animator = null;

    public event Observer.SaveDataHandler OnSaveData;
    public event Observer.ClearDataHandler OnClearData;

    private void Start()
    {
        OnSaveData($"[{System.DateTime.Now}] Start Game");
    }

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
        OnSaveData($"[{System.DateTime.Now}] End Game");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    private IEnumerator LoadNewScene(string sceneName, LoadSceneMode loadSceneMode)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName, loadSceneMode);
    }

    public void GetData(List<string> listOfData)
    {
        throw new System.NotImplementedException();
    }

    public void WriteData(string data)
    {
        throw new System.NotImplementedException();
    }
}
