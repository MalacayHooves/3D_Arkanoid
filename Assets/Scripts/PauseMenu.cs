using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour, IObservable
{
    private Animator _animator;

    private InputActionsAsset _input;

    private Button[] _buttons;

    private bool _isSettingsOpened;

    public event Observer.SaveDataHandler OnSaveData;
    public event Observer.ClearDataHandler OnClearData;

    private void Awake()
    {
        _input = new InputActionsAsset();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        _buttons = GetComponentsInChildren<Button>();
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.MainMap.Pause.performed += OpenPauseMenu;
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    public void Resume()
    {
        Time.timeScale = 1;
        _animator.Play("ClosePauseMenu");
        StartCoroutine(SettingButtonsStateAfterPause(0f, false));
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        OnSaveData($"[{System.DateTime.Now}] End Game");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void OpenSettings()
    {
        _isSettingsOpened = true;
        StartCoroutine(LoadNewScene("Settings", LoadSceneMode.Additive));
    }

    private void OpenPauseMenu(InputAction.CallbackContext context)
    {
        if (context.performed && !_isSettingsOpened)
        {
            Time.timeScale = 0;
            _animator.Play("OpenPauseMenu");
            StartCoroutine(SettingButtonsStateAfterPause(1f, true));
        }
    }

    private IEnumerator SettingButtonsStateAfterPause(float time, bool state)
    {
        yield return new WaitForSecondsRealtime(time);
        foreach (Button button in _buttons)
        {
            button.interactable = state;
        }
    }

    private IEnumerator LoadNewScene(string sceneName, LoadSceneMode loadSceneMode)
    {
        yield return new WaitForSecondsRealtime(1);
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
