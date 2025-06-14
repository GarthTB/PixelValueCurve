using System.Diagnostics;

namespace PixelValueCurve.src;

internal static class Program
{
    private static void Main()
    {
        try
        {
            Console.WriteLine("欢迎使用像素值曲线工具！\n"
                + "版本：1.1.0 (2025-06-12)\n"
                + "作者：Garth TB <g-art-h@outlook.com>\n"
                + "仓库地址：https://github.com/GarthTB/PixelValueCurve");
            Stopwatch sw = new();
            sw.Start();
            Core.Run();
            sw.Stop();
            Console.WriteLine($"程序成功执行完毕！\n"
                + $"总用时：{sw.Elapsed.TotalSeconds:F6} s");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"程序出错：\n{ex.Message}\n已中断！");
            Console.ResetColor();
        }
    }
}
