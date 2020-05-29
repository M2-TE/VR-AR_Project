using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;

public class PortalCam : MonoBehaviour
{
    [SerializeField] private Camera m_playerCam;
    [SerializeField] private Transform m_portalPair;
    [SerializeField] private Transform m_portalOther;

    private Transform m_playerCamTransf;
    private Transform m_transf;
    private Camera m_camera;

    void Start()
    {
        m_transf = GetComponent<Transform>();
        m_camera = GetComponent<Camera>();
        m_playerCamTransf = m_playerCam.transform;
    }

    void Update()
    {
        var relativePosition = m_portalPair.InverseTransformPoint(m_playerCam.transform.position);
        relativePosition = Vector3.Scale(relativePosition, new Vector3(-1, 1, -1));
        transform.position = m_portalOther.TransformPoint(relativePosition);

        var relativeRotation = m_portalPair.InverseTransformDirection(m_playerCam.transform.forward);
        relativeRotation = Vector3.Scale(relativeRotation, new Vector3(-1, 1, -1));
        transform.forward = m_portalOther.TransformDirection(relativeRotation); 

        //var diff = m_playerCamTransf.position - m_portalPair.position;
        //m_transf.position = m_portalOther.position - diff;
        //m_transf.position = new Vector3(m_transf.position.x, m_playerCamTransf.position.y, m_transf.position.z);
        
        //var euler = m_playerCamTransf.rotation.eulerAngles;
        //m_transf.rotation = Quaternion.Euler(euler.x, euler.y + 180f, euler.z);
    }
}