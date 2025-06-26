
namespace AG.PathHelpers
{
    public class PathHelpers
    {
        /// <summary>
        /// Concatenates a number of string segments into a valid file path,
        /// ensuring that there are no duplicate directory separators between segments.
        /// This function uses Path.Join internally for robust path handling and performance,
        /// but includes custom logic to handle scenarios where segments might introduce
        /// redundant leading/trailing slashes.
        /// </summary>
        /// <param name="segments">An array of string segments to concatenate.</param>
        /// <returns>A consolidated, valid file path.</returns>
        public static string ConcatenateFilePath(params string[] segments)
        {
            if (segments is null || segments.Length == 0)
                return string.Empty;

            var cleanedSegments = new List<string>(segments.Length);

            foreach (var segment in segments)
            {
                if (string.IsNullOrWhiteSpace(segment))
                    continue;

                string trimmed = segment.Trim(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

                if (Path.IsPathRooted(segment) && !Path.IsPathRooted(trimmed))
                {
                    // Restore leading slash for rooted paths if trimming removed it
                    if (segment.StartsWith(Path.DirectorySeparatorChar) ||
                        segment.StartsWith(Path.AltDirectorySeparatorChar))
                    {
                        trimmed = Path.DirectorySeparatorChar + trimmed;
                    }
                }

                cleanedSegments.Add(trimmed);
            }

            return Path.Join(cleanedSegments.ToArray());
        }
    }
}
