using Unity.VisualScripting;
using UnityEngine;

public class ItemsCollisionController : MonoBehaviour
{
    [SerializeField] private GameObject _colliderDie;
    private UIManager _uIManager;
    private MarioManager _m_Manager;
    private ChangeMarioSprite _changeMarioSprite;
    private MarioRaycast _marioRaycast;
    bool _isDownStatus;
    private void Awake()
    {
        _m_Manager = GameObject.FindGameObjectWithTag("MarioManager").GetComponent<MarioManager>();
        _changeMarioSprite = GetComponent<ChangeMarioSprite>();
        _marioRaycast = GetComponent<MarioRaycast>();
        _uIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_marioRaycast.CurrentRayStatus())
        {
            if (collision.gameObject.CompareTag("BreakableMark"))
            {
                if (collision.gameObject.GetComponent<PowerUpManager>() != null)
                    collision.gameObject.GetComponent<PowerUpManager>().ChangePower();
                if (collision.gameObject.GetComponentInChildren<QuestionMarkManager>() != null)
                {
                    collision.gameObject.GetComponentInChildren<QuestionMarkManager>().Enabled();
                    _uIManager.AddPoint(200);
                    _uIManager.AddCoin();
                }

            }
            if (collision.gameObject.CompareTag("BreakableMark1UP"))
            {
                if (collision.gameObject.GetComponent<PowerUpManager>() != null)
                    collision.gameObject.GetComponent<PowerUpManager>().ChangePower();
                collision.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                collision.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            }
            if (collision.gameObject.CompareTag("BreakableMark") || collision.gameObject.CompareTag("BreakableMarkHit") || collision.gameObject.CompareTag("BreakableMark1UP"))
            {

                if (collision.gameObject.GetComponent<ChangeSprite>() != null)
                    collision.gameObject.GetComponent<ChangeSprite>().Change();
                else
                    Debug.Log("ChangeSprite.cs null!");
                if (collision.gameObject.GetComponent<ChangeTag>() != null)
                    collision.gameObject.GetComponent<ChangeTag>().Change();

                else
                    Debug.LogError("ChangeTag.cs null!");
                //if (collision.gameObject.GetComponent<PlayAnimItems>() != null)
                //    collision.gameObject.GetComponentInChildren<PlayAnimItems>().PlayAnim();
                //else
                //    Debug.LogError("PlayAnim.cs null!");

            }




            if (collision.gameObject.CompareTag("BreakableBrick"))
            {
                if (collision.gameObject.GetComponent<BreakableBrickController>() != null)
                {
                    collision.gameObject.GetComponent<BreakableBrickController>().Movement();

                }
                else
                    Debug.LogError("BreakableBreakController null!");
            }
        }
        if (collision.gameObject.CompareTag("Mushroom"))
        {
            collision.gameObject.GetComponentInParent<PowerUpManager>().CollisionSFX();
            collision.gameObject.GetComponent<Mushrooms>().StartCoroutine("DestroyObject");
            _uIManager.AddPoint(1000);

            _m_Manager.ChangeMarioSize(true);

            _changeMarioSprite.ChangeCurrentMarioSprite("Normal");


        }
        else if (collision.gameObject.CompareTag("FireFlower"))
        {
            _m_Manager.ChangeFireFlowerStatus(true);
            _uIManager.AddPoint(1000);
            collision.gameObject.GetComponentInParent<PowerUpManager>().CollisionSFX();
            collision.gameObject.GetComponent<FireFlower>().StartCoroutine("DestroyObject");
            _changeMarioSprite.ChangeCurrentMarioSprite("Normal");

        }
        else if (collision.gameObject.CompareTag("1UP"))
        {
            collision.gameObject.GetComponentInParent<PowerUpManager>().CollisionSFX();
            collision.gameObject.GetComponent<Mushrooms>().StartCoroutine("DestroyObject");
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Coin"))
        {
            _uIManager.AddPoint(200);
            _uIManager.AddCoin();
            Destroy(collision.gameObject);
        }
         if (collision.gameObject.tag==("PipeUnderground"))
        {
            GetComponent<MovementController>().enabled = false;
            collision.gameObject.GetComponent<PipeUnderground>().PipeController();
        }
        //else if (collision.gameObject.CompareTag("PipeDown"))
        //{
        //    print("Çaliþti");
        //    if (_m_Manager.CurrentDownKey())
        //    {
        //        GetComponent<MovementController>().enabled = false;
        //        collision.gameObject.GetComponentInParent<PipeDown>().PipeController();
        //    }
        //}

    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (!_isDownStatus)
        {
            if (collision.gameObject.CompareTag("PipeDown"))
            {

                if (_m_Manager.CurrentDownKey())
                {
                    _colliderDie.SetActive(false);
                   
                    
                    GetComponent<MovementController>().enabled = false;
                    GetComponent<SpriteRenderer>().sortingOrder = -500;
                    GetComponent<Rigidbody2D>().bodyType=RigidbodyType2D.Dynamic;
                    
                    collision.gameObject.GetComponentInParent<PipeDown>().PipeController();
                    _isDownStatus = true;

                }
            }
        }
    }

}
