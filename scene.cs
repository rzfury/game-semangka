using Godot;
using System;

public partial class scene : Node2D
{
	[Signal] public delegate void OnBuledCollideEventHandler(Vector2 pos, int type);

    private readonly string prefabPath = "res://semangka.tscn";

    public Vector2 currentCollidedPos { get; private set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public async void SpawnLater(semangka buled)
	{
		await ToSignal(GetTree().CreateTimer(0.1), "timeout");
        GetNode("/root/Scene").CallDeferred("add_child", buled);

    }

	public void BuledCollide(Vector2 pos, int type)
    {
        var x = MathF.Floor(pos.X);
        var y = MathF.Floor(pos.Y);
        var cx = MathF.Floor(currentCollidedPos.X);
		var cy = MathF.Floor(currentCollidedPos.Y);

		if (x == cx && y == cy) { return; }

        var prefab = ResourceLoader.Load<PackedScene>(prefabPath);
		var buled = prefab.Instantiate<semangka>();
        buled.Set("position", pos);
		buled.SetType(type == 7 ? 0 : type + 1);
		//SpawnLater(buled);
        GetNode("/root/Scene").CallDeferred("add_child", buled);

        currentCollidedPos = pos;
    }
}
