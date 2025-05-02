using Godot;
using System;
[Tool]
public partial class Lights : Node3D
{
	[ExportCategory("Light")] 

	[Export] public Color LightRedActiveColor = new Color(1, 0, 0);
	[Export] public Color LightGreenActiveColor = new Color(0, 1, 0);
	[Export] public Color LightRedInActiveColor = new Color(0.1f, 0, 0);
	[Export] public Color LightGreenInActiveColor = new Color(0, 0.1f, 0);
	// [Export] public float LightIntensity = 1.0f;

	//initialized but not assigned
	private MeshInstance3D _lightGreen;
	private MeshInstance3D _lightRed;

	//created a reference to the TempEnvironment node to access the variable there
	//also not set yet
	private TemperatureEnvironment _tempEnv; 

	private Color _lastRedColor = new Color();
	private Color _lastGreenColor = new Color();
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_lightRed = GetNode<MeshInstance3D>("LightRed");
		_lightGreen = GetNode<MeshInstance3D>("LightGreen");

		
		GD.Print(GetPath());
		//Tells the getnode where to find the TemperatureEnvironment, ../../ is two grandparents, bit like css
		_tempEnv = GetNodeOrNull<TemperatureEnvironment>("../../TemperatureEnvironment");

		//check if the node gets assigned correctly
		if(_tempEnv == null)
		GD.PushError("TemperatureEnvironment not found!");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
    if (_tempEnv == null)
        return;

    // Make sure to update the lights in both editor and runtime
    HandleGreenLight();
    HandleRedLight();
	}

public void HandleGreenLight()
{
    var greenMat = _lightGreen.GetActiveMaterial(0) as ShaderMaterial;
    var redMat = _lightRed.GetActiveMaterial(0) as ShaderMaterial;

    if (greenMat == null || redMat == null)
        return;

    if (_tempEnv.GreenLight && !_tempEnv.RedLight)
    {
        var targetGreenColor = LightGreenActiveColor;
        var targetRedColor = LightRedInActiveColor;

        if (_lastGreenColor != targetGreenColor)
        {
            greenMat.SetShaderParameter("green_light_color", targetGreenColor);
            redMat.SetShaderParameter("red_light_color", targetRedColor);
            _lastGreenColor = targetGreenColor;
            _lastRedColor = targetRedColor;
        }
    }
}

public void HandleRedLight()
{
    var greenMat = _lightGreen.GetActiveMaterial(0) as ShaderMaterial;
    var redMat = _lightRed.GetActiveMaterial(0) as ShaderMaterial;

    if (greenMat == null || redMat == null)
        return;

    if (_tempEnv.RedLight && !_tempEnv.GreenLight)
    {
        var targetRedColor = LightRedActiveColor;
        var targetGreenColor = LightGreenInActiveColor;

        if (_lastRedColor != targetRedColor)
        {
            redMat.SetShaderParameter("red_light_color", targetRedColor);
            greenMat.SetShaderParameter("green_light_color", targetGreenColor);
            _lastGreenColor = targetGreenColor;
            _lastRedColor = targetRedColor;
        }
    }
}
}
