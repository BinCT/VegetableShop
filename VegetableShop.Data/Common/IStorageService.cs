

namespace VegetableShop.Data.Common
{
	public interface IStorageService
	{
		string GetFileUrl(string FileName);
		Task SaveFileAsync(Stream stream, string fileName);
		Task DeleteFileAsync(string fileName);
	}
}
