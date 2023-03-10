namespace imgtest;

public interface ImageProcessor
{
    void GetExif(string processorName, string src, string dst);
    void ResizeImage(string src, string dst, int dstWidth, int dstHeight);
}
