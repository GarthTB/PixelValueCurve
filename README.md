# 📸 Pixel Value Curve 像素值曲线工具 📈

[![README English](https://img.shields.io/badge/README-English-blue)](https://github.com/GarthTB/PixelValueCurve/blob/master/README_en.md)
[![用前必读 中文](https://img.shields.io/badge/用前必读-中文-red)](https://github.com/GarthTB/PixelValueCurve/blob/master/README.md)
[![开发框架 .NET 9.0](https://img.shields.io/badge/开发框架-.NET%209.0-indigo)](https://dotnet.microsoft.com/zh-cn/download/dotnet/9.0)
[![最新版本 1.1.0](https://img.shields.io/badge/最新版本-1.1.0-brightgreen)](https://github.com/GarthTB/PixelValueCurve/releases/latest)
[![开源协议 Apache 2.0](https://img.shields.io/badge/开源许可-Apache%202.0-royalblue)](https://www.apache.org/licenses/LICENSE-2.0)

## 概述

Pixel Value Curve 是一个高性能的 C# 控制台应用程序，
专为图像和视频帧分析而设计。
它能自动测量图像或视频帧中指定区域（ROI）的像素值，
支持多种色彩空间指标，
并将结果导出为**同目录下**的 CSV 文件或可视化图表。

**核心功能**：从图像序列/视频中提取像素数据 →
转换为指定色彩通道 →
计算平均值 →
生成数据报告和可视化图表

## 特点

- ⚡ **极速处理**：基于 OpenCV 开发，充分利用多核 CPU，高效处理大型图像/视频
- 🎯 **精确测量**：使用每像素每通道32位浮点数保证处理精度
- 📁 **广泛格式支持**：
    - **图像**：JPEG、PNG、TIFF、BMP、WebP 等
    - **视频**：MP4、AVI、MOV、MKV 等主流格式
- 🤖 **自动化友好**：通过 TOML 配置文件操作，无其他交互
- 📊 **双输出模式**：同时生成 CSV 数据和 PNG 图表
- 🌈 **多色彩空间支持**：可选 10 种目标指标

| **色彩空间** |         **支持的指标**         |
|:------------:|:------------------------------:|
|  L\*a\*b\*   | L (明度)                       |
|     HSL      | L (亮度), S (饱和度)           |
|     HSV      | I (强度)                       |
|     HSI      | H (色相), S (饱和度), V (明度) |
|     RGB      | R, G, B (三通道)               |

## 技术栈

- **运行环境**：.NET 9.0
- **外部库**：
    - [OpenCvSharp4](https://github.com/shimat/opencvsharp) - 图像和视频处理
    - [ScottPlot](https://scottplot.net/) - 数据可视化
    - [Tomlyn](https://github.com/xoofx/Tomlyn) - TOML 配置解析

## 快速开始

### 系统要求

- Windows系统
- [.NET 9.0 运行时](https://dotnet.microsoft.com/zh-cn/download/dotnet/9.0)

### 使用步骤

1. 从 [Release页面](https://github.com/GarthTB/PixelValueCurve/releases/latest) 下载发行包
2. 完整解压发行包
3. 调整程序同目录中的 `config.toml` 以控制程序运行
4. 运行程序：
    - 直接运行 `PixelValueCurve.exe`（结束后立即退出）
    - 命令行运行：`.\PixelValueCurve.exe`

## 注意事项

- 待测文件夹内的所有图片/视频文件都会被分析；若是图片，则只分析一次；若是视频，则每个视频文件分析一次
- 待测文件夹内除待测文件外不能包含其他文件（包括输出的csv文件和png折线图）
- 由于 OpenCV 底层限制，有多种格式、通道数、位深的组合不支持色彩空间转换，从而导致程序直接中断

## 关于

- **作者**：Garth TB <g-art-h@outlook.com>
- **开源协议**：[Apache License 2.0](https://www.apache.org/licenses/LICENSE-2.0) - 允许自由使用、修改和分发，保留版权声明和许可声明
- **问题反馈**：欢迎在 [项目地址](https://github.com/GarthTB/PixelValueCurve) 提交 Issue

## 更新日志

### v1.1.0 (2025-06-12)

- 修复：多种图片位深的支持

### v1.0.0 (2025-06-12)

- 初始版本
