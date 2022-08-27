using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

public class Observer : MonoBehaviour, IObserver
{
    public delegate void SaveDataHandler(string data);
    public delegate void ClearDataHandler();

    private static string path;

    private List<string> _dataStrings = new List<string>();

    private IObservable[] _observables;

    private void Awake()
    {
        path = Application.dataPath + "/observer.txt";
        FileStream file = new FileStream(path, FileMode.OpenOrCreate);
        file.Close();

        _observables = FindObjectsOfType<MonoBehaviour>().OfType<IObservable>().ToArray();
        foreach (IObservable observable in _observables)
        {
            observable.OnSaveData += SaveData;
            observable.OnClearData += ClearData;
        }
    }

    private void OnDisable()
    {
        foreach (IObservable observable in _observables)
        {
            observable.OnSaveData -= SaveData;
            observable.OnClearData -= ClearData;
        }
    }

    private void Start()
    {
        ReadData();
    }

    public void SaveData(string data)
    {
        using(FileStream stream = new FileStream(path, FileMode.Append))
        {
            using(BinaryWriter writer = new BinaryWriter(stream, System.Text.Encoding.Default, false))
            {
                writer.Write($"{data}\n");
            }
        }
    }

    public void ReadData()
    {
        _dataStrings.Clear();
        _dataStrings.TrimExcess();

        using(FileStream stream = File.OpenRead(path))
        {
            using(BinaryReader binaryData = new BinaryReader(stream))
            {
                _dataStrings.Add(binaryData.ReadString());
                while (binaryData.PeekChar() != -1)
                {
                    _dataStrings.Add(binaryData.ReadString());
                }
            }
        }

        OnReturnData?.Invoke(_dataStrings);
    }

    public void ClearData()
    {
        FileStream newFile = new FileStream(path, FileMode.Create);
        newFile.Close();
    }

    public static event ReturnDataHandler OnReturnData;
    public delegate void ReturnDataHandler(List<string> data);
}

public interface IObservable
{
    event Observer.SaveDataHandler OnSaveData;

    event Observer.ClearDataHandler OnClearData;
    
    void GetData(List<string> listOfData);

    void WriteData(string data);
}

public interface IObserver
{
    void SaveData(string data);

    void ReadData();

    void ClearData();
}
