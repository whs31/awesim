using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetEngine : MonoBehaviour
{
    [SerializeField] private Color m_emissionColorValue;
    [SerializeField] private float m_intensityMultiplier;
    [SerializeField] private Material m_material;


    void Start()
    {
        
    }

    void Update()
    {
        m_material.SetVector("_EmissionColor", m_emissionColorValue * m_intensityMultiplier * PlayerInput.Throttle);
    }
}
