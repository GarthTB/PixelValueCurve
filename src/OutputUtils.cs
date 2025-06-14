using ScottPlot;

namespace PixelValueCurve.src;

internal static class OutputUtils
{
    internal static void Output(
        OutputConfig outputConfig, double[] data, string outputPathRoot)
    {
        if (outputConfig.Data == true)
            OutputData(data, outputPathRoot);
        if (outputConfig.Plot == true)
            OutputPlot(data, outputPathRoot);
    }

    private static void OutputData(double[] data, string outputPathRoot)
    {
        var path = FileUtils.GetUniqueOutputPath(outputPathRoot, "csv");
        var text = Enumerable.Range(0, data.Length)
            .Select(i => $"{i}\t{data[i]}")
            .Aggregate((a, b) => $"{a}{Environment.NewLine}{b}");
        File.WriteAllText(path, text);
    }

    private static void OutputPlot(double[] data, string outputPathRoot)
    {
        using Plot plot = new() { ScaleFactor = 2 };
        plot.Axes.AntiAlias(true);
        plot.XLabel("Frame Index");
        plot.YLabel("Pixel Value");

        if (data.Length < 2)
            plot.Axes.SetLimitsX(0, 2);
        else plot.Axes.SetLimitsX(0, data.Length);

        var width = data.Length switch
        {
            <= 400 => 3200,
            >= 800 => 6400,
            _ => data.Length * 8,
        };

        var indexes = Enumerable.Range(0, data.Length).ToArray();
        _ = plot.Add.Scatter(indexes, data);

        var path1 = FileUtils.GetUniqueOutputPath($"{outputPathRoot}_zoom", "png");
        _ = plot.SavePng(path1, width, 1600);

        plot.Axes.SetLimitsY(0, 1);
        var path2 = FileUtils.GetUniqueOutputPath($"{outputPathRoot}_full", "png");
        _ = plot.SavePng(path2, width, 1600);
    }
}
