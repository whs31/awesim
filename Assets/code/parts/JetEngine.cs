using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetEngine : MonoBehaviour
{
    [SerializeField] private Color m_emissionColorValue;
    [SerializeField] private float m_intensityMultiplier;
    [SerializeField] private Material m_material;

    [SerializeField] private GameObject m_physicsObject;

    [Space(10)]
    [Header("Engine Properties")]
    [Tooltip("In kilogram-force")] [SerializeField] private float m_thrust = 4100;

    void Update()
    {
        m_material.SetVector("_EmissionColor", m_emissionColorValue * m_intensityMultiplier * PlayerInput.Throttle);
    }

    private void FixedUpdate()
    {
        Vector3 forwardForce = (PlayerInput.Throttle * m_thrust) * (-m_physicsObject.transform.forward);
        m_physicsObject.GetComponent<Rigidbody>().AddForce(forwardForce);
        DrawArrow.DebugColor(m_physicsObject.transform.position, forwardForce / 500, Color.red);
    }
}
