using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace imgtest;

public class ImageSharpProcessor
    : ImageProcessor
{
    public void ResizeImage(string src, string dst, int dstWidth, int dstHeight)
    {
        using var image = Image.Load(src);

        image.Mutate(c => c.Resize(dstWidth, dstHeight));

        image.SaveAsJpeg(dst);
    }
}
