using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AnimationCard : MonoBehaviour
{
    public GameObject _cardAnim;
    public static bool _animation;
    public static float _timer = 0;
    public TextMeshProUGUI _text;
    public Card _card;
    bool showtext = false;
    public SC_PlayerTurn _endTurn;

    private void Start()
    {
        _cardAnim.transform.position = new Vector3(300, 300, 0);
    }
    void Update()
    {
        if (_cardAnim.transform.rotation.y <= 0.75 && !showtext && Card._card != null)
        {
            _text.text = Card._card._description;
            showtext = true;
        }

        if (Zone._draw)
        {
            _animation = true;
        }
        Animation();
    }  

    public void Animation()
    {

        if (_animation && _cardAnim.transform.rotation.y > 0)
        {
            Zone._draw = false;
            _cardAnim.transform.position = new Vector3(_cardAnim.transform.position.x - 3 * Time.deltaTime,
                                                       _cardAnim.transform.position.y,
                                                       _cardAnim.transform.position.z);
            _cardAnim.transform.Rotate(0, 100 * Time.deltaTime, 0);
        }
        if (_cardAnim.transform.rotation.y <= 0 && !CardChoice._choice)
        {
            AnimationStats._animation = true;
            if (!Card.Instance._isChoice)
            {
                _timer += Time.deltaTime;
                if (Input.GetMouseButtonDown(0))
                    _timer = 15;
            }
            
            if (_timer >= 15 && !CardChoice._cardDonation && !CardChoice._medium && !CardChoice._cardTrade && !CardChoice._choice)
            {
                _animation = false;
                _timer = 0.0f;
                _endTurn.endTurn = true;
                showtext = false;
                _text.text = "";
                _cardAnim.transform.Rotate(0, -180, 0);
                _cardAnim.transform.position = new Vector3(300, 300, 0);
                Zone._draw = false;
                AnimationStats._hpAnim = 0;
                AnimationStats._bulletAnim = 0;
                AnimationStats._goldAnim = 0;
                SC_PlayerTurn.Instance.turn += 1;
                Card.Instance._isChoice = false;
                if (SC_PlayerTurn.Instance.turn == Stats._nbPlayer)
                    SC_PlayerTurn.Instance.turn = 0;
                SkipTurn();
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
                SC_PlayerTurn.Instance.turn = 0;
            if (Card._skipTurn[SC_PlayerTurn.Instance.turn])
            {
                Card._skipTurn[SC_PlayerTurn.Instance.turn] = false;
                SC_PlayerTurn.Instance.turn += 1;
                if (SC_PlayerTurn.Instance.turn == Stats._nbPlayer)
                    SC_PlayerTurn.Instance.turn = 0;
                if (Card._skipTurn[SC_PlayerTurn.Instance.turn])
                {
                    Card._skipTurn[SC_PlayerTurn.Instance.turn] = false;
                    SC_PlayerTurn.Instance.turn += 1;
                    if (SC_PlayerTurn.Instance.turn == Stats._nbPlayer)
                        SC_PlayerTurn.Instance.turn = 0;
                }
            }
        }
    }
}
