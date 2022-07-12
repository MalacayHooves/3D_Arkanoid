using UnityEngine;
using UnityEngine.UI;

public class ApplicationNumber : MonoBehaviour
{
    private Text _text;

    private void Start()
    {
        _text = GetComponent<Text>();
        _text.text = " Application Version : " + Application.version;
    }
}
