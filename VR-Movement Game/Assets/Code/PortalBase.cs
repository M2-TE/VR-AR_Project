using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class PortalBase : MonoBehaviour
{
    public PortalBase Other;
}
