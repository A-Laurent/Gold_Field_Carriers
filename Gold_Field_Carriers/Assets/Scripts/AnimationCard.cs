using UnityEngine;
using UnityEngine.UI;

public class AnimationCard : MonoBehaviour
{
    public GameObject _cardAnim;
    public static bool _animation;
    public static float _timer = 0;
    public Text _text;
    public Card _card;

    private void Start()
    {
        _cardAnim.transform.position = new Vector3(1400, 300, 0);
    }
    void Update()
    {
        if (_cardAnim.transform.rotation.y <= 0.75)
            _text.text = _card._description.text;

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
            _cardAnim.transform.position = new Vector3(_cardAnim.transform.position.x - 300 * Time.deltaTime,
                                                       _cardAnim.transform.position.y,
                                                       _cardAnim.transform.position.z);
            _cardAnim.transform.Rotate(0, 100 * Time.deltaTime, 0);
        }
        if (_cardAnim.transform.rotation.y <= 0 && !CardChoice._choice)
        {
            _timer += Time.deltaTime;
            AnimationStats._animation = true;
            if (Input.GetMouseButtonDown(0))
                _timer = 15;
            if (_timer >= 15 && !CardChoice._choice && !CardChoice._cardTrade)
            {
                _animation = false;
                _timer = 0.0f;
                _text.text = "";
                _cardAnim.transform.Rotate(0, -180, 0);
                _cardAnim.transform.position = new Vector3(1400, 300, 0);
                Zone._draw = false;
                AnimationStats._hpAnim = 0;
                AnimationStats._bulletAnim = 0;
                AnimationStats._goldAnim = 0;
                Stats._turnPlayer += 1;
                if (Stats._turnPlayer == Stats._nbPlayer)
                    Stats._turnPlayer = 0;
                if (Card._skipTurn[Stats._turnPlayer])
                {
                    Card._skipTurn[Stats._turnPlayer] = false;
                    Stats._turnPlayer += 1;
                    if (Stats._turnPlayer == Stats._nbPlayer)
                        Stats._turnPlayer = 0;
                }
            }
        }
    }
}
