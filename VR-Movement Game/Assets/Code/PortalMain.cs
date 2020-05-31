using UnityEngine;
using UnityEngine.Rendering;

public class PortalMain : PortalBase
{
    [SerializeField] private Camera m_portalCam;
    [SerializeField] private Camera m_playerCam;

    void Update()
    {
        var relativePosition = transform.InverseTransformPoint(m_playerCam.transform.position);
        relativePosition = Vector3.Scale(relativePosition, new Vector3(-1, 1, -1));
        m_portalCam.transform.position = Other.transform.TransformPoint(relativePosition);

        var relativeRotation = transform.InverseTransformDirection(m_playerCam.transform.forward);
        relativeRotation = Vector3.Scale(relativeRotation, new Vector3(-1, 1, -1));
        m_portalCam.transform.forward = Other.transform.TransformDirection(relativeRotation);
    }
}
