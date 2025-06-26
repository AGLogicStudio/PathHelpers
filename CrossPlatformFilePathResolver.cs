namespace AG.PathHelpers
{
    public class CrossPlatformFilePathResolver
    {
        private readonly bool _isWebApplication;

        public CrossPlatformFilePathResolver()
        {
            _isWebApplication = DetectWebEnvironment();
        }

        public string GetAbsolutePath(string relativePath, bool ensureDirectoryExists = false)
        {
            string baseDirectory = GetBaseDirectory();
            string fullPath = Path.Combine(baseDirectory, NormalizePath(relativePath));

            if (ensureDirectoryExists)
            {
                EnsureDirectory(fullPath);
            }

            return fullPath;
        }

        public string GetRelativePath(string absolutePath)
        {
            string baseDirectory = GetBaseDirectory();
            return Path.GetRelativePath(baseDirectory, absolutePath);
        }

        private string GetBaseDirectory()
        {
            return _isWebApplication ? GetWebRootPath() : AppDomain.CurrentDomain.BaseDirectory;
        }

        private string GetWebRootPath()
        {
            var webRootPath = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != null
                ? Directory.GetCurrentDirectory()
                : null;

            return webRootPath ?? AppDomain.CurrentDomain.BaseDirectory;
        }

        private bool DetectWebEnvironment()
        {
            // Try detecting common ASP.NET Core namespaces to infer a web environment
            return AppDomain.CurrentDomain.GetAssemblies()
                .Any(assembly => assembly.FullName?.StartsWith("Microsoft.AspNetCore") == true);
        }

        private void EnsureDirectory(string filePath)
        {
            string? directory = Path.GetDirectoryName(filePath);
            if (directory != null && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        private string NormalizePath(string path)
        {
            return path.Replace("\\", "/").TrimStart('/');
        }

        public bool FileExists(string relativePath)
        {
            string absolutePath = GetAbsolutePath(relativePath);
            return File.Exists(absolutePath);
        }
        public bool FileExistsWithPermissions(string relativePath, bool requireRead = true, bool requireWrite = false)
        {
            string absolutePath = GetAbsolutePath(relativePath);

            if (!File.Exists(absolutePath))
            {
                return false; // File does not exist
            }

            FileInfo fileInfo = new FileInfo(absolutePath);

            bool canRead = requireRead ? fileInfo.IsReadOnly == false || fileInfo.Exists : true;
            bool canWrite = requireWrite ? fileInfo.IsReadOnly == false : true;

            return canRead && canWrite;
        }


    }
}