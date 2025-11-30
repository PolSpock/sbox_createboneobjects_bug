using System;

public sealed class OnStartBuggedTransform : Component
{
	[Property] public GameObject CreatedGameObject { get; set; }

	protected override void OnStart()
	{
		Log.Info( GetType().Name + " Creating GameObject component." );

		CreatedGameObject = this.GameObject;
		CreatedGameObject.Name = "Bugged OnStart";

		////////////////////////////////////////////////////////

		var goSub = new GameObject
		{
			Parent = CreatedGameObject,
			Name = "Sub",
			Enabled = true,
		};

		var filePath = "props/destructions/v3/construction_wood/subs/door/damaged/";
		foreach ( var file in FileSystem.Mounted.FindFile( filePath, "*.vmdl" ) )
		{
			if ( !file.EndsWith( ".vmdl", StringComparison.OrdinalIgnoreCase ) ) continue;

			var modelPath = $"{filePath}{file}";
			var model = Model.Load( modelPath );

			bool startEnabled = true; // turn false to get the bug

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

	protected override void OnUpdate()
	{

		if ( Input.Pressed( "Attack1" ) )
		{
			Log.Info( GetType().Name + " Enabling GameObject component." );
			CreatedGameObject.Enabled = true;

			foreach ( var child in CreatedGameObject.GetAllObjects( false ) )
			{
				child.Enabled = true;
			}

			foreach ( var modelRender in CreatedGameObject.Components.GetAll<SkinnedModelRenderer>( FindMode.EverythingInDescendants ) )
			{
				modelRender.Sequence.Name = "door_open";
				modelRender.Sequence.Looping = false;
			}

		}

	}
}
