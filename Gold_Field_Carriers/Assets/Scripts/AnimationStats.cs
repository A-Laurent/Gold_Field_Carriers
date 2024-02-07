using UnityEngine;
using UnityEngine.UI;

public class AnimationStats : MonoBehaviour
{
    public static bool _animation;
    public static int _bulletAnim;
    public static int _hpAnim;
    public static int _goldAnim;

    public Text _textHp;
    public Text _textGold;
    public Text _textBullet;

    // Update is called once per frame
    void Update()
    {
        if (_animation)
        {
            if (_hpAnim != 0)
                _textHp.text = _hpAnim.ToString();
            if (_bulletAnim != 0)
                _textBullet.text = _bulletAnim.ToString();
            if (_goldAnim != 0)
                _textGold.text = _goldAnim.ToString();

            _textHp.color = new Color(_textHp.color.r, _textHp.color.g, _textHp.color.b, _textHp.color.a - 0.002f);
            _textBullet.color = new Color(_textBullet.color.r, _textBullet.color.g, _textBullet.color.b, _textBullet.color.a - 0.002f);
            _textGold.color = new Color(_textGold.color.r, _textGold.color.g, _textGold.color.b, _textGold.color.a - 0.002f);
        }
        if (_textGold.color.a <= 0.001)
        {
            _animation = false;
            _textHp.color = new Color(_textHp.color.r, _textHp.color.g, _textHp.color.b, 1);
            _textBullet.color = new Color(_textBullet.color.r, _textBullet.color.g, _textBullet.color.b, 1);
            _textGold.color = new Color(_textGold.color.r, _textGold.color.g, _textGold.color.b, 1);
        }
    }
}
