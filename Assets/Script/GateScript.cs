using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GateScript : MonoBehaviour
{
    [SerializeField] private GateType _gateType = null;
    [SerializeField] private string _gateValue = "";
    [SerializeField] private TextMeshPro _gateValueText;

    private void Awake()
    {
        _gateValue = _gateType.gateValue;
    }

    private void Start()
    {
        gameObject.name = _gateValue;
        if (gameObject.layer == 7)
        {
            _gateValueText.text = "+" + _gateValue;
        }
        else if(gameObject.layer == 8)
        {
            _gateValueText.text = "-" + _gateValue;
        }
    }
}
