# 📸 Pixel Value Curve 像素值曲线工具 📈

[![README English](https://img.shields.io/badge/README-English-blue)](https://github.com/GarthTB/PixelValueCurve/blob/master/README_en.md)
[![用前必读 中文](https://img.shields.io/badge/用前必读-中文-red)](https://github.com/GarthTB/PixelValueCurve/blob/master/README.md)
[![Framework .NET 9.0](https://img.shields.io/badge/Framework-.NET%209.0-indigo)](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)  
[![Latest Release 1.1.0](https://img.shields.io/badge/Latest%20Release-1.1.0-brightgreen)](https://github.com/GarthTB/PixelValueCurve/releases/latest)  
[![License Apache 2.0](https://img.shields.io/badge/License-Apache%202.0-royalblue)](https://www.apache.org/licenses/LICENSE-2.0)  

## Overview

Pixel Value Curve is a high-performance C# console application
designed for image and video frame analysis.
It measures pixel values in specified ROI within images or video frames,
supporting multiple color space metrics,
and exports results as CSV files or visual charts in the **same directory**.

**Core Function**: Extract pixel data from image sequences/videos →
Convert to specified color channels →
Calculate average values →
Generate data reports and visual charts

## Features

- ⚡ **High-Speed Processing**: Built on OpenCV with multi-core CPU optimization for efficient handling of large images/videos
- 🎯 **Precise Measurement**: Ensuring processing precision with 32-bit floating-point numbers per-pixel per-channel
- 📁 **Broad Format Support**:
    - **Images**: JPEG, PNG, TIFF, BMP, WebP, etc.
    - **Videos**: MP4, AVI, MOV, MKV, and other mainstream formats
- 🤖 **Automation-Friendly**: Operates via TOML configuration files without additional interaction
- 📊 **Dual Output Modes**: Simultaneously generates CSV data and PNG charts
- 🌈 **Multi-Color Space Support**: 10 selectable target metrics

| **Color Space** |        **Supported Metrics**       |
|:---------------:|:----------------------------------:|
|    L\*a\*b\*    | L (Lightness)                      |
|      HSL        | L (Lightness), S (Saturation)      |
|      HSV        | I (Intensity)                      |
|      HSI        | H (Hue), S (Saturation), V (Value) |
|      RGB        | R, G, B (Three Channels)           |

## Tech Stack

- **Runtime**: .NET 9.0
- **External Libraries**:
    - [OpenCvSharp4](https://github.com/shimat/opencvsharp) - Image/Video processing
    - [ScottPlot](https://scottplot.net/) - Data visualization
    - [Tomlyn](https://github.com/xoofx/Tomlyn) - TOML configuration parsing

## Quick Start

### System Requirements

- Windows
- [.NET 9.0 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)

### Usage Steps

1. Download the release package from the [Releases page](https://github.com/GarthTB/PixelValueCurve/releases/latest)
2. Fully extract the release package
3. Modify `config.toml` in the same directory to configure the program
4. Run the program:
    - Directly execute `PixelValueCurve.exe` (exits upon completion)
    - Command line: `.\PixelValueCurve.exe`

## Important Notes

- All image/video files in the target folder will be analyzed. Images are analyzed once; videos are analyzed once per file.
- The target folder must contain only media files (no CSV/PNG outputs or unrelated files).
- Due to OpenCV limitations, certain format/channel/bit-depth combinations may cause unexpected termination during color space conversion.

## About

- **Author**: Garth TB <g-art-h@outlook.com>
- **License**: [Apache License 2.0](https://www.apache.org/licenses/LICENSE-2.0) - Permits free use, modification, and distribution with preserved copyright/license notices
- **Issues**: Submit via [Project Repository](https://github.com/GarthTB/PixelValueCurve)

## Changelog

### v1.1.0 (2025-06-12)

- Fixed: Support for multiple image bit depths

### v1.0.0 (2025-06-12)

- Initial release
