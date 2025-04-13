namespace Clouds.Simulation
{
	public readonly struct CloudCell
	{
		public readonly bool Hum;
		public readonly bool Act;
		public readonly bool Cld;

		public CloudCell(bool hum, bool act, bool cld) {
			Hum = hum;
			Act = act;
			Cld = cld;
		}
	}
}
