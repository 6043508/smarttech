using Godot;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Tools;

[Tool]
public partial class TemperatureEnvironment : Node
{
	[Export(PropertyHint.Range, "1, 100, 1")]
	public float Temperature = 17;
	[Export(PropertyHint.Range, "1, 100, 1")]
	public float Humidity = 70;

	[ExportCategory("Regulators")]
	[Export] public bool WindowOpen = true;
	[Export] public bool SprinklersOn = false;

	private bool _redLight = false;
	private bool _greenLight = true;

	private float _tempMin = 15;
	private float _tempMax = 25;
	private float _humidMin = 50;
	private float _humidMax = 70;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!Engine.IsEditorHint())
			Regulator(delta);
		}

	private void Regulator(double delta)
	{
        float tempDelta = (float)(delta * 1);  // Speed factor
        float humidDelta = (float)(delta * 1);

		Temperature += WindowOpen ? tempDelta : -tempDelta;
		Temperature = Mathf.Clamp(Temperature, 1, 100);

		Humidity += SprinklersOn ? humidDelta : -humidDelta;
		Humidity = Mathf.Clamp(Humidity, 1, 100);

		GD.Print($"Temp: {Mathf.Round(Temperature * 10) / 10.0f} °C, Humid: {Mathf.Round(Humidity * 10) / 10.0f} %");

		if (Temperature < _tempMin)
		{
			WindowOpen = false;
		}
		else if (Temperature > _tempMax)
		{
			WindowOpen = true;
		}

		if (Humidity < _humidMin)
		{
			SprinklersOn = true;
		}
		else if (Humidity > _humidMax)
		{
			SprinklersOn = false;
		}

		if (Temperature.IsBetween(_tempMin, _tempMax) && Humidity.IsBetween(_humidMin, _humidMax))
		{
			_greenLight = true;
			_redLight = false;
			GD.Print("Green");
		}
		else{
			_greenLight = false;
			_redLight = true;
			GD.Print("Red");
		}
	} 

	public override void _Input(InputEvent @event)
{
    if (@event is InputEventKey keyEvent && keyEvent.Pressed && !keyEvent.Echo)
    {
        if (keyEvent.Keycode == Key.Escape)
        {
            GetTree().Quit();
        }
    }
}

}
