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

string fullPath = FilePaths.ConcatenateFilePath("data", "logs", "output.txt");
// Works on Windows and Linux without worrying about slashes

var resolver = new CrossPlatformFilePathResolver();

string absolute = resolver.GetAbsolutePath("reports/monthly.pdf");
string relative = resolver.GetRelativePath(absolute);
