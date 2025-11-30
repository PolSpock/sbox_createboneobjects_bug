using Sandbox;

public sealed class GameObjectSpawner : Component
{
	protected override void OnUpdate()
	{

		if ( Input.Pressed( "attack1" ) )
		{
			for ( int i = 0; i < 1; i++ )
			{
				var rootGameObject = new GameObject(); // CleanMain
				//rootGameObject.NetworkSpawn( null );

				for ( int j = 0; j < 10; j++ )
				{
					var childGameObject = new GameObject(); // CleanChild
					childGameObject.SetParent( rootGameObject );
					//childGameObject.NetworkSpawn( null );

					for ( int k = 0; k < 10; k++ )
					{
						var grandChildGameObject = new GameObject(); // CleanNode
						grandChildGameObject.SetParent( childGameObject );
						//grandChildGameObject.NetworkSpawn( null );

						for ( int l = 0; l < 20; l++ )
						{
							var greatGrandChildGameObject = new GameObject(); // CleanPiece
							greatGrandChildGameObject.SetParent( grandChildGameObject );
							//greatGrandChildGameObject.NetworkSpawn( null );
						}
					}
				}

				rootGameObject.NetworkSpawn( null );

			}
		}

	}
}
