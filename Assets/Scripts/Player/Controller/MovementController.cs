using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [Header("Movement Speed Controller")]
    [SerializeField] private float _movementSpeed;
   
     [SerializeField] private float _acceleration = 0.1f; //karakterin hýzlanma oraný
    [SerializeField] private float _maxSpeed=10f;
   
    //[SerializeField] private float _jumpSpeed = 10f;
    private Rigidbody2D _rigidbody2D;
    private float _defaultMovementSpeed;
    //Vector3 vertical;


    //private ChangeMarioSprite _changeMarioSprite;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
      //  _changeMarioSprite = GetComponent<ChangeMarioSprite>();
        _defaultMovementSpeed = _movementSpeed;
    }
    void FixedUpdate()
    {
        Move();
    }
    private void Update()
    {
        FlipSprite();
    }
    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Vector2 mInput = new Vector3(horizontal * _movementSpeed, _rigidbody2D.velocity.y);
        
        _rigidbody2D.velocity = mInput;        
        if(Input.GetButton("Fire3"))
        {
            print("Çalýþýyorum");
                _movementSpeed += _acceleration*Time.deltaTime;
            _movementSpeed = Mathf.Clamp(_movementSpeed, 0f, _maxSpeed);
              
            
            
        }
        else 
        {
            _movementSpeed = _defaultMovementSpeed;
        }
    }

    //public void Jump(bool _isJump)
    //{
    //    if (_isJump)
    //    {

    //        _rigidbody2D.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
    //        _changeMarioSprite.ChangeCurrentMarioSprite("Jump");
    //    }
    //    else if (!_isJump)
    //    {
    //        _changeMarioSprite.ChangeCurrentMarioSprite("Normal");
    //    }
    //}
    void FlipSprite()
    {
        bool playerhasHorizontal = Mathf.Abs(_rigidbody2D.velocity.x) > Mathf.Epsilon;
        if (playerhasHorizontal)
        {
            transform.localScale = new Vector2(Mathf.Sign(_rigidbody2D.velocity.x), 1f);
        }
    }

}
