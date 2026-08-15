namespace Ayllu.Application.Common.Abstractions.File;

public interface IFileUpload
{
    Stream OpenRead();
    string FileName { get; }
    string ContentType { get; }
    long Length { get; }
}
