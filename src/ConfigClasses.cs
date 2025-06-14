namespace PixelValueCurve.src;

internal class MainConfig
{
    public InputConfig? InputConfig { get; set; }
    public OutputConfig? OutputConfig { get; set; }
    public ImageConfig? ImageConfig { get; set; }
    public VideoConfig? VideoConfig { get; set; }
    public RoiConfig? RoiConfig { get; set; }
}

internal class InputConfig
{
    /// <summary>
    /// The directory of the input file or directory.
    /// </summary>
    public string? Dir { get; set; }

    /// <summary>
    /// The type of the input file.
    /// </summary>
    public bool? IsImage { get; set; }

    /// <summary>
    /// The index of the target color channel.
    /// 0 - L (L*a*b*)
    /// 1 - L (HSL)
    /// 2 - I (HSI)
    /// 3 - V (HSV)
    /// 4 - R
    /// 5 - G
    /// 6 - B
    /// 7 - S (HSL)
    /// 8 - S (HSV)
    /// 9 - H (HSV)
    /// </summary>
    public byte? Target { get; set; }
}

internal class OutputConfig
{
    /// <summary>
    /// Whether to output the raw data of the measurement result.
    /// </summary>
    public bool? Data { get; set; }

    /// <summary>
    /// Whether to output the plot of the measurement result.
    /// </summary>
    public bool? Plot { get; set; }
}

internal class ImageConfig
{
    /// <summary>
    /// The sorting method of the input image.
    /// 0 - File name
    /// 1 - Creation time
    /// 2 - Modification time
    /// 3 - File size
    /// </summary>
    public byte? SortBy { get; set; }

    /// <summary>
    /// Whether to sort the input image in descending order.
    /// </summary>
    public bool? Descending { get; set; }
}

internal class VideoConfig
{
    /// <summary>
    /// The starting frame number of the video segment.
    /// if null, from the beginning of the video.
    /// </summary>
    public uint? StartFrame { get; set; }

    /// <summary>
    /// The number of frames to analyze in the video segment.
    /// if null, to the end of the video.
    /// </summary>
    public uint? FrameCount { get; set; }
}

internal class RoiConfig
{
    /// <summary>
    /// The left-top x coordinate of the ROI.
    /// if null, analyze the whole frame.
    /// </summary>
    public uint? TopLeftX { get; set; }

    /// <summary>
    /// The left-top y coordinate of the ROI.
    /// if null, analyze the whole frame.
    /// </summary>
    public uint? TopLeftY { get; set; }

    /// <summary>
    /// The width of the ROI.
    /// if null, analyze the whole frame.
    /// </summary>
    public uint? Width { get; set; }

    /// <summary>
    /// The height of the ROI.
    /// if null, analyze the whole frame.
    /// </summary>
    public uint? Height { get; set; }
}
