using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace imgtest;

public class ImageSharpProcessor
    : ImageProcessor
{
    public void GetExif(string processorName, string src, string dst)
    {
        using var sw = new StreamWriter(new FileStream(dst, FileMode.OpenOrCreate));
        using var image = Image.Load(src);

        if(image.Metadata.ExifProfile != null)
        {
            foreach(var val in image.Metadata.ExifProfile.Values)
            {
                sw.WriteLine($"{val.DataType} :: {val.Tag} :: {val.IsArray} :: {val.GetValue()}");
            }
        }
        else
        {
            Console.WriteLine($"{processorName}: cannot read exif data from {src}");
        }
    }

    public void ResizeImage(string src, string dst, int dstWidth, int dstHeight)
    {
        using var image = Image.Load(src);

        image.Mutate(c => c.Resize(dstWidth, dstHeight));

        image.SaveAsJpeg(dst);
    }
}
