using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(CircleCollider2D))]
public class LightArea : MonoBehaviour
{
    Light2D _light;
    CircleCollider2D col;
    void Start()
    {
        _light = GetComponent<Light2D>();
        col = GetComponent<CircleCollider2D>();
        col.radius = _light.pointLightOuterRadius;
        col.isTrigger = true;
    }
}
