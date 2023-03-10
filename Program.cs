using imgtest;

var _srcImageDir = $"{Environment.GetEnvironmentVariable("HOME")}/image_processor_tests";

var _processors = new List<(string name, string outDir, ImageProcessor processor)>()
{
    ("ImageSharp", Path.Combine(_srcImageDir, "ImageSharp"), new ImageSharpProcessor()),
    ("MagickNet",  Path.Combine(_srcImageDir, "MagickNet"),  new MagickNetProcessor()),
    ("MetadataExtractor", Path.Combine(_srcImageDir, "MetadataExtractor"), new MetadataExtractProcessor())
};

foreach(var (_, dir, _) in _processors)
{
    Directory.CreateDirectory(dir);
}

var _all = Directory.EnumerateFiles(_srcImageDir);
var _raws = Directory.EnumerateFiles(_srcImageDir).Where(x => x.EndsWith("nef", StringComparison.OrdinalIgnoreCase));
var _jpgs = Directory.EnumerateFiles(_srcImageDir).Where(x => x.EndsWith("jpg", StringComparison.OrdinalIgnoreCase));

foreach(var img in _all)
{
    foreach(var (name, dir, processor) in _processors)
    {
        RunTest(name, img, "resize", () => {
            processor.ResizeImage(img, $"{Path.Combine(dir, Path.GetFileName(img))}.jpg", 1200, 796);
        });

        RunTest(name, img, "exif", () => {
            processor.GetExif(name, img, $"{Path.Combine(dir, Path.GetFileName(img))}.exif.txt");
        });
    }
}

void RunTest(string processorName, string file, string testName, Action action)
{
    try
    {
        action();
    }
    catch(Exception ex)
    {
        Console.WriteLine($"{processorName} : {Path.GetFileName(file)} : {testName} : {ex.Message}");
    }
}
