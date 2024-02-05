using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

        if (Zone.draw)
        {
            _animation = true;
        }
        Animation();
    }  

    public void Animation()
    {
        if (_animation && _cardAnim.transform.rotation.y > 0)
        {
            _cardAnim.transform.position = new Vector3(_cardAnim.transform.position.x - 0.7f,
                                                       _cardAnim.transform.position.y,
                                                       _cardAnim.transform.position.z);
            _cardAnim.transform.Rotate(0, 0.2f, 0);
        }
        if (_cardAnim.transform.rotation.y <= 0)
        {
            AnimationStats._animation = true;
            if (Input.GetMouseButtonDown(0))
                _timer = 2;
            _timer += Time.deltaTime;
            if (_timer >= 2 && !CardChoice._choice)
            {
                _animation = false;
                _timer = 0;
                _text.text = "";
                _cardAnim.transform.Rotate(0, -180, 0);
                _cardAnim.transform.position = new Vector3(1400, 300, 0);
            }           
        }
    }
}
