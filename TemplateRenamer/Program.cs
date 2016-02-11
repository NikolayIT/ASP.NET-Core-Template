namespace TemplateRenamer
{
    using System;
    using System.IO;
    using System.Text;

    public static class Program
    {
        private const string OriginalName = "MvcTemplate";

        public static void Main()
        {
            Console.WriteLine("ASP.NET MVC Template Renamer v1.0");
            Console.WriteLine("Working in: " + Environment.CurrentDirectory);

            var newName = string.Empty;
            while (string.IsNullOrWhiteSpace(newName))
            {
                Console.Write("What is your project's name ([a-zA-z]): ");
                newName = Console.ReadLine();
            }

            Console.WriteLine("Renaming directories...");
            RenameDirectories(Environment.CurrentDirectory, OriginalName, newName);
            Console.WriteLine("Directories renamed.");

            Console.WriteLine("Renaming files...");
            RenameFiles(Environment.CurrentDirectory, OriginalName, newName);
            Console.WriteLine("Files renamed.");

            Console.WriteLine("Renaming file contents...");
            RenameFileContents(Environment.CurrentDirectory, OriginalName, newName);
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
                if (!file.EndsWith(".exe"))
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
