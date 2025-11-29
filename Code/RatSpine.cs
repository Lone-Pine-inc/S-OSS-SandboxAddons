using Sandbox;
using System;

public sealed class RatSpine : Component
{
	[Property] public GameObject PoopPrefab { get; set; }
	[Property] public GameObject Body { get; set; }
	[Property] public SoundEvent MusicSound { get; set; }
	[Property] public SoundEvent PoopSound { get; set; }
	[Property] public Rigidbody rigidbody { get; set; }
	[Property] public float SpinSpeed { get; set; } = 380f;
	[Property] public float JumpVelocity { get; set; } = 1500f;
	[Property] public float PoopInterval { get; set; } = 3f;

	private TimeSince timeSinceLastPoop;
	private SoundHandle musicHandle;

	protected override void OnAwake()
	{
		base.OnAwake();
		timeSinceLastPoop = 0;
		
	}

	protected override void OnStart()
	{
		base.OnStart();

		// Start infinite music
		if ( MusicSound != null )
		{
			musicHandle = Sound.Play( MusicSound, WorldPosition );
			
		}
	}

	protected override void OnUpdate()
	{
		base.OnUpdate();


		var currentRotation = Body.LocalRotation.Angles();
		currentRotation.yaw += SpinSpeed * Time.Delta;
		Body.LocalRotation = Rotation.From( currentRotation );

		// Poop function
		if ( timeSinceLastPoop >= PoopInterval )
		{
			Poop();
			timeSinceLastPoop = 0;
		}

		// Update music position
		if ( musicHandle != null )
		{
			musicHandle.Position = WorldPosition;
		}
	}

	private void Poop()
	{
		if ( PoopPrefab == null ) return;

		var poopPosition = WorldPosition + Vector3.Down * 20f;
		var poop = PoopPrefab.Clone( poopPosition );

		Sound.Play( PoopSound, WorldPosition );

		var throwForce = Vector3.Random * JumpVelocity;
		rigidbody.Velocity += throwForce;

	}

	protected override void OnDestroy()
	{
		// Stop music when destroyed
		if ( musicHandle != null )
		{
			musicHandle.Stop();
		}

		base.OnDestroy();
	}
}
