using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] Toggle _soundToggle = null;
    [SerializeField] Slider _volume = null;
    [SerializeField] Dropdown _difficultyLevel = null;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _soundToggle.isOn = PlayerPrefs.GetString("soundToggle") == "True";
        _volume.value = PlayerPrefs.GetFloat("volumeLevel");
        _difficultyLevel.value = PlayerPrefs.GetInt("difficultyLevel");
    }

    public void Return()
    {
        StartCoroutine(CloseScene());
        _animator.Play("CloseSettings");
    }

    public void ToggleSound()
    {
        PlayerPrefs.SetString("soundToggle", _soundToggle.isOn.ToString());
    }

    public void ChangeVolume()
    {
        PlayerPrefs.SetFloat("volumeLevel", _volume.value);
    }

    public void ChangeDifficulty()
    {
        PlayerPrefs.SetInt("difficultyLevel", _difficultyLevel.value);
    }

    private IEnumerator CloseScene()
    {
        yield return new WaitForSecondsRealtime(1);
        SceneManager.UnloadSceneAsync("Settings");
    }
}
