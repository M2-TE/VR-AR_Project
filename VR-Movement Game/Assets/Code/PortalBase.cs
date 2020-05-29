using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class PortalBase : MonoBehaviour
{
    [SerializeField] protected PortalBase m_other;
}
