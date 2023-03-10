using ImageMagick;

namespace imgtest;

public class MagickNetProcessor
    : ImageProcessor
{
    public void ResizeImage(string src, string dst, int dstWidth, int dstHeight)
    {
        using var image = new MagickImage(src);

        image.Resize(dstWidth, dstHeight);

        image.Write(dst);
    }
}
