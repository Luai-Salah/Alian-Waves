using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable CS0618 // Type or member is obsolete
public class PlayerSetup : NetworkBehaviour
#pragma warning restore CS0618 // Type or member is obsolete
{
	[SerializeField] private Behaviour[] m_ComponentsToDisable;

	private void Start()
	{
		if (!isLocalPlayer)
		{
			foreach (Behaviour b in m_ComponentsToDisable)
				b.enabled = false;
		}
	}
}