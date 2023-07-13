using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlackScreenController : MonoBehaviour
{
    private UndergroundWorldController _undergroundWorldController;
    [SerializeField] private GameObject _mainCam;
 

    public bool _isDown;
    private void Awake()
    {
        _undergroundWorldController = GameObject.FindGameObjectWithTag("UndergroundWorld").GetComponent<UndergroundWorldController>();
        _mainCam = GameObject.FindGameObjectWithTag("MainCamera").gameObject;
        
    }
    void Start()
    {
        StartCoroutine(DestroyAuto());
    }

    // Update is called once per frame
    IEnumerator DestroyAuto()
    {
        yield return new WaitForSeconds(1f);
        EnabledUnderground(_isDown);
        Destroy(this.gameObject);
    }
    void EnabledUnderground(bool isDown)
    {
        _isDown = isDown;
        if (_isDown)
        {
            _undergroundWorldController.enabled = true;
        }
        if (!isDown)
        {
            _undergroundWorldController.ChangeLocation();
            _undergroundWorldController.DisableGameObject();
            _mainCam.GetComponent<Camera>().enabled=true;
           
            

        }
    }
}
