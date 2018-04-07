using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour {


    void Start()
    {
        SetBrickRandomColor();
    }

    private void SetBrickRandomColor()
    {
        Renderer rend = GetComponent<Renderer>();

        rend.material.color = Random.ColorHSV();
    }

}
