namespace Task04
{
	using System;

	public class PickUp : MapObject, IPickable//todo pn я так понимаю, что это у тебя бонусы, тогда где препятствия?
	{
		public PickUp(int x, int y, Map map) : base(x, y, map) { }

		public virtual void ChangeFeatures(Player player)
		{
			throw new NotImplementedException();
		}
	}
}
