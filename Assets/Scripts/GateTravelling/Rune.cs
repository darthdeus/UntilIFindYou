using UnityEngine;
using System.Collections;
using System.Linq;
using Assets;
using UnityEngine.UI;

public class Rune : MonoBehaviour
{
    private DialingBook _book;

    public string RuneName;
    public string RuneTranslation;

    private AudioSource _sound;
    private bool _alreadyClicked = false;
    private GameObject _dialingBook;
    private Text _text;
    private Image _bg;

    void Start()
    {
        _book = DialingBook.Find();
        _text = GameObject.Find("RuneTooltip").GetComponent<Text>();
        _bg = GameObject.Find("RuneTextBg").GetComponent<Image>();
        _dialingBook = GameObject.Find("DialingBook");

        _text.enabled = false;
        _bg.enabled = false;

        var sounds = GetComponentsInParent<AudioSource>();

        foreach (var sound in sounds) {
            if (sound.clip.name == "GotItem") {
                _sound = sound;
            }
        }
    }

    void Update()
    {
  //      _text.transform.position = GameObject.Find("RuneTooltipPosition").transform.position;
        _bg.transform.position = GameObject.Find("RuneTooltipPosition").transform.position;
    }

    void OnMouseOver()
    {
        if (_dialingBook.GetComponent<DialingBook>()._isVisible) {
            _text.text = RuneTranslation;
            _text.enabled = true;
            _bg.enabled = true;
        }
    }

    void OnMouseExit()
    {
        _text.enabled = false;
        _bg.enabled = false;
    }

    void OnMouseUp()
    {
        if (_book.GetComponent<Renderer>().isVisible) {
            if (_book.CurrentAddress.Length < 4) {
                _sound.Play();
            }

            if (_book.CurrentAddress.Length < 5)
                if (_alreadyClicked) {
                    _book.UnsetSlot();
                    _alreadyClicked = false;

                    GetComponent<SpriteRenderer>().material.color = Color.white;
                } else {
                    _book.SetSlot(RuneName);
                    _alreadyClicked = true;

                    GetComponent<SpriteRenderer>().material.color = Color.red;
                }
        }
    }

    public void Reset()
    {
        GetComponent<SpriteRenderer>().material.color = Color.white;
        _alreadyClicked = false;
    }
}
