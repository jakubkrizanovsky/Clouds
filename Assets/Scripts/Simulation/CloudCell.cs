namespace Clouds.Simulation
{
	public readonly struct CloudCell
	{
		public readonly bool Hum;
		public readonly bool Act;
		public readonly bool Cld;
		public readonly bool Ext;

		public CloudCell(bool hum, bool act, bool cld, bool ext) {
			Hum = hum;
			Act = act;
			Cld = cld;
			Ext = ext;
		}
	}
}
