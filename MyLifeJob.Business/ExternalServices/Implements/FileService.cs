using Microsoft.AspNetCore.Http;
using MyLifeJob.Business.Constants;
using MyLifeJob.Business.Exceptions.FileService;
using MyLifeJob.Business.Extensions;
using MyLifeJob.Business.ExternalServices.Interfaces;

namespace MyLifeJob.Business.ExternalServices.Implements;

public class FileService : IFileService
{
    public void Delete(string path)
    {
        if (String.IsNullOrEmpty(path) || String.IsNullOrWhiteSpace(path)) throw new ImagePathNullOrEmptyException();
        if (!path.StartsWith(RootContsant.Root))
            path = Path.Combine(RootContsant.Root, path);
        if (File.Exists(path))
            File.Delete(path);
    }

    public async Task SaveAsync(IFormFile file, string path)
    {
        using FileStream fs = new FileStream(Path.Combine(RootContsant.Root, path), FileMode.Create);
        await file.CopyToAsync(fs);
    }

    public async Task<string> UploadAsync(IFormFile file, string path, string contentType = "image", int mb = 3)
    {
        if (!file.IsSizeValid(mb)) throw new SizeNotValidException();
        if (!file.IsTypeValid(contentType)) throw new TypeNotValidException();

        string newFileName = _renameFile(file);
        _checkDirectory(path);
        path = Path.Combine(path, newFileName);
        await SaveAsync(file, path);
        return path;
    }

    private string _renameFile(IFormFile file)
        => Guid.NewGuid() + Path.GetExtension(file.FileName);

    private void _checkDirectory(string path)
    {
        if (!Directory.Exists(Path.Combine(RootContsant.Root, path)))
        {
            Directory.CreateDirectory(Path.Combine(RootContsant.Root, path));
        }
    }
}
