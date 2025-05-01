using Godot;
using System;

public partial class Lights : Node3D
{
	[ExportCategory("Light")] 

	[Export] public Color LightRedActiveColor = new Color(1, 0, 0);
	[Export] public Color LightGreenActiveColor = new Color(0, 1, 0);
	[Export] public float LightIntensity = 1.0f;

	//initialized but not assigned
	private MeshInstance3D _lightGreen;
	private MeshInstance3D _lightRed;

	//created a reference to the TempEnvironment node to access the variable there
	//also not set yet
	private TemperatureEnvironment _tempEnv; 
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_lightRed = GetNode<MeshInstance3D>("LightRed");
		_lightGreen = GetNode<MeshInstance3D>("LightGreen");

		// GD.Print(_lightGreen);
		// GD.Print(_lightRed);

		//Tells the getnode where to find the TemperatureEnvironment, ../../ is two grandparents, bit like css
		_tempEnv = GetNodeOrNull<TemperatureEnvironment>("../../TemperatureEnvironment");

		//check if the node gets assigned correctly
		if(_tempEnv == null)
		GD.PushError("TemperatureEnvironment not found!");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
