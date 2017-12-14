using System;
using System.IO.MemoryMappedFiles;
using System.Numerics;
using Engine;
using Engine.Utils;
using Jitter.LinearMath;
using OpenTK.Input;
using Engine;

public class HelloWorldComponent : GameComponent, ILogicComponent, IRenderComponent
{
	private int _count = 0;
	private RigidBodyComponent _body;

	public float speed = 1000f;

	public void Start()
	{
		_body = gameObject.GetComponent<RigidBodyComponent>();
		
		if (_body != null)
		{
			Game.Instance.window.Mouse.ButtonDown += (sender, e) =>
			{
				var horizontalSign = (e.Button == 0) ? 1 : -1;
				var magnitude = 10000.0f;

				var movement = new JVector(horizontalSign * 1, 1, 0) * magnitude;
				_body.rigidBody.AddForce(movement);
			};
		}
	}
	
	public void Update() {
		++_count;

		/*if (_count > 5) {
			SetActive(false);
		}
		*/
		var position = gameObject.transform.position;
		var rotatin = gameObject.transform.rotation;
		
		var angle = MathUtils.Deg2Rad((10 * _count / 60.0f));
		
		//gameObject.transform.rotation = Quaternion.CreateFromAxisAngle(new Vector3(1, 1, 1), angle * 2);
		//gameObject.transform.scale = 0.5f * Vector3.One * (float) Math.Sin(angle);

		var velocity = _body.rigidBody.LinearVelocity;
		var frameSpeed = speed / 60.0f;
		
		var new2DVelocity = new Vector2(Input.GetAxis(Input.Axis.Horizontal),
										Input.GetAxis(Input.Axis.Vertical));

		if (new2DVelocity.LengthSquared() != 0)
		{
			new2DVelocity /= new2DVelocity.Length();
			new2DVelocity *= frameSpeed;
		}

		velocity.X = new2DVelocity.X;
		velocity.Z = -new2DVelocity.Y;

		_body.rigidBody.LinearVelocity = velocity;
		
		if (Input.GetButtonDown("Jump")) _body.rigidBody.AddForce(new JVector(0, 10000.0f, 0));
		
		//Play shotgun sound if player shoots
        //if (Input.GetButtonDown("Shoot")) gameObject.GetComponent<SpeakerComponent>().Play(@"..\..\Resources\Audio\shotgun-mossberg590.mp3");
	}

	public void Render() {
		//Console.Out.WriteLine ("hello, world: " + _count);
	}
}

