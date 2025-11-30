using Sandbox;
using Sandbox.Network;

public sealed class Network : Component
{
	protected override void OnUpdate()
	{

	}

	protected override void OnAwake()
	{
		base.OnAwake();

		var lobby = new LobbyConfig();
		lobby.AutoSwitchToBestHost = false;
		Networking.CreateLobby( lobby );
	}
}
