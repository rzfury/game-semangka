using Godot;
using System;

public partial class semangka : RigidBody2D
{
    private readonly Vector2 mainSize = Vector2.One * 0.6f;
    private readonly Vector2 colliderSize = Vector2.One * 2;
	private readonly Vector2 spriteSize = Vector2.One * 0.1f;

    public int SemangkaType = 0;

	// Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		ContactMonitor = true;
		MaxContactsReported = 1;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetType(int type)
	{
		SemangkaType = type;

		int size = type + 1;
        GetNode("CollisionShape2D").Set("scale", colliderSize * mainSize * size);
        GetNode("Sprite2D").Set("scale", spriteSize * mainSize * size);
        GetNode("Sprite2D").Set("texture", ResourceLoader.Load(string.Format("res://{0}.png", type)));
    }

	public void OnBodyEntered(Node _node)
    {
        if (_node.IsQueuedForDeletion())
        {
            return;
        }

        if (_node is semangka node)
		{
			if (node.SemangkaType != SemangkaType)
			{
				return;
			}

			node.MaxContactsReported = 0;
			node.ContactMonitor = false;

			var midPos = ((Vector2)Get("position") + (Vector2)node.Get("position")) / 2f;
            GetNode("/root/Scene").EmitSignal(scene.SignalName.OnBuledCollide, midPos, node.SemangkaType);

			if (!node.IsQueuedForDeletion())
			{
				node.QueueFree();
			}

			if (!IsQueuedForDeletion())
			{
				QueueFree();
			}
		}
	}
}
