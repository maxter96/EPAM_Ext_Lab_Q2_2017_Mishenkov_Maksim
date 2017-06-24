using System.ComponentModel;

namespace Task01
{
	enum Post
	{
		[Description("Генеральный директор")]
		GeneralDirector,
        [Description("Аналитик")]
        Analyst,
        [Description("Менеджер проекта")]
        ProjectManager,
        [Description("Тестировщик")]
        SoftwareTester,
        [Description("Разработчик")]
        SoftwareEngineer
	}
}
