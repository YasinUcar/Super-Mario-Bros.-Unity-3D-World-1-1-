using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector2 _followOffset;
    [SerializeField] private GameObject _targetObject; //kameranýn takip edeceði obje
    [SerializeField] private float _cameraLerp = 3f;
    private Vector2 _threshold;
    private void Awake()
    {
        this.transform.position = new Vector3(-2.57f, -0.64f, -10f);
    }
    private void Start()
    {
        _threshold = CalculateThreshold(); //ilk atanan deðeri alýyoruz

    }
    private void FixedUpdate()
    {
        if (_targetObject != null)
        {
            Vector2 follow = _targetObject.transform.position;

            Vector3 newPosition = transform.position;//mevcut pozisyonu koruyoruz
            if (follow.x > _threshold.x) //Eðer takip edilen obje belirtilen alaný  geçtiyse
            {
                newPosition.x = _threshold.x; //pozisyonu takip edilen objenin geçtiði nokta olarak belirliyoruz
                _threshold = new Vector2(follow.x, _threshold.y);

            }

            transform.position = Vector3.MoveTowards(transform.position, newPosition, _cameraLerp * Time.deltaTime); //Mevcut kamera pozisyonunu MoveTowards komutuyla yumuþak bir þekilde yeni pozisyona geçmesini saðlýyoruz
        }
        else
        {
          
            print("Bulamýyoruz");
        }
    }
    private Vector2 CalculateThreshold()
    {
        Rect aspect = Camera.main.pixelRect; //ana kameranýn ekran üzerindeki alanýný alýyoruz
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize); //mevcut camera orthograpicSize'ýmýza göre aspect'in yüksekliðini ve geniþliðini alýp vector 2 cinsinden döndürüyoruz.
        t.x -= _followOffset.x; //Unity editöründen verdiðimiz offsetlere göre mevcut çözünürlükten bir sýnýr belirliyoruz.
        t.y -= _followOffset.y; //Y deðerini bu proje'de kullanmayacaðýz Super Mario Bros'da kemara açýlarý sadece X deðerleriyle deðiþiyor. Fakat burada Gizmoz'da daha tutarlý bir görünüm elde edilmesi adýna eklemekte yarar var.
        return t;
    }
    private void OnDrawGizmos() //Ekranda verdiðimiz vectörlere göre bir alanda çizim yapar
    {
        Gizmos.color = Color.yellow;
        Vector2 border = _threshold;
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y, 1)); //gelen deðerleri daha mantýklý görünmesi adýna 2'ye katlýyoruz.
    }
}
