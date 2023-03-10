using System;
using System.Collections.Generic;
using System.IO;
using imgtest;

var _srcImageDir = $"{Environment.GetEnvironmentVariable("HOME")}/image_processor_tests";

var _processors = new List<(string name, string outDir, ImageProcessor processor)>()
{
    ("ImageSharp", Path.Combine(_srcImageDir, "ImageSharp"), new ImageSharpProcessor()),
    ("MagickNet",  Path.Combine(_srcImageDir, "MagickNet"),  new MagickNetProcessor())
};

foreach(var (_, dir, _) in _processors)
{
    Directory.CreateDirectory(dir);
}

foreach(var image in Directory.EnumerateFiles(_srcImageDir))
{
    foreach(var (name, dir, processor) in _processors)
    {
        processor.ResizeImage(image, Path.Combine(dir, Path.GetFileName(image)), 1200, 796);
    }
}
