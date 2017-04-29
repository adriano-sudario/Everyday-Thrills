using EverydayThrills.JsonModels;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace EverydayThrills.Code
{
    static class Loader
    {
        static string contentFullPath;
        public static string ContentFullPath
        {
            get
            {
                if (contentFullPath == null || contentFullPath == "")
                {
                    contentFullPath = GetSolutionDirectoryPath();
                }

                return contentFullPath;
            }
        }

        static SaveModel saveData;

        public static SaveModel SaveData
        {
            get
            {
                if (saveData == null)
                {
                    saveData = LoadDeserializedJsonFile<SaveModel>("save");
                }

                return saveData;
            }
        }

        private static ContentManager content;

        public static void Initialize(ContentManager content)
        {
            Loader.content = content;
        }

        public static Texture2D LoadTexture(string textureName)
        {
            return content.Load<Texture2D>("Graphics\\" + textureName);
        }

        public static T LoadDeserializedJsonFile<T>(string fileName)
        {
            string jsonString = LoadJsonFile(fileName);
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public class Opa
        {
            public int Id;

            [JsonConstructor]
            public Opa(int id)
            {
                Id = id;
            }
        }

        public class Eita : Opa
        {
            public string Name;

            [JsonConstructor]
            public Eita(string name, int id) : base(id)
            {
                Name = name;
            }
        }

        private static string LoadJsonFile(string fileName)
        {
            return File.ReadAllText(Path.Combine(ContentFullPath, "Data\\" + fileName + ".json"));
        }

        private static object DeserializeJsonFile(string jsonString)
        {
            return JsonConvert.DeserializeObject<object>(jsonString);
        }

        private static string GetSolutionDirectoryPath(string currentPath = null)
        {
            var directory = new DirectoryInfo(
                currentPath ?? Directory.GetCurrentDirectory());
            while (directory != null && !directory.GetDirectories("Content").Any())
            {
                directory = directory.Parent;
            }
            return directory.GetDirectories("Content")[0].FullName;
        }
    }
}
