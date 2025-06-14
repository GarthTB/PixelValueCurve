using OpenCvSharp;

namespace PixelValueCurve.src;

internal static class ImageUtils
{
    internal static void Process(MainConfig config, FileInfo[] sortedFiles)
    {
        var data = new double[sortedFiles.Length];
        for (var i = 0; i < sortedFiles.Length; i++)
        {
            data[i] = new Mat(sortedFiles[i].FullName, ImreadModes.Unchanged)
                .GetROI(config.RoiConfig)
                .ToFloat()
                .MeanValue(config.InputConfig!.Target);
            Console.Write($"\r已处理{i + 1}张图片...");
        }
        Console.WriteLine("处理完成。\n输出结果...");
        var outputName = sortedFiles.Length == 1
            ? $"{sortedFiles[0].Name}_result"
            : $"{sortedFiles[0].Name}-{sortedFiles[^1].Name}_result";
        var outputPathRoot = Path.Combine(config.InputConfig!.Dir!, outputName);
        OutputUtils.Output(config.OutputConfig!, data, outputPathRoot);
        Console.WriteLine("输出完成。");
    }

    internal static Mat GetROI(this Mat image, RoiConfig? roiConfig)
        => roiConfig is null
        || roiConfig.TopLeftX is null
        || roiConfig.TopLeftY is null
        || roiConfig.Width is null
        || roiConfig.Height is null
            ? image
            : image.SubMat(
                new Rect((int)roiConfig.TopLeftX,
                    (int)roiConfig.TopLeftY,
                    (int)roiConfig.Width,
                    (int)roiConfig.Height));

    internal static Mat ToFloat(this Mat image)
    {
        if (image.Depth() is MatType.CV_32F or MatType.CV_64F)
            return image;
        Mat floatMat = new(image.Size(), MatType.CV_32F);
        if (image.Depth() == MatType.CV_8U)
            image.ConvertTo(floatMat, MatType.CV_32F, 1.0 / 255);
        else if (image.Depth() == MatType.CV_8S)
            image.ConvertTo(floatMat, MatType.CV_32F,
                1.0 / 255, 128.0 / 255);
        else if (image.Depth() == MatType.CV_16U)
            image.ConvertTo(floatMat, MatType.CV_32F, 1.0 / 65535);
        else if (image.Depth() == MatType.CV_16S)
            image.ConvertTo(floatMat, MatType.CV_32F,
                1.0 / 65535, 32768.0 / 65535);
        else if (image.Depth() == MatType.CV_32S)
            image.ConvertTo(floatMat, MatType.CV_32F,
                1.0 / 4294967295, 2147483648.0 / 4294967295);
        else throw new ArgumentException("不支持此图像深度");
        return floatMat;
    }

    internal static double MeanValue(this Mat image, byte? target)
        => image.Channels() switch
        {
            // 单通道直接计算
            1 => target < 7 ? image.Mean().Val0 : 0,
            // 3通道断言为BGR（视频始终如此），分类处理
            3 => image.MeanValue3(target),
            // 4通道断言为BGRA，混合Alpha后按BGR处理
            4 => MixAlpha(image).MeanValue3(target),
            _ => throw new ArgumentException("不支持此图像通道数")
        };

    private static Mat MixAlpha(Mat image)
        => image.ExtractChannel(3)
        * image.CvtColor(ColorConversionCodes.BGRA2BGR);

    private static double MeanValue3(this Mat image, byte? target)
        => target switch
        {
            0 => BGR2CIEL(image).Mean().Val0,
            1 => BGR2L(image).Mean().Val0,
            2 => BGR2MeanIValue(image),
            3 => BGR2V(image).Mean().Val0,
            4 => image.ExtractChannel(2).Mean().Val0,
            5 => image.ExtractChannel(1).Mean().Val0,
            6 => image.ExtractChannel(0).Mean().Val0,
            7 => BGR2Sl(image).Mean().Val0,
            8 => BGR2Sv(image).Mean().Val0,
            9 => BGR2H(image).Mean().Val0 / 360.0,
            _ => throw new ArgumentException("不支持此待测指标")
        };

    private static Mat BGR2CIEL(Mat image)
        => image.CvtColor(ColorConversionCodes.BGR2Lab).ExtractChannel(0);

    private static Mat BGR2L(Mat image)
        => image.CvtColor(ColorConversionCodes.BGR2HLS).ExtractChannel(1);

    private static double BGR2MeanIValue(Mat image)
    {
        var mean = image.Mean();
        return (mean.Val0 + mean.Val1 + mean.Val2) / 3.0;
    }

    private static Mat BGR2V(Mat image)
        => image.CvtColor(ColorConversionCodes.BGR2HSV).ExtractChannel(2);

    private static Mat BGR2Sl(Mat image)
        => image.CvtColor(ColorConversionCodes.BGR2HLS).ExtractChannel(2);

    private static Mat BGR2Sv(Mat image)
        => image.CvtColor(ColorConversionCodes.BGR2HSV).ExtractChannel(1);

    private static Mat BGR2H(Mat image)
        => image.CvtColor(ColorConversionCodes.BGR2HSV).ExtractChannel(0);
}
