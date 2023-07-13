using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    [SerializeField] AudioClip _jumpSFX;
    private MovementController _movementController;
    private MarioManager _marioManager;
    ChangeMarioSprite _changeMarioSprite;
    private bool _isDown;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private float _jumpTime;
    private float _jumpTimeCounter;
    private Rigidbody2D _rigidbody2D;
    private bool _isKey;
    private bool _isJumping;
    private void Awake()
    {
        _marioManager = GameObject.FindGameObjectWithTag("MarioManager").GetComponent<MarioManager>();
        if (GetComponentInParent<MovementController>() != null)
            _movementController = GetComponentInParent<MovementController>();
        else
            Debug.LogError("Movement Controller null!");
        _rigidbody2D = GetComponentInParent<Rigidbody2D>();
        _changeMarioSprite = GetComponentInParent<ChangeMarioSprite>();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        _changeMarioSprite.ChangeCurrentMarioSprite("Normal");
        _isJumping = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        _isJumping = false;
        _changeMarioSprite.ChangeCurrentMarioSprite("Jump");

    }
    void Jumping()
    {
        if (_isJumping && Input.GetButtonDown("Jump"))
        {
            _marioManager.ChangeJumpStatus(true);
           
            if (_jumpSFX != null)
                AudioManager.Instance.PlaySound(_jumpSFX);
            else
                Debug.LogError("_jumpSFX null!");
            _isKey = true;
            _jumpTimeCounter = _jumpTime;
            _rigidbody2D.velocity = Vector3.up * _jumpForce;

        }
        if (Input.GetButton("Jump") && _isKey == true)
        {
            if (_jumpTimeCounter > 0)
            {
              
                _rigidbody2D.velocity = Vector3.up * _jumpForce;
                _jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                _isKey = false;
               
                _marioManager.ChangeJumpStatus(false);
            }

        }
        if (Input.GetButtonUp("Jump"))
        {
          
            _isKey = false;
        }



    }

    private void Update()
    {
        DownKey();
        Jumping();
    }
    private void DownKey()
    {
        if (_marioManager.CurrentMarioSize())
        {
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                _changeMarioSprite.ChangeCurrentMarioSprite("Down");
                _isDown = true;
            }

            else
                _isDown = false;
        }
    }
    public bool CurrentDownKeyStatus()
    {
        return _isDown;
    }
}
