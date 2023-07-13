using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndergroundWorldController : MonoBehaviour
{
    [SerializeField] private GameObject _mario;
    [SerializeField] private Transform _newLocation;
    [SerializeField] private GameObject _underGroundCamera;
    [SerializeField] private CheckPointManager _checkPointManager;
    [SerializeField] Canvas _uiCanvas;
    void Start()
    {

        Camera.main.enabled = false;
        _uiCanvas.worldCamera = _underGroundCamera.GetComponent<Camera>();
        _underGroundCamera.SetActive(true);

        _mario.transform.position = new Vector3(36.11f, -9.07f, 0);
        _mario.GetComponent<MovementController>().enabled = true;

    }
    public void ChangeLocation()
    {
        _checkPointManager.enabled = false;
        _mario.transform.position = _newLocation.position;
        _mario.GetComponent<SpriteRenderer>().sortingOrder = 0;
        _mario.GetComponent<MovementController>().enabled = true;
    }

  public void DisableGameObject()
    {
        _underGroundCamera.SetActive(false);
    }



}
