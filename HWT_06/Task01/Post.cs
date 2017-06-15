using System.ComponentModel;

namespace Task01
{
	enum Post
	{
		[Description("Генеральный директор")]//todo pn лучше уж так
		GeneralDirector,
		Analyst,
		ProjectManager,
		SoftwareTester,
		SoftwareEngineer
	}
}
