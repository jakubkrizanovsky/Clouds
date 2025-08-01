namespace Clouds.Simulation
{
	public interface ICloudSimulation
	{
		public bool SimulationPlaying {get; set;}
		public float TickDuration {get; set;}
		public void TickSimulation();
		public void ResetSimulation();
	}
}
