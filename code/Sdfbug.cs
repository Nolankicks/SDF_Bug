using Sandbox;
using Sandbox.Network;
using Sandbox.Sdf;
using System.Threading.Tasks;
public sealed class Sdfbug : Component
{
	[Property] public Sdf3DWorld sdf3DWorld { get; set; }
	[Property] public Sdf3DVolume sdf3DVolume { get; set; }
	public enum Repo
	{
		Repo1,
		Repo2,
		ExpectedResult
	}
	[Property] public Repo repo { get; set; }
	protected override void OnStart()
	{
		if (Networking.IsHost)
		{
		if (repo == Repo.Repo1)
		{
			_ = BuildWorld1();
		}
		else if (repo == Repo.Repo2)
		{
			_ = BuildWorld2();
		}
		else if (repo == Repo.ExpectedResult)
		{
			_ = Working();
		}
		}

	}
	//Repo 1
	public async Task BuildWorld1()
	{
		var box = new BoxSdf3D(Vector3.Zero, new Vector3(10000, 10000, 500));
		await sdf3DWorld.AddAsync(box, sdf3DVolume);
		GameNetworkSystem.CreateLobby();
	}
	//Repo 2
	public async Task BuildWorld2()
	{
		var chunkSize = 500;
		for (int x = 0; x < 50; x++)
		{
			for (int y = 0; y < 50; y++)
			{
				var box = new BoxSdf3D(Vector3.Zero, new Vector3(chunkSize, chunkSize, 500)).Transform(new Vector3(x * chunkSize, y * chunkSize, 0));
				await sdf3DWorld.AddAsync(box, sdf3DVolume);
			}
		}
		GameNetworkSystem.CreateLobby();
	}

	public async Task Working()
	{
		var box = new BoxSdf3D(Vector3.Zero, new Vector3(1000, 1000, 500));
		await sdf3DWorld.AddAsync(box, sdf3DVolume);
		GameNetworkSystem.CreateLobby();
	}
}
