using System.Collections;
using System;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public class SavingAndLoading : MonoBehaviour {

    public static SavingAndLoading save;
    public int score;

    private void Awake()
    {
        if (save == null)
        {
            DontDestroyOnLoad(gameObject);
            save = this;
        }
        else if (save != this)
        {
            Destroy(gameObject);
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/save.dat");

        Data data = new Data();
        data.score = score;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (!File.Exists(Application.persistentDataPath + "/save.dat"))
        {
            return;
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/save.dat", FileMode.Open);

        Data data = (Data)bf.Deserialize(file);
        file.Close();

        score = data.score;
    }

}

[Serializable]
class Data
{
    public int score;
}
