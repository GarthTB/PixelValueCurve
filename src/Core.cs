namespace PixelValueCurve.src;

internal static class Core
{
    internal static void Run()
    {
        Console.WriteLine("载入配置文件...");
        var config = FileUtils.LoadConfig();
        if (config.InputConfig is null
            || config.InputConfig.Dir is null
            || config.InputConfig.IsImage is null
            || config.InputConfig.Target is null)
            throw new ArgumentException("配置文件中输入配置不完整");
        if (config.OutputConfig is null
            || config.OutputConfig.Data is null
            || config.OutputConfig.Plot is null)
            throw new ArgumentException("配置文件中输出配置不完整");
        if (config.OutputConfig is { Data: false, Plot: false })
            throw new ArgumentException("不要求任何输出，什么都没做");

        Console.WriteLine("载入成功。读取待测文件...");
        if (config.InputConfig.IsImage == true)
            ProcessImages(config);
        else ProcessVideos(config);
    }

    private static void ProcessImages(MainConfig config)
    {
        if (config.ImageConfig is null
            || config.ImageConfig.SortBy is null
            || config.ImageConfig.Descending is null)
            throw new ArgumentException("配置文件中图片配置不完整");

        var files = Directory.GetFiles(config.InputConfig!.Dir!)
            .Select(filePath => new FileInfo(filePath))
            .SortFiles(config.ImageConfig)
            .ToArray();
        if (files.Length == 0)
            throw new ArgumentException("没有任何待测文件");

        Console.WriteLine($"读取完成。开始处理{files.Length}张图片...");
        ImageUtils.Process(config, files);
    }

    private static void ProcessVideos(MainConfig config)
    {
        if (config is { VideoConfig.FrameCount: 0 })
            throw new ArgumentException("待测帧数为0，什么都没做");

        var files = Directory.GetFiles(config.InputConfig!.Dir!)
            .Select(filePath => new FileInfo(filePath))
            .ToArray();
        if (files.Length == 0)
            throw new ArgumentException("没有任何待测文件");

        Console.WriteLine($"读取完成。开始处理{files.Length}个视频...");
        VideoUtils.Process(config, files);
    }
}
