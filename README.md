# AG.PathHelpers

[![NuGet](https://img.shields.io/nuget/v/AG.PathHelpers.svg)](https://www.nuget.org/packages/AG.PathHelpers)


Cross-platform-safe path utilities for .NET applications  
Developed and maintained by [AGLogicStudio](https://github.com/AGLogicStudio)

---

## ✨ Features

- 🔹 `FilePaths.ConcatenateFilePath(...)` — Joins path segments intelligently across platforms
- 🔹 `CrossPlatformFilePathResolver` — Resolves absolute and relative paths based on runtime environment
- 🧠 Auto-detects ASP.NET vs console apps
- 🧪 Unit-tested with `xUnit`

---

## 📦 Install via NuGet


Supports .NET 8 and compatible .NET Standard targets.

---

## 🔧 Usage Examples

### Concatenate Path Segments


```csharp
using AG.PathHelpers;

// ConcatenateFilePath
// Works on Windows and Linux without worrying about slashes

string fullPath = FilePaths.ConcatenateFilePath("data", "logs", "output.txt");

// Handling empty or null segments
var path1 = FilePaths.ConcatenateFilePath(null, "", "final.txt");
// Result: "final.txt"
// Nulls and empty strings are safely ignored—no crash, no leading slashes.

        ////public static void Main(string[] args)
        ////{
        ////    Console.WriteLine("--- Test Cases ---");

        ////    // Case 1: Standard concatenation
        ////    Console.WriteLine($"Case 1: {ConcatenateFilePath("C:", "folder1", "folder2", "file.txt")}");
        ////    // Expected: C:\folder1\folder2\file.txt

        ////    // Case 2: Redundant slashes at ends of segments
        ////    Console.WriteLine($"Case 2: {ConcatenateFilePath("C:\\", "folder1\\", "\\folder2", "file.txt")}");
        ////    // Expected: C:\folder1\folder2\file.txt

        ////    // Case 3: Empty or null segments
        ////    Console.WriteLine($"Case 3: {ConcatenateFilePath("C:", "", "folder1", null, "file.txt")}");
        ////    // Expected: C:\folder1\file.txt

        ////    // Case 4: Absolute path in the middle (Path.Join behavior)
        ////    Console.WriteLine($"Case 4: {ConcatenateFilePath("C:\\Users", "Documents", "D:\\Data", "report.pdf")}");
        ////    // Expected: C:\Users\Documents\D:\Data\report.pdf (Path.Join concatenates, doesn't "reset")

        ////    // Case 5: Segments that are just slashes
        ////    Console.WriteLine($"Case 5: {ConcatenateFilePath("/", "folder", "sub", "file.txt")}");
        ////    // Expected: /folder/sub/file.txt (on Unix-like) or \folder\sub\file.txt (on Windows if first is not absolute root)

        ////    Console.WriteLine($"Case 6: {ConcatenateFilePath("folder1", "/folder2", "file.txt")}");
        ////    // Expected: folder1/folder2/file.txt (on Unix-like) or folder1\folder2\file.txt (on Windows)

        ////    Console.WriteLine($"Case 7: {ConcatenateFilePath("C:\\", "\\logs\\", "errors.log")}");
        ////    // Expected: C:\logs\errors.log

        ////    Console.WriteLine($"Case 8: {ConcatenateFilePath("file.txt")}");
        ////    // Expected: file.txt

        ////    Console.WriteLine($"Case 9: {ConcatenateFilePath("C:\\", "file.txt")}");
        ////    // Expected: C:\file.txt

        ////    Console.WriteLine($"Case 10: {ConcatenateFilePath("home", "user", "")}");
        ////    // Expected: home\user (on Windows) or home/user (on Unix)

        ////    Console.WriteLine($"Case 11: {ConcatenateFilePath("C:", "a", "b", "c")}");
        ////    // Expected: C:\a\b\c

        ////    Console.WriteLine($"Case 12: {ConcatenateFilePath("a", "b", "c")}");
        ////    // Expected: a\b\c

        ////    Console.WriteLine($"Case 13: {ConcatenateFilePath("C:\\", "a", "b", "c")}");
        ////    // Expected: C:\a\b\c
        ////}

```

### CrossPlatformFilePathResolver

```csharp
var resolver = new CrossPlatformFilePathResolver();
string absolute = resolver.GetAbsolutePath("reports/monthly.pdf");
string relative = resolver.GetRelativePath(absolute);


// Repeated Resolutions Should Be Idempotent
var resolver = new CrossPlatformFilePathResolver();
var first = resolver.GetAbsolutePath("reports/q1.csv");
var second = resolver.GetAbsolutePath(first);
Console.WriteLine(first == second); // true
//This ensures that running resolution multiple times doesn't re-flatten or alter already-absolute paths.


// GetRelativePath handles mixed separators
var resolver = new CrossPlatformFilePathResolver();
var abs = resolver.GetAbsolutePath("logs\\2025\\summary.log");
var rel = resolver.GetRelativePath(abs);
// Result: "logs/2025/summary.log" (normalized)


// Edge Case: Absolute path input with mixed case root
var resolver = new CrossPlatformFilePathResolver();
var baseDir = AppDomain.CurrentDomain.BaseDirectory;
var input = Path.Combine(baseDir.ToUpperInvariant(), "SubFolder", "file.dat");
var rel = resolver.GetRelativePath(input);
// Still returns "SubFolder/file.dat" despite casing difference
// Good for validating robustness on case-insensitive vs case-sensitive file systems.


// Resolving Paths in web like environments
// Simulate hosted environment
Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Production");
var resolver = new CrossPlatformFilePathResolver();
var webRelative = resolver.GetAbsolutePath("wwwroot/index.html");
// On a web app, this resolves relative to the app’s content root or wwwroot, depending on how you've configured GetWebRootPath.
```
