using Godot;

public partial class clicker : Node
{
	private readonly string prefabPath = "res://semangka.tscn";
    private Vector2 mousePos = Vector2.Zero;
    private PackedScene prefab = null;

    private semangka freezed = null;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        prefab = ResourceLoader.Load<PackedScene>(prefabPath);
        Summon();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustReleased("action_click"))
        {
            freezed.Set("freeze", false);
            freezed.GetNode("CollisionShape2D").Set("disabled", false);
            Summon();
        }

        if (freezed != null)
        {
            freezed.Set("position", mousePos);
        }
	}

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

		if (@event is InputEventMouseMotion ev)
        {
            mousePos = ev.Position;
        }
    }

    void Summon()
    {
        int size = GD.RandRange(1, 4);
        GD.Print(size);

        freezed = prefab.Instantiate<semangka>();
        freezed.GetNode("CollisionShape2D").Set("disabled", true);
        freezed.Set("position", mousePos);
        freezed.Set("freeze", true);
        freezed.SetType(size);
        GetNode("/root/Scene").AddChild(freezed);
    }
}
