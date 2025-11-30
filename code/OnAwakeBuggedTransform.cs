using System;

public sealed class OnAwakeBuggedTransform : Component
{
	[Property] public GameObject CreatedGameObject { get; set; }
	[Property] public GameObject goBone { get; set; }

	protected override void OnAwake()
	{
		Log.Info( GetType().Name + " Creating GameObject with BuggedTransform component." );

		CreatedGameObject = this.GameObject;
		CreatedGameObject.Name = "Bugged OnAwake";

		////////////////////////////////////////////////////////

		bool startEnabled = false;

		////////////////////////////////////////////////////////

		var goSub = new GameObject
		{
			Parent = CreatedGameObject,
			Name = "Sub",
			Enabled = startEnabled,
		};

		var filePath = "props/destructions/v3/construction_wood/subs/door/damaged/";
		foreach ( var file in FileSystem.Mounted.FindFile( filePath, "*.vmdl" ) )
		{
			if ( !file.EndsWith( ".vmdl", StringComparison.OrdinalIgnoreCase ) ) continue;

			var modelPath = $"{filePath}{file}";
			var model = Model.Load( modelPath );

			var piece = new GameObject
			{
				Name = model.ResourceName,
				Parent = goSub,
				WorldTransform = goSub.WorldTransform,
				Enabled = startEnabled
			};

			var modelRenderer = piece.Components.Create<SkinnedModelRenderer>();
			modelRenderer.Model = model;
			modelRenderer.CreateBoneObjects = true;

			var goBone = piece.Children.FirstOrDefault();
			if ( !goBone.IsValid() )
			{
				Log.Warning( "Bone is missing for " + file + "!" );
				continue;
			}

			var goModelCollider = new GameObject
			{
				Name = "ModelCollider",
				Parent = goBone,
				WorldTransform = piece.WorldTransform,
			};

			var cleanModelCollider = goModelCollider.Components.Create<ModelCollider>();
			cleanModelCollider.Model = model;

			piece.Enabled = startEnabled;
		}

	}

}
