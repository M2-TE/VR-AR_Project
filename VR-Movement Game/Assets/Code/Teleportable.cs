using UnityEngine;

public class Teleportable : MonoBehaviour
{
    PortalBase m_currentPortal;
    GameObject m_clone;
    

    private void Start()
    {
        if(m_clone == null)
        {
            m_clone = Instantiate(gameObject, Vector3.zero, Quaternion.identity, null);
            m_clone.SetActive(false);
            m_clone.GetComponent<Teleportable>().m_clone = gameObject;
        }
    }

    private void Update()
    {
        // check if teleportable has passed the teleporter threshhold
        if(m_currentPortal != null)
        {
            var relVec = transform.position - m_currentPortal.transform.position;
            var angle = Vector3.Angle(m_currentPortal.transform.forward, relVec);

            // move clone in connected portal area
            if(angle > 90f)
            {
                var cam = GetComponentInChildren<Camera>();
                var relativePosition = m_currentPortal.transform.InverseTransformPoint(transform.position);
                relativePosition = Vector3.Scale(relativePosition, new Vector3(-1, 1, -1));
                m_clone.transform.position = m_currentPortal.Other.transform.TransformPoint(relativePosition);

                var relativeRotation = m_currentPortal.transform.InverseTransformDirection(cam.transform.forward);
                relativeRotation = Vector3.Scale(relativeRotation, new Vector3(-1, 1, -1));
                m_clone.transform.forward = m_currentPortal.Other.transform.TransformDirection(relativeRotation);

                cam.transform.rotation = Quaternion.identity;
            }
            else
            {
                // swap with clone
                m_clone.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        m_currentPortal = other.GetComponent<PortalBase>();
    }
    private void OnTriggerExit(Collider other)
    {
        m_currentPortal = other.GetComponent<PortalBase>();
    }
}
