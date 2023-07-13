using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector2 _followOffset;
    [SerializeField] private GameObject _targetObject; //kameran�n takip edece�i obje
    [SerializeField] private float _cameraLerp = 3f;
    private Vector2 _threshold;
    private void Awake()
    {
        this.transform.position = new Vector3(-2.57f, -0.64f, -10f);
    }
    private void Start()
    {
        _threshold = CalculateThreshold(); //ilk atanan de�eri al�yoruz

    }
    private void FixedUpdate()
    {
        if (_targetObject != null)
        {
            Vector2 follow = _targetObject.transform.position;

            Vector3 newPosition = transform.position;//mevcut pozisyonu koruyoruz
            if (follow.x > _threshold.x) //E�er takip edilen obje belirtilen alan�  ge�tiyse
            {
                newPosition.x = _threshold.x; //pozisyonu takip edilen objenin ge�ti�i nokta olarak belirliyoruz
                _threshold = new Vector2(follow.x, _threshold.y);

            }

            transform.position = Vector3.MoveTowards(transform.position, newPosition, _cameraLerp * Time.deltaTime); //Mevcut kamera pozisyonunu MoveTowards komutuyla yumu�ak bir �ekilde yeni pozisyona ge�mesini sa�l�yoruz
        }
        else
        {
          
            print("Bulam�yoruz");
        }
    }
    private Vector2 CalculateThreshold()
    {
        Rect aspect = Camera.main.pixelRect; //ana kameran�n ekran �zerindeki alan�n� al�yoruz
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize); //mevcut camera orthograpicSize'�m�za g�re aspect'in y�ksekli�ini ve geni�li�ini al�p vector 2 cinsinden d�nd�r�yoruz.
        t.x -= _followOffset.x; //Unity edit�r�nden verdi�imiz offsetlere g�re mevcut ��z�n�rl�kten bir s�n�r belirliyoruz.
        t.y -= _followOffset.y; //Y de�erini bu proje'de kullanmayaca��z Super Mario Bros'da kemara a��lar� sadece X de�erleriyle de�i�iyor. Fakat burada Gizmoz'da daha tutarl� bir g�r�n�m elde edilmesi ad�na eklemekte yarar var.
        return t;
    }
    private void OnDrawGizmos() //Ekranda verdi�imiz vect�rlere g�re bir alanda �izim yapar
    {
        Gizmos.color = Color.yellow;
        Vector2 border = _threshold;
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y, 1)); //gelen de�erleri daha mant�kl� g�r�nmesi ad�na 2'ye katl�yoruz.
    }
}
