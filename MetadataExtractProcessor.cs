using MetadataExtractor;

namespace imgtest;

public class MetadataExtractProcessor
    : ImageProcessor
{
    public void GetExif(string processorName, string src, string dst)
    {
        using var sw = new StreamWriter(new FileStream(dst, FileMode.OpenOrCreate));
        var directories = ImageMetadataReader.ReadMetadata(src);

        foreach (var directory in directories)
        foreach (var tag in directory.Tags)
            sw.WriteLine($"{directory.Name} - {tag.Name} = {tag.Description}");
    }

    public void ResizeImage(string src, string dst, int dstWidth, int dstHeight)
    {
        throw new NotSupportedException();
    }
}
