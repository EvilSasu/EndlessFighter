using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindUICamera : MonoBehaviour
{
    private Canvas _canvas;
    private const string CAM_TAG = "UICamera";
    private void Awake()
    {
        GameObject _game = GameObject.FindGameObjectWithTag(CAM_TAG);
        if( _game != null)
        {
            Camera _cam = _game.GetComponent<Camera>();
            _canvas = GetComponent<Canvas>();
            _canvas.worldCamera = _cam;
        }     
    }
}
