using Sandbox;

public sealed class Poop : Component
{
	[Property] public float RotationSpeed { get; set; } = 180f;
	[Property] public ParticleEffect SmellEffect { get; set; }

	private ParticleEffect activeEffect;

	protected override void OnStart()
	{
		base.OnStart();

		// Add a smell effect if configured
		if (SmellEffect != null)
		{
			var particles = Scene.CreateObject();
			particles.WorldPosition = WorldPosition;
			particles.Parent = GameObject;

			var particleComponent = particles.Components.Create<ParticleEffect>();
			// You can configure particle effects here
			activeEffect = particleComponent;
		}
	}

	protected override void OnUpdate()
	{
		base.OnUpdate();

		// Rotate the poop for extra meme effect
		var currentRotation = WorldRotation.Angles();
		currentRotation.yaw += RotationSpeed * Time.Delta;
		WorldRotation = Rotation.From(currentRotation);
	}
}
