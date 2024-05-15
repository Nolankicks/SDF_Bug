using Sandbox;
using Sandbox.Network;
using Sandbox.Sdf;
using System.Threading.Tasks;
public sealed class Sdfbug : Component
{
	[Property] public Sdf3DWorld sdf3DWorld { get; set; }
	[Property] public Sdf3DVolume sdf3DVolume { get; set; }
	protected override void OnStart()
	{
		_ = BuildWorld();
	}

	public async Task BuildWorld()
	{
		var box = new BoxSdf3D(Vector3.Zero, new Vector3(50000, 50000, 100));
		await sdf3DWorld.AddAsync(box, sdf3DVolume);
		GameNetworkSystem.CreateLobby();
	}
}
