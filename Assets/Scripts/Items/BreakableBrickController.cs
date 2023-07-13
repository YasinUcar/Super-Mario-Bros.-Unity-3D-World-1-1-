using DG.Tweening;
using UnityEngine;
public class BreakableBrickController : MonoBehaviour
{

    [SerializeField] private GameObject _animationObject;
    [SerializeField] private AudioClip _firstSFX;
    [SerializeField] private AudioClip _secondSFX;
    private MarioManager _marioManager;

    private Vector2 _firstLocation;

    private void Awake()
    {
        _marioManager = GameObject.FindGameObjectWithTag("MarioManager").GetComponent<MarioManager>();

        _firstLocation = transform.position;
    }
    private void Start()
    {
        if (_marioManager == null)
        {
            Debug.LogError("MarioManager.cs null!");
        }
    }
    public void Movement()
    {
        if (_marioManager.CurrentMarioSize() == true)
        {
            AnimationManager.Instance.EnableAnimator(_animationObject);
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            AudioManager.Instance.PlaySound(_secondSFX);
            Destroy(this.gameObject, 5f);
            return;
        }
        else
        {
            transform.DOMoveY(0.15f, 0.5f).OnComplete(() =>
            {
                transform.DOMoveY(_firstLocation.y, 0.5f);
                AudioManager.Instance.PlaySound(_firstSFX);
            });
        }
    }
}
