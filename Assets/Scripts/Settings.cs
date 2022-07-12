using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Return()
    {
        StartCoroutine(CloseScene());
        _animator.Play("CloseSettings");
    }

    private IEnumerator CloseScene()
    {
        yield return new WaitForSecondsRealtime(1);
        SceneManager.UnloadSceneAsync("Settings");
    }
}
