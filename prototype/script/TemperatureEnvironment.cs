using Godot;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Tools;

[Tool]
public partial class TemperatureEnvironment : Node
{
	[Export(PropertyHint.Range, "1, 100, 1")]
	public float Temperature = 20;
	[Export(PropertyHint.Range, "1, 100, 1")]
	public float Humidity = 60;

	[ExportCategory("Regulators")]
	[Export] public bool WindowOpen = true;
	[Export] public bool SprinklersOn = false;

	public bool RedLight = false;
	public bool GreenLight = true;

	private float _tempMin = 15;
	private float _tempMax = 25;
	private float _humidMin = 50;
	private float _humidMax = 70;

	private Lights lights;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        lights = GetNodeOrNull<Lights>("../KasMesh/Lights");

        if (lights == null)
        {
            GD.PushError("Can't find the Lights node!");
			return;
        }
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (lights == null)
		{
			lights = GetNodeOrNull<Lights>("../KasMesh/Lights");
			if (lights == null) return;
		}

		if (!Engine.IsEditorHint())
		{
			Regulator(delta); 
		}

 		KasCheck();       // Runs temperature check in editor and at runtime
	}
		

	protected void Regulator(double delta)
	{
        float tempDelta = (float)(delta * 1);  // Speed factor
        float humidDelta = (float)(delta * 1);

		Temperature += WindowOpen ? -tempDelta : tempDelta;
		Temperature = Mathf.Clamp(Temperature, 1, 100);

		Humidity += SprinklersOn ? humidDelta : -humidDelta;
		Humidity = Mathf.Clamp(Humidity, 1, 100);

	} 

		private void KasCheck()
		{
			if (Temperature.IsBetween(_tempMin, _tempMax) && Humidity.IsBetween(_humidMin, _humidMax))
			{
				GreenLight = true;
				RedLight = false;
			
			}
			else{
				GreenLight = false;
				RedLight = true;
			
			}

			// GD.Print($"Temp: {Mathf.Round(Temperature * 10) / 10.0f} °C, Humid: {Mathf.Round(Humidity * 10) / 10.0f} %");
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
