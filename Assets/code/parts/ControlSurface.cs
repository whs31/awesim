using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputAxis
{
    Pitch,
    Yaw,
    Roll,
    Throttle
}

public enum Orientation
{
    Horizontal,
    Vertical
}    

public class ControlSurface : MonoBehaviour
{
    [SerializeField] private GameObject m_physicsObject;

    [Space(10)]
    [Header("Part Properties")]
    [Tooltip("In degrees")][SerializeField] private float m_maxDeflectionAngle = 15;
    [SerializeField] private float m_torqueMultiplier = 50;
    [SerializeField] private bool m_inverse = false;
    [SerializeField] private InputAxis m_axis = InputAxis.Pitch;
    [SerializeField] private Orientation m_orientation = Orientation.Horizontal;

    private float m_axisValue;
    private Vector3 m_torqueAxis;

    private void FixedUpdate()
    {
        switch (m_axis)
        {
            case InputAxis.Pitch:
                m_axisValue = PlayerInput.Pitch;
                m_torqueAxis = transform.right;
                break;
            case InputAxis.Yaw:
                m_axisValue = PlayerInput.Yaw;
                m_torqueAxis = transform.up;
                break;
            case InputAxis.Roll:
                m_axisValue = PlayerInput.Roll;
                m_torqueAxis = transform.forward;
                break;
            case InputAxis.Throttle:
                m_axisValue = PlayerInput.Throttle;
                break;
            default: 
                break;
        }

        //apply force to physical body
        m_physicsObject.GetComponent<Rigidbody>().AddTorque(-m_torqueAxis * m_torqueMultiplier * m_axisValue);

        if (m_inverse)
            m_axisValue *= -1;
        Quaternion _startRotation = transform.localRotation;
        Quaternion _endRotation = Quaternion.Euler(m_orientation == Orientation.Horizontal ? m_maxDeflectionAngle * m_axisValue : transform.localEulerAngles.x,
                                                   m_orientation == Orientation.Horizontal ? transform.localEulerAngles.y : m_maxDeflectionAngle * m_axisValue,
                                                   transform.localEulerAngles.z);
        DrawArrow.DebugColor(transform.position, transform.up, Color.yellow);
        transform.localRotation = Quaternion.Lerp(_startRotation, _endRotation, 4 * Time.fixedDeltaTime);
    }
}
