namespace Clouds.Simulation
{
	public interface ICloudSimulation
	{
		public bool SimulationPlaying {get; set;}  		
		public CloudCell UpdateCell(CloudCell oldCell, int x, int y, int z);
		public void TickSimulation();
		public void ResetSimulation();
	}
}
