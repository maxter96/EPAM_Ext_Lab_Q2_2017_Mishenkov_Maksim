namespace Task02
{
	using System;

	public class CameEventArgs
	{
		private DateTime dateTime;

		public CameEventArgs(DateTime dateTime)
		{
			if (dateTime == null)
			{
				this.dateTime = DateTime.Now;
			}

			this.dateTime = dateTime;
		}

		public DateTime DateTime
		{
			get { return this.dateTime; }
		}
	}
}
