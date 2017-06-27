namespace Task02
{
	using System;

	public class CameEventArgs
	{
		private DateTime dateTime;

		public CameEventArgs(DateTime dateTime)
		{
			this.dateTime = dateTime;
		}

		public DateTime DateTime
		{
			get { return this.dateTime; }
		}
	}
}
