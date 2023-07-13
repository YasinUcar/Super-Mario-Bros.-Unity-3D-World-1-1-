using UnityEngine;

public class ChangeMarioSprite : MonoBehaviour
{

    [SerializeField] private Sprite _smalljumpSprite;
    [SerializeField] private Sprite _bigjumpSprite;

    [SerializeField] Sprite _smallMario;
    [SerializeField] private Sprite _bigMario;
    [SerializeField] private Sprite _fireFlowerMario;
    [SerializeField] private Sprite _jumpFireFlowerMario;
    [SerializeField] private Sprite _downFireFlowerMario;

    [SerializeField] private Sprite _marioDown;
    private SpriteRenderer _spriteRenderer;

    private MarioManager _marioManager;
    private string _currentSprite="Normal";

    private ChangeMarioCollider _changeMarioCollider;
    private JumpController _jumpController;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _changeMarioCollider = GetComponent<ChangeMarioCollider>();
        _marioManager = GameObject.FindGameObjectWithTag("MarioManager").GetComponent<MarioManager>();
        _jumpController = GetComponentInChildren<JumpController>();
    }


    public void ChangeCurrentMarioSprite(string currentSprite)
    {
        _currentSprite = currentSprite;
        switch (_currentSprite)
        {
            case "Jump":
                if (_marioManager.CurrentMarioSize())
                    if (_marioManager.CurrentFireFlowerStatus())
                        _spriteRenderer.sprite = _jumpFireFlowerMario;
                    else
                        _spriteRenderer.sprite = _bigjumpSprite;

                else
                    _spriteRenderer.sprite = _smalljumpSprite;
                break;
            case "Normal":

                if (_marioManager.CurrentMarioSize())
                {
                    if (_marioManager.CurrentFireFlowerStatus())
                        _spriteRenderer.sprite = _fireFlowerMario;
                    else
                        _spriteRenderer.sprite = _bigMario;
                    _changeMarioCollider.ChangeCurrentMarioCollider("Big");
                }
                else
                {
                    _spriteRenderer.sprite = _smallMario;
                    _changeMarioCollider.ChangeCurrentMarioCollider("Small");
                }
                break;
            case "Down":
                if (_jumpController.CurrentDownKeyStatus())
                {
                    if (_marioManager.CurrentFireFlowerStatus())
                        _spriteRenderer.sprite = _downFireFlowerMario;
                    else
                        _spriteRenderer.sprite = _marioDown;
                    _changeMarioCollider.ChangeCurrentMarioCollider("Down");
                }
                break;
            default:
                _spriteRenderer.sprite = _smallMario;
                break;
        }
    }
 
}
