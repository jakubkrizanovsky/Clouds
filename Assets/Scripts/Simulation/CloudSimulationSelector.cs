using UnityEngine;

namespace Clouds.Simulation
{
    public class CloudSimulationSelector : MonoBehaviour, ICloudSimulation
    {
		[field: SerializeField]
		public DefaultCloudSimulation DefaultCloudSimulation {get; private set;}

		[field: SerializeField]
		public CustomCloudSimulation CustomCloudSimulation {get; private set;}

        public ACloudSimulation CurrentSimulation {get; set;}

        public bool SimulationPlaying {
			get => CurrentSimulation.SimulationPlaying;
			set => CurrentSimulation.SimulationPlaying = value; 
		}
        public float TickDuration {
			get => CurrentSimulation.TickDuration;
			set {
				DefaultCloudSimulation.TickDuration = value;
				CustomCloudSimulation.TickDuration = value;
			}
		}

        private void Awake() {
			CurrentSimulation = DefaultCloudSimulation;
		}

        public void TickSimulation()
        	=> CurrentSimulation.TickSimulation();

        public void ResetSimulation()
        	=> CurrentSimulation.ResetSimulation();
    }
}
