namespace Clouds.Simulation
{
	public struct CloudCell
	{
    	public readonly int X;
		public readonly int Y;
		public readonly int Z;

		public bool hum;
		public bool act;
		public bool cld;

		public CloudCell(int x, int y, int z) {
			X = x;
			Y = y;
			Z = z;

			hum = false;
			act = false;
			cld = false;
		}
	}
}
