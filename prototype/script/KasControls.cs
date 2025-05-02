using Godot;
using System;

public partial class KasControls : Control
{
	private TemperatureEnvironment tempEnv;
	private Label _tempLabel;
	private Label _humidLabel;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
		tempEnv = GetParent().GetNode<TemperatureEnvironment>("%TemperatureEnvironment");
		if (tempEnv == null)
		{
			GD.PushError("Can't find TemperatureEnvironment!");
			return;
		}

	
		_tempLabel = GetNode<Label>("MarginContainer/Panel/HFlowContainer/TempValue");
		_humidLabel= GetNode<Label>("MarginContainer/Panel/HFlowContainer/HumidValue");
		
		if (_tempLabel == null)
			GD.PushError("Can't find TempValue label!");
		if(_humidLabel == null)	
			GD.PushError("Can't find Humidity!");

		GD.Print("KasControls ready.");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
			if (tempEnv == null || _tempLabel == null || _humidLabel == null) return;
			

			_tempLabel.Text = $"{Math.Round(tempEnv.Temperature, 1)} °C";
			_humidLabel.Text = $"{Math.Round(tempEnv.Humidity, 1)} %";
	}


}
