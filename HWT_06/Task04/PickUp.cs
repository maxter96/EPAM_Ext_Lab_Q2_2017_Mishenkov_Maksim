namespace Task04
{
	using System;

	public class PickUp : MapObject, IPickable
	{
		public PickUp(int x, int y, Map map) : base(x, y, map) { }

		public virtual void ChangeFeatures(Player player)
		{
			throw new NotImplementedException();
		}
	}
}
