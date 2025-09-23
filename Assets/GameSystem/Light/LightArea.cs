using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

[RequireComponent(typeof(CircleCollider2D))]
public class LightArea : MonoBehaviour
{
    Light2D _light;
    CircleCollider2D col;
    public float radius;
    void Awake()
    {
        _light = GetComponent<Light2D>();
        col = GetComponent<CircleCollider2D>();
        radius = _light.pointLightOuterRadius;
        col.radius = radius;
        col.isTrigger = true;
    }

    public void UpdateLight(float lightValue)
    {
        radius = lightValue;
        _light.pointLightOuterRadius = radius;
        col.radius = radius;
    }
}
