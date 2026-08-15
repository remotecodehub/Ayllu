namespace Ayllu.Application.Common.Abstractions.Storage;

public interface IFilesStorage
{
    Task<string> SaveAsync(
        Stream audioStream,
        string fileName,
        CancellationToken ct);

    Task<Stream> GetAsync(string path, CancellationToken ct);

    Task DeleteAsync(string path, CancellationToken ct);
}