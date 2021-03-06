namespace ChangeFileName
{
	using System;
	using System.IO;
	using System.Text.RegularExpressions;

	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("**********************************************************************************");
			Console.WriteLine("* Description:\t\t 이 프로그램은 현재 file name(숫자)에 1,000을 더하도록\t *");
			Console.WriteLine("* \t\t\t 변경하는 프로그램입니다.\t\t\t\t *");
			Console.WriteLine("* Company:\t\t SY Company \t\t\t\t\t\t *");
			Console.WriteLine("* Responsibility:\t Seyoon Park \t\t\t\t\t\t *");
			Console.WriteLine("* Contact:\t\t psyoona@naver.com \t\t\t\t\t *");
			Console.WriteLine("**********************************************************************************");

			while (true)
			{
				Console.WriteLine("");
				Console.WriteLine("-------------------------------------------------------------------");
				Console.WriteLine("1. Change Directory + 1000\n2. Change File name + 1000\n3. Exit the program");
				Console.Write("Select Number: ");
				string input = Console.ReadLine();

				if (string.IsNullOrEmpty(input))
				{
					continue;
				}
				else if (input == "1")
				{
					Console.WriteLine("Please enter the directory name: ");
					input = Console.ReadLine();

					if (string.IsNullOrEmpty(input))
					{
						Console.WriteLine("Typed it wrong. Return to the initial screen.");
					}
					else
					{
						Console.WriteLine("Start changing the directory name.");

						// Directory 변경
						string[] directories = Directory.GetDirectories(input, "*", SearchOption.AllDirectories);
						Array.Sort(directories);
						Array.Reverse(directories);

						Regex regex = new Regex("^*([0-9]{4,5})*$");

						foreach (var directory in directories)
						{
							if (regex.IsMatch(directory))
							{
								string oldDirectoryNo = regex.Match(directory).Value;

								if (!string.IsNullOrEmpty(oldDirectoryNo))
								{
									int temp = Convert.ToInt32(oldDirectoryNo) + 1000;
									Directory.Move(directory, directory.Replace(oldDirectoryNo, temp.ToString()));
								}
							}
						}

						Console.WriteLine("Directory name change has been completed.");
					}
				}
				else if (input == "2")
				{
					Console.WriteLine("Please enter the directory name: ");
					input = Console.ReadLine();

					if (string.IsNullOrEmpty(input))
					{
						Console.WriteLine("Typed it wrong. Return to the initial screen.");
					}
					else
					{
						Console.WriteLine("Start changing the file name.");

						// File name 변경
						Regex regex = new Regex("^([0-9]{4,5})$");
						string[] files = Directory.GetFiles(input, "*", SearchOption.AllDirectories);

						Array.Sort(files);
						Array.Reverse(files);

						foreach (var file in files)
						{
							string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);

							if (regex.IsMatch(fileNameWithoutExtension))
							{
								string extension = Path.GetExtension(file);
								string directoryName = Path.GetDirectoryName(file);
								int oldFileNo = Convert.ToInt32(fileNameWithoutExtension);
								oldFileNo += 1000;

								File.Move(file, Path.Combine(directoryName, oldFileNo.ToString() + extension));
							}
						}

						Console.WriteLine("File name change has been completed.");
					}
				}
				else if (input == "3")
				{
					Console.WriteLine("Exit the program");
					break;
				}
			}

			Console.ReadKey();
		}
	}
}
