using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBagLinia : MonoBehaviour
{
    LineRenderer _lineRenderer;

    public Transform StartPosition;
    public Transform EndPosition;
    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        SetPositions();
    }

    private void SetPositions()
    {
        _lineRenderer.SetPosition(0, StartPosition.position);
        _lineRenderer.SetPosition(1, EndPosition.position);
    }
}