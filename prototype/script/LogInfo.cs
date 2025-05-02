using Godot;
using System;
using System.Reflection.Metadata.Ecma335;

public partial class LogInfo : Node
{
	
	private TemperatureEnvironment _environment;
	private double timeAccumulator = 0;
	private FileAccess logFile;

	private string directoryPath = "user://log";
	private string filePath;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_environment = GetParent<TemperatureEnvironment>();
		filePath = directoryPath + "/kas_log.txt";
		SaveToFile();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_environment == null)
		{
			GD.PushError("Environment not found!");
			return;
		}

		timeAccumulator += delta;
		if (timeAccumulator >= 1.0) // every 1 second
		{
			timeAccumulator = 0;

			DateTime logDate = DateTime.Now;
			logFile.StoreString($"[{logDate:HH:mm:ss}] H: {Math.Round(_environment.Humidity, 1)} T: {Math.Round(_environment.Temperature, 1)}\n");
		}
	}

  	private void SaveToFile()
	{

		var dir = DirAccess.Open(directoryPath);
		if (dir == null)
		{
			var userDir = DirAccess.Open("user://");
			if (userDir == null)
			{
				GD.PushError("Failed to open user directory");
				return;
			}

			Error err = userDir.MakeDirRecursive("Log");
			if (err != Error.Ok)
			{
				GD.PushError("Failed to create Log");
				return;
			}

		}

		logFile = FileAccess.Open(filePath, FileAccess.ModeFlags.Write);
		if (logFile == null)
		{
			GD.Print("Failed to open: " + filePath);
			return;
		}
		
		logFile.SeekEnd();
		GD.Print("Log file created at: " + ProjectSettings.GlobalizePath(filePath));	}

    public override void _ExitTree()
    {
        // base._ExitTree();
		logFile?.Close();
		OS.ShellOpen(ProjectSettings.GlobalizePath(directoryPath));
    }

}
