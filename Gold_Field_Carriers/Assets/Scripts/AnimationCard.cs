using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AnimationCard : MonoBehaviour
{
    public GameObject _cardAnim;
    public static bool _animation;
    public static float _timer = 0;
    public TextMeshProUGUI _text;
    public TextMeshProUGUI _textName;
    public Card _card;
    bool showtext = false;
    public SC_PlayerTurn _endTurn;

    public GameObject _cardUi;

    public static AnimationCard Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }

        Instance = this;
    }

    private void Start()
    {
        Animation();
    }

    void Update()
    {
        if (_cardAnim.transform.rotation.y <= 0.60 && !showtext && Card._card != null)
        {
            _text.text = Card._card._description;
            _textName.text = Card._card._name;
            _cardUi.GetComponent<Image>().sprite = Card._card._cardImage;
            showtext = true;
        }

        Animation();
    }

    public void Animation()
    {
        if (_animation && _cardAnim.transform.rotation.y > 0)
        {
            _cardAnim.transform.Rotate(0, 100 * Time.deltaTime, 0);
            _cardUi.SetActive(true);
            if (!showtext)
                _cardUi.GetComponent<Image>().sprite = Card._card._cardImageDos;
        }

        if (_cardAnim.transform.rotation.y <= 0 && !CardChoice._choice)
        {
            AnimationStats._animation = true;
            if (!Card._isChoice)
            {
                _timer += Time.deltaTime;
                if (Input.GetMouseButtonDown(0))
                {
                    _timer = 15;
                    Sc_CharacterManager.Instance.ChangePlayer();
                }
            }

            if (_timer >= 15 && !CardChoice._cardDonation && !CardChoice._medium && !CardChoice._cardTrade &&
                !CardChoice._choice)
            {
                _animation = false;
                _timer = 0.0f;
                _endTurn.endTurn = true;
                showtext = false;
                _text.text = "";
                _textName.text = "";
                _cardAnim.transform.Rotate(0, -180, 0);
                Zone._draw = false;
                AnimationStats._hpAnim = 0;
                AnimationStats._bulletAnim = 0;
                AnimationStats._goldAnim = 0;

                // if (SC_PlayerTurn.Instance.turn == 0)
                // {
                //     if (SC_PlayerTurn.Instance._canMove[2] == false)
                //     {
                //         SC_PlayerTurn.Instance.turn += 1;
                //     }
                // }
                // else if (SC_PlayerTurn.Instance._canMove[SC_PlayerTurn.Instance.turn -1])
                // {
                //     SC_PlayerTurn.Instance.turn += 1;
                // }
                
                SC_PlayerTurn.Instance.turn += 1;
                if (SC_PlayerTurn.Instance.turn >= Stats._nbPlayer)
                    SC_PlayerTurn.Instance.turn = 0;
                
                if(!SC_PlayerTurn.Instance._canMove[SC_PlayerTurn.Instance.turn])
                {
                    SC_PlayerTurn.Instance.turn += 1;
                    if (SC_PlayerTurn.Instance.turn >= Stats._nbPlayer)
                        SC_PlayerTurn.Instance.turn = 0;
                    if (!SC_PlayerTurn.Instance._canMove[SC_PlayerTurn.Instance.turn])
                    {
                        SC_PlayerTurn.Instance.turn += 1;
                        if (SC_PlayerTurn.Instance.turn >= Stats._nbPlayer)
                            SC_PlayerTurn.Instance.turn = 0;
                        if (!SC_PlayerTurn.Instance._canMove[SC_PlayerTurn.Instance.turn])
                        {
                            Sc_VictoryDefeat._endGame = true;
                        }
                    }

                }          
                
                Sc_CharacterManager.Instance.ChangePlayer();
                
                if (SC_PlayerTurn.Instance.turn >= Stats._nbPlayer)
                    SC_PlayerTurn.Instance.turn = 0;
                
                Card._isChoice = false;
                Zone.AddTurn();
                SkipTurn();
                _cardUi.SetActive(false);
            }
        }
    }


    public void SkipTurn()
    {
        if (Card._skipTurn[SC_PlayerTurn.Instance.turn])
        {
            Card._skipTurn[SC_PlayerTurn.Instance.turn] = false;
            SC_PlayerTurn.Instance.turn += 1;
            if (SC_PlayerTurn.Instance.turn == Stats._nbPlayer)
            {
                SC_PlayerTurn.Instance.turn = 0;
                Zone.AddTurn();
            }
            if (Card._skipTurn[SC_PlayerTurn.Instance.turn])
            {
                Card._skipTurn[SC_PlayerTurn.Instance.turn] = false;
                SC_PlayerTurn.Instance.turn += 1;
                if (SC_PlayerTurn.Instance.turn == Stats._nbPlayer)
                {
                    Zone.AddTurn();
                    SC_PlayerTurn.Instance.turn = 0;
                }
                if (Card._skipTurn[SC_PlayerTurn.Instance.turn])
                {
                    Card._skipTurn[SC_PlayerTurn.Instance.turn] = false;
                    SC_PlayerTurn.Instance.turn += 1;
                    if (SC_PlayerTurn.Instance.turn == Stats._nbPlayer)
                    {
                        Zone.AddTurn();
                        SC_PlayerTurn.Instance.turn = 0;
                    }
                }
            }
        }
    }
}