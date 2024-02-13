using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class Sc_StartGame : MonoBehaviour
{
    [SerializeField] private GameObject Panel_Text;
    private void Awake()
    {
        Panel_Text.SetActive(false);
    }
    public void StartGame()
    {
        if (Sc_SpritesCharacters.Instance._selectedCharacters.Count == 3)
        {
            Sc_SaveCharacters.Instance.SaveToJson();
            Panel_Text.SetActive(true);
        }
    }
}
