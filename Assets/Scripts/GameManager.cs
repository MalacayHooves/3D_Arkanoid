using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField, Tooltip("Количество жизней игроков на всю игру")] private int _lives = 3;
    private int Lives
    {
        get { return _lives; }
        set
        {
            _lives = value;
            if (_lives <= 0)
            {
                ManageDefeat();
            }
        }
    }
    [SerializeField, Tooltip("Ссылка на шарик")] private Ball _ball = null;
    [SerializeField, Tooltip("Позиция в которой шарик появляется при рестарте")] private GameObject _ballSpawnPoint = null;
    private List<GameObject> _destroyableObjectsList = null;

    private LevelManager _levelManager;
    private Healthbar[] _healthbars;

    private void Start()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        _healthbars = FindObjectsOfType<Healthbar>();
    }

    public void ManageBallHittedGatesSituation()
    {
        Lives--;
        foreach (Healthbar item in _healthbars)
        {
            item.DecreaseHealth();
        }

        StartCoroutine(ResetBall());
    }

    public void SetNewDestroyableObjectsList(List<GameObject> newDestroyableObjectsList)
    {
        _destroyableObjectsList = newDestroyableObjectsList;
    }

    public void RemoveFromDestroyableObjectsList(GameObject destroyable)
    {
        _destroyableObjectsList.Remove(destroyable);
        if (_destroyableObjectsList.Count <= 0)
        {
            ManageVictory();
        }
    }

    private IEnumerator ResetBall()
    {
        if (_ball == null) yield break;
        _ball.enabled = false;
        _ball.transform.position = _ballSpawnPoint.transform.position;
        _ball.transform.rotation = _ballSpawnPoint.transform.rotation;
        yield return new WaitForSeconds(1);
        print("Countdown: 1");
        yield return new WaitForSeconds(1);
        print("Countdown: 2");
        yield return new WaitForSeconds(1);
        print("Countdown: 3");
        _ball.enabled = true;
    }

    private void ManageDefeat()
    {
        print("You lose.");
    }

    private void ManageVictory()
    {
        StartCoroutine(ResetBall());
        print("You won!");
        print("Starting new level");
        _levelManager.LoadNewLevel();
    }
}
