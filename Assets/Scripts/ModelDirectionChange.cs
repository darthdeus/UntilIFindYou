using UnityEngine;

public class ModelDirectionChange : MonoBehaviour
{
    private Vector2 _current;
    private SpriteRenderer _default;

    private Sprite _idle;
    private Sprite _faceLeft;
    private Sprite _faceUpLeft;
    private Sprite _faceUp;
    private Sprite _faceUpRight;
    private Sprite _faceRight;
    private Sprite _faceDownRight;
    private Sprite _faceDown;
    private Sprite _faceDownLeft;
    // Use this for initialization
    void Start()
    {
        _current = new Vector2();
        _default = GetComponent<SpriteRenderer>();

        // basic
        _idle = Resources.Load<Sprite>(_default.sprite.name + "/Idle");
        _faceUp = Resources.Load<Sprite>(_default.sprite.name + "/Up");
        _faceDown = Resources.Load<Sprite>(_default.sprite.name + "/Down");
        _faceRight = Resources.Load<Sprite>(_default.sprite.name + "/Right");
        _faceLeft = Resources.Load<Sprite>(_default.sprite.name + "/Left");

        // optional
        _faceUpLeft = Resources.Load<Sprite>(_default.sprite.name + "/UpLeft");
        _faceUpRight = Resources.Load<Sprite>(_default.sprite.name + "/UpRight");
        _faceRight = Resources.Load<Sprite>(_default.sprite.name + "/Right");
        _faceDownRight = Resources.Load<Sprite>(_default.sprite.name + "/DownRight");      
        _faceDownLeft = Resources.Load<Sprite>(_default.sprite.name + "/DownLeft");
    }

    // Update is called once per frame
    void Update()
    {
        float x, y;
        x = _current.x; y = _current.y;
        _current.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _current.Normalize();

        if (x != _current.x || y != _current.y)
            SetSprite();
    }

    /// <summary>
    /// Checks for current vector and sets the sprite to be matching the orientation.
    /// </summary>
    void SetSprite()
    {
        if (_current.x > 0)
        {
            if (_current.y > 0 && _faceUpRight != null)
                _default.sprite = _faceUpRight;
            else if (_current.y == 0 && _faceRight != null)
                _default.sprite = _faceRight;
            else if (_current.y < 0 && _faceDownRight != null)
                _default.sprite = _faceDownRight;
        }
        else if (_current.x == 0)
        {
            if (_current.y == 0 && _idle != null)
                _default.sprite = _idle;
            else if (_current.y > 0 && _faceUp != null)
                _default.sprite = _faceUp;
            else if (_current.y < 0 && _faceDown != null)
                _default.sprite = _faceDown;
        }
        else
        {
            if (_current.y > 0 && _faceUpLeft != null)
                _default.sprite = _faceUpLeft;
            else if (_current.y == 0 && _faceLeft != null)
                _default.sprite = _faceLeft;
            else if (_current.y < 0 && _faceDownLeft != null)
                _default.sprite = _faceDownLeft;
        }
    }
}
