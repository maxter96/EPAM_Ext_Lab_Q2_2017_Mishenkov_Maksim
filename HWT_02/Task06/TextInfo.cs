namespace Task06
{
	using System.Collections.Generic;
	using System.Text;

	public class TextInfo
	{
		private Parameter[] parameters;

		public TextInfo()
		{
			parameters = new Parameter[]
			{
				new Parameter("Bold"),
				new Parameter("Italic"),
				new Parameter("Underline")
			};
		}

		public bool ChangeParameter(int index)
		{
			if (index < 0 || index >= parameters.Length)
			{
				return false;
			}

			parameters[index].IsActive = !parameters[index].IsActive;
			return true;
		}

		public void ClearParameters()
		{
			for (int i = 0; i < parameters.Length; i++)
			{
				parameters[i].IsActive = false;
			}
		}

		public string GetInfo()
		{
			StringBuilder builder = new StringBuilder();
			int numOfParams = 0;

			for (int i = 0; i < parameters.Length; i++)
			{
				if (parameters[i].IsActive)
				{
					string name = parameters[i].Name;
					builder.Append(numOfParams == 0 ? name : ", " + name);
					numOfParams++;
				}
			}

			if (builder.Length == 0)
			{
				builder.Append("None");
			}

			return builder.ToString();
		}

		public IEnumerable<Parameter> GetParameters()
		{
			for (int i = 0; i < parameters.Length; i++)
			{
				yield return parameters[i];
			}
		}
	}
}
