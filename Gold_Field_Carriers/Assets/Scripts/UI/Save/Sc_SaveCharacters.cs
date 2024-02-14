using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Sc_SaveCharacters : MonoBehaviour
{
    public Sc_DataCharacters _DataCharacters = new Sc_DataCharacters();
    public static Sc_SaveCharacters Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void LoadFromJson()
    {
        string filepath = Application.dataPath + "/Saves/Characters.json";
        string gameData = File.ReadAllText(filepath);

        _DataCharacters = JsonUtility.FromJson<Sc_DataCharacters>(gameData);

        Sc_CharacterManager.Instance._ID[0] = _DataCharacters._id[0];
        Sc_CharacterManager.Instance._ID[1] = _DataCharacters._id[1];
        Sc_CharacterManager.Instance._ID[2] = _DataCharacters._id[2];
    }

    public void SaveToJson()
    {
        _DataCharacters._id.Add(Sc_SpritesCharacters.Instance._ID[0]);
        _DataCharacters._id.Add(Sc_SpritesCharacters.Instance._ID[1]);
        _DataCharacters._id.Add(Sc_SpritesCharacters.Instance._ID[2]);
        
        string gameData = JsonUtility.ToJson(_DataCharacters);
        File.WriteAllText(Application.dataPath + "/Saves/Characters.json", gameData);
        Debug.Log("Characters selection saved !");
    }
}
