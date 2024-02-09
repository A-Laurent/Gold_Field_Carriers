using UnityEngine;
using UnityEngine.SceneManagement;
public class Sc_StartGame : MonoBehaviour
{
    public void StartGame()
    {
        if (Sc_SpritesCharacters.Instance._selectedCharacters.Count == 3)
        {
            Sc_SaveCharacters.Instance.SaveToJson();
            SceneManager.LoadScene("Devroom_FixUI");
        }
    }
}
