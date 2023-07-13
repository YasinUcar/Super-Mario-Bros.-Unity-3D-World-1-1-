using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public int Point { get; set; }
    [SerializeField] private TextMeshProUGUI pointText;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private float _countDown = 400f;
    [SerializeField] private int _coin = 0;

    private void Start()
    {
        Point = Convert.ToInt32(pointText.text.ToString());
    }
    private void Update()
    {
        CountDown();
    }
    public void AddCoin()
    {
        _coin++;
        _coinText.text = "X" + _coin.ToString("D2");
    }
    public void AddPoint(int addPoint)
    {
        Point += addPoint;
        pointText.text = Point.ToString("D6"); //literal syntax D6 : add the number to the beginning 
    }

    private void CountDown()
    {
        _countDown -= Time.deltaTime;
        _timeText.text = Convert.ToInt16(_countDown).ToString();
    }

}
