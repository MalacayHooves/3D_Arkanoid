using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Image[] _hearts = null;
    [SerializeField] private Text _text = null;

    private int health = 3;

    public void DecreaseHealth()
    {
        health--;
        _hearts[health].enabled = false;
        _text.text = $"Lives: {health}";
    }
}
