﻿namespace Task04
{
	using System;

	public abstract class MapObject
	{
		private int x;
		private int y;
		private Map map;

		public MapObject(int x, int y, Map map)
		{
			this.x = x;
			this.y = y;
			this.map = map;
		}

		public int X
		{
			get { return x; }
		}

		public int Y
		{
			get { return y; }
		}

		public virtual void Kill()//todo pn "убей себя" чтоли (молодец, если так)? если нет, то входные параметры нужны
		{
			throw new NotImplementedException();
		}

		public virtual void Collide(MapObject obj)
		{
			throw new NotImplementedException();
		}
	}
}
