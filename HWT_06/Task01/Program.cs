namespace Task01
{
	using System;
	using System.Text;

	public class Program
	{
		private static void Main(string[] args)
		{
			Console.InputEncoding = Encoding.Unicode;
			Console.OutputEncoding = Encoding.Unicode;

			Employee emp1 = new Employee("Мишенков", "Максим", "Андреевич", new DateTime(1996, 9, 29), 0, Post.SoftwareEngineer);
			Console.WriteLine("До изменений:\n{0}\n\n", emp1);
			emp1.WorkExperience = 2;
			Console.WriteLine("Изменен стаж:\n{0}\n\n", emp1);
			emp1.Post = Post.ProjectManager;
			Console.WriteLine("Изменена должность:\n{0}\n\n", emp1);
			Console.ReadKey();
		}
	}
}
