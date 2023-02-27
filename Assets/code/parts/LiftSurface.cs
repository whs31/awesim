using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftSurface : MonoBehaviour
{
    [SerializeField] private GameObject m_physicsObject;

    [Space(10)]
    [Header("Part Properties")]
    [SerializeField] private float wingSpan = 7.15f;
    [SerializeField] private float wingArea = 23.78f;

    private float m_aspectRatio;

    void Start()
    {
        m_physicsObject.GetComponent<Rigidbody>().drag = Mathf.Epsilon;
        m_aspectRatio = (wingSpan * wingSpan) / (wingArea * 5);
    }

    // Update is called once per frame
    void Update()
    {
        // *flip sign(s) if necessary*
        var localVelocity = transform.InverseTransformDirection(m_physicsObject.GetComponent<Rigidbody>().velocity);
        var angleOfAttack = Mathf.Atan2(-localVelocity.y, -localVelocity.z);

        // α * 2 * PI * (AR / AR + 2)
        var inducedLift = angleOfAttack * (m_aspectRatio / (m_aspectRatio + 2f)) * 2f * Mathf.PI;

        // CL ^ 2 / (AR * PI)
        var inducedDrag = (inducedLift * inducedLift) / (m_aspectRatio * Mathf.PI);

        // V ^ 2 * R * 0.5 * A
        var pressure = m_physicsObject.GetComponent<Rigidbody>().velocity.sqrMagnitude * 1.2754f * 0.5f * wingArea;

        var lift = inducedLift * pressure;
        var drag = (0.021f + inducedDrag) * pressure;

        // *flip sign(s) if necessary*
        var dragDirection = m_physicsObject.GetComponent<Rigidbody>().velocity.normalized;
        var liftDirection = Vector3.Cross(dragDirection, transform.right);

        // Lift + Drag = Total Force
        m_physicsObject.GetComponent<Rigidbody>().AddForce((liftDirection * lift - dragDirection * drag));
        DrawArrow.DebugColor(m_physicsObject.transform.position, (liftDirection * lift) / 10, Color.blue);
        DrawArrow.DebugColor(m_physicsObject.transform.position, (-dragDirection * drag) / 10, Color.magenta);
    }
}
