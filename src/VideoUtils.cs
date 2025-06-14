using OpenCvSharp;

namespace PixelValueCurve.src;

internal static class VideoUtils
{
    internal static void Process(MainConfig config, FileInfo[] files)
    {
        var start = config.VideoConfig?.StartFrame ?? 0;
        var end = start + config.VideoConfig?.FrameCount - 1;
        for (int i = 0; i < files.Length; i++)
        {
            try
            {
                Console.WriteLine($"开始处理第{i + 1}个视频...");
                var data = ProcessFile(config, files[i]);
                Console.WriteLine("处理完成。\n输出结果...");
                var outputName = end is null
                    ? $"{files[i].Name}_{start}-end_result"
                    : $"{files[i].Name}_{start}-{end}_result";
                var outputPathRoot = Path.Combine(config.InputConfig!.Dir!, outputName);
                OutputUtils.Output(config.OutputConfig!, data, outputPathRoot);
                Console.WriteLine("输出完成。");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"第{i + 1}个视频处理出错：{ex.Message}\n"
                    + $"继续处理下一个视频。");
                Console.ResetColor();
            }
        }
    }

    private static double[] ProcessFile(MainConfig config, FileInfo fileInfo)
    {
        using VideoCapture vid = new(fileInfo.FullName);
        if (vid.FrameCount == 0)
            throw new Exception("视频总帧数为0，什么都没做");

        var start = (int?)config.VideoConfig?.StartFrame ?? 0;
        var end = config.VideoConfig?.FrameCount is null
            || start + (int)config.VideoConfig?.FrameCount! > vid.FrameCount
            ? vid.FrameCount - 1
            : start + (int)config.VideoConfig?.FrameCount! - 1;
        if (!vid.Set(VideoCaptureProperties.PosFrames, start))
            throw new Exception("设置起始帧失败");

        using Mat frame = new();
        var data = new double[end - start + 1];
        Console.WriteLine($"共需处理{data.Length}帧...");
        for (int i = 0; i < data.Length; i++)
        {
            if (!vid.Read(frame))
                throw new Exception("读取视频帧失败");
            data[i] = frame.GetROI(config.RoiConfig)
                .ToFloat()
                .MeanValue(config.InputConfig!.Target);
            Console.Write($"\r已处理{i + 1}帧...");
        }
        return data;
    }
}
