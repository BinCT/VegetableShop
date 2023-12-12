using Microsoft.AspNetCore.Hosting;

namespace VegetableShop.Data.Common
{
	public class StorageService : IStorageService
	{
		private readonly string _ContentName;
		private const string _Folder_Name = "user-content";
		public StorageService(IWebHostEnvironment webHostEnvironment)
		{
			_ContentName = Path.Combine(webHostEnvironment.WebRootPath, _Folder_Name);
		}
		public string GetFileUrl(string FileName)
		{
			return $"/{_Folder_Name}/{FileName}";
		}
		public async Task SaveFileAsync(Stream stream, string fileName)
		{
			var filePath = Path.Combine(_ContentName, fileName);
			using var output = new FileStream(filePath, FileMode.Create);
			await stream.CopyToAsync(output);
		}
		public async Task DeleteFileAsync(string fileName)
		{
			var filePath = Path.Combine(_ContentName, fileName);
			if (File.Exists(filePath))
			{
				await Task.Run(() => File.Delete(filePath));
			}
		}




	}
}
