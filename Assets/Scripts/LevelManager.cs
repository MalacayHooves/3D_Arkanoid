using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField, Tooltip("Список с уровнями")] private List<GameObject> _levels = null;

    private int _currentLevel = 0;

    public void LoadNewLevel()
    {
        StartCoroutine(LoadNewLevelWithPause());
    }

    private IEnumerator LoadNewLevelWithPause()
    {
        yield return new WaitForSeconds(1);
        _levels[_currentLevel].SetActive(false);
        _currentLevel++;
        if (_levels.Count < _currentLevel)
        {
            print("There is no more levels");
            print("You beat the game!");
        }
        else
        {
            _levels[_currentLevel].SetActive(true);
        }
    }
}
