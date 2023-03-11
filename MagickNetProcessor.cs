using System;
using ImageMagick;
using ImageMagick.Formats;

namespace imgtest;

public class MagickNetProcessor
    : ImageProcessor
{
    public void GetExif(string processorName, string src, string dst)
    {
        using var sw = new StreamWriter(new FileStream(dst, FileMode.OpenOrCreate));
        using var image = new MagickImage(src);

        var exif = image.GetExifProfile();

        if(exif != null)
        {
            foreach(var val in exif.Values)
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
        using var image = new MagickImage(src);

        //image.ColorSpace = ColorSpace.Lab;
        //image.WhiteBalance();
        //image.ColorSpace = ColorSpace.sRGB;

        image.AutoOrient();
        //image.AutoGamma();

        image.Interlace = Interlace.Jpeg;
        image.Resize(dstWidth, dstHeight);
        image.UnsharpMask(0, 0.6);
        image.Strip();

        //image.ColorSpace = ColorSpace.sRGB;

        image.Format = MagickFormat.Jpg;
        image.Quality = 75;
        image.Write(dst);
    }
}
