using Tomlyn;

namespace PixelValueCurve.src;

internal static class FileUtils
{
    internal static MainConfig LoadConfig()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "config.toml");
        if (!File.Exists(path))
            throw new FileNotFoundException("找不到配置文件");
        var text = File.ReadAllText(path);
        return Toml.ToModel<MainConfig>(text);
    }

    internal static IOrderedEnumerable<FileInfo> SortFiles(
        this IEnumerable<FileInfo> files, ImageConfig imageConfig)
    {
        Func<FileInfo, object> method = imageConfig.SortBy switch
        {
            0 => p => p.Name,
            1 => p => p.CreationTime,
            2 => p => p.LastWriteTime,
            3 => p => p.Length,
            _ => throw new ArgumentException("配置文件中图片排序方式错误")
        };
        return imageConfig.Descending == true
            ? files.OrderByDescending(method)
            : files.OrderBy(method);
    }

    internal static string GetUniqueOutputPath(string root, string extension)
    {
        var path = $"{root}.{extension}";
        for (int i = 2; File.Exists(path); i++)
            path = $"{root}_{i}.{extension}";
        return path;
    }
}
