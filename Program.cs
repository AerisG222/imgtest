using imgtest;

var _srcImageDir = $"{Environment.GetEnvironmentVariable("HOME")}/image_processor_tests";

var _processors = new List<(string name, string outDir, ImageProcessor processor)>()
{
    ("ImageFlow", Path.Combine(_srcImageDir, "ImageFlow"), new ImageFlowProcessor()),
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
var _nonraws = Directory.EnumerateFiles(_srcImageDir).Where(x => !x.EndsWith("nef", StringComparison.OrdinalIgnoreCase));

foreach(var img in _nonraws)
{
    foreach(var (name, dir, processor) in _processors)
    {
        RunTest(name, img, "resize", () => {
            processor.ResizeImage(img, $"{Path.Combine(dir, Path.GetFileName(img))}._1200x769jpg", 1200, 796);
        });

        RunTest(name, img, "resize", () => {
            processor.ResizeImage(img, $"{Path.Combine(dir, Path.GetFileName(img))}_160x120.jpg", 160, 120);
        });
    }
}

foreach(var img in _all)
{
    foreach(var (name, dir, processor) in _processors)
    {
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
