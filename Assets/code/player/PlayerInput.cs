using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("Input values (readonly)")]
    [Range(-1.0f, 1.0f)]    [SerializeField]    private static float m_pitch;
    [Range(-1.0f, 1.0f)]    [SerializeField]    private static float m_yaw;
    [Range(-1.0f, 1.0f)]    [SerializeField]    private static float m_roll;
    [Range(0.0f, 1.0f)]     [SerializeField]    private static float m_throttle;
    public static float Pitch { get { return m_pitch; } private set { m_pitch = value; } }
    public static float Yaw { get { return m_yaw; } private set { m_yaw = value; } }
    public static float Roll { get { return m_roll; } private set { m_roll = value;} }
    public static float Throttle { get { return m_throttle; } private set { m_throttle = value; } }

    void Start()
    {
        
    }

    void Update()
    {
        getInput();
    }

    void getInput()
    {
        Pitch = Input.GetAxisRaw("Pitch");
        Yaw = Input.GetAxisRaw("Yaw");
        Roll = Input.GetAxisRaw("Roll");
        Throttle = Mathf.Clamp(Throttle + Input.GetAxisRaw("Throttle") / 300, 0, 1);
    }
}
