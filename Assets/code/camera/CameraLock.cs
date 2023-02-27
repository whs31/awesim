using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraLock : MonoBehaviour
{
    private CinemachineFreeLook m_camera;
    private float m_storedMaxX;
    private float m_storedMaxY;

    void Start()
    {
        m_camera = GetComponent<CinemachineFreeLook>();
        m_storedMaxX = m_camera.m_XAxis.m_MaxSpeed;
        m_storedMaxY = m_camera.m_YAxis.m_MaxSpeed;
        Lock();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
            Unlock();
        if (Input.GetMouseButtonUp(1))
            Lock();
    }

    public void Lock()
    {
        m_camera.m_XAxis.m_MaxSpeed = 0;
        m_camera.m_YAxis.m_MaxSpeed = 0;
    }
    public void Unlock()
    {
        m_camera.m_XAxis.m_MaxSpeed = m_storedMaxX;
        m_camera.m_YAxis.m_MaxSpeed = m_storedMaxY;
    }
}
