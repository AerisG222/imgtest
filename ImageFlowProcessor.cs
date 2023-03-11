using Imageflow.Fluent;

namespace imgtest;

public class ImageFlowProcessor
    : ImageProcessor
{
    public void GetExif(string processorName, string src, string dst)
    {
        throw new NotImplementedException();
    }

    public void ResizeImage(string src, string dst, int dstWidth, int dstHeight)
    {
        // below isn't quite working, and don't love the api right now, so putting this on hold
        throw new NotImplementedException();

        using var job = new ImageJob();

        var process = Task.Run(() =>
            job
                .Decode(new FileStream(src, FileMode.Open), true)
                .ResizerCommands($"width={dstWidth}&height={dstHeight}")
                .EncodeToBytes(new MozJpegEncoder(75, true))
                .Finish()
                .InProcessAsync()
        ).Result;

        using var fsout = new StreamWriter(new FileStream(dst, FileMode.OpenOrCreate));

        fsout.Write(process.First.TryGetBytes()!.Value);
        fsout.Flush();
    }
}
