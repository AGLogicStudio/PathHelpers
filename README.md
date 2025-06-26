# AG.PathHelpers

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
