namespace TemplateRenamer
{
    using System;
    using System.IO;
    using System.Text;

    public static class Program
    {
        public static void Main()
        {
            Console.WriteLine(new string('=', 40));
            Console.WriteLine("Template Renamer");
            Console.WriteLine(new string('-', 40));
            Console.WriteLine("Working in: " + Environment.CurrentDirectory);
            Console.WriteLine(new string('=', 40));
            Console.WriteLine();


            var oldName = "AspNetCoreTemplate";
            while (string.IsNullOrWhiteSpace(oldName))
            {
                Console.Write("What is your project's old name ([A-Z][a-z]): ");
                oldName = Console.ReadLine();
            }

            var newName = string.Empty;
            while (string.IsNullOrWhiteSpace(newName))
            {
                Console.Write("What is your project's name ([A-Z][a-z]): ");
                newName = Console.ReadLine();
            }

            Console.WriteLine("Renaming directories...");
            RenameDirectories(Environment.CurrentDirectory, oldName, newName);
            Console.WriteLine("Directories renamed.");

            Console.WriteLine("Renaming files...");
            RenameFiles(Environment.CurrentDirectory, oldName, newName);
            Console.WriteLine("Files renamed.");

            Console.WriteLine("Renaming file contents...");
            RenameFileContents(Environment.CurrentDirectory, oldName, newName);
            Console.WriteLine("File contents renamed.");

            Console.WriteLine("Done!");
        }

        private static void RenameDirectories(string currentDirectory, string originalName, string newName)
        {
            var directories = Directory.GetDirectories(currentDirectory);
            foreach (var directory in directories)
            {
                var newDirectoryName = directory.Replace(originalName, newName);
                if (newDirectoryName != directory)
                {
                    Directory.Move(directory, newDirectoryName);
                }
            }

            directories = Directory.GetDirectories(currentDirectory);
            foreach (var directory in directories)
            {
                RenameDirectories(directory, originalName, newName);
            }
        }

        private static void RenameFiles(string currentDirectory, string originalName, string newName)
        {
            var files = Directory.GetFiles(currentDirectory);
            foreach (var file in files)
            {
                var newFileName = file.Replace(originalName, newName);
                if (newFileName != file)
                {
                    Directory.Move(file, newFileName);
                }
            }

            var subDirectories = Directory.GetDirectories(currentDirectory);
            foreach (var directory in subDirectories)
            {
                RenameFiles(directory, originalName, newName);
            }
        }

        private static void RenameFileContents(string currentDirectory, string originalName, string newName)
        {
            var files = Directory.GetFiles(currentDirectory);
            foreach (var file in files)
            {
                if (!file.EndsWith(".exe") && !file.EndsWith(".dll") && !file.EndsWith(".runtimeconfig.json"))
                {
                    var contents = File.ReadAllText(file);
                    contents = contents.Replace(originalName, newName);
                    File.WriteAllText(file, contents, Encoding.UTF8);
                }
            }

            var subDirectories = Directory.GetDirectories(currentDirectory);
            foreach (var directory in subDirectories)
            {
                RenameFileContents(directory, originalName, newName);
            }
        }
    }
}
