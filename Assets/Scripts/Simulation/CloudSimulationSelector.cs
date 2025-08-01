using UnityEngine;

namespace Clouds.Simulation
{
    public class CloudSimulationSelector : MonoBehaviour, ICloudSimulation
    {
		[field: SerializeField]
		public DefaultCloudSimulation DefaultCloudSimulation {get; private set;}

		[field: SerializeField]
		public CustomCloudSimulation CustomCloudSimulation {get; private set;}

		[field: SerializeField]
        private ACloudSimulation CurrentSimulation {get; set;}

        public bool SimulationPlaying {
			get => CurrentSimulation.SimulationPlaying;
			set => CurrentSimulation.SimulationPlaying = value; 
		}

		private void Awake() {
			CurrentSimulation = CustomCloudSimulation;
		}

        public CloudCell UpdateCell(CloudCell oldCell, int x, int y, int z)
			=> CurrentSimulation.UpdateCell(oldCell, x, y, z);

        public void TickSimulation()
        	=> CurrentSimulation.TickSimulation();

        public void ResetSimulation()
        	=> CurrentSimulation.ResetSimulation();
    }
}
