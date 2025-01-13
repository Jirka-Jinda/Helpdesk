namespace Helpdesk.Models.Storage.Manager;

public class StorageOptions
{
    public MemoryCacheOptions SessionMemoryCacheOptions { get; set; } = new();

    public MemoryCacheEntryOptions SessionMemoryCacheEntryOptions { get; set; } = new();

    public MemoryCacheOptions ScopeMemoryCacheOptions { get; set; } = new();

    public MemoryCacheEntryOptions ScopeMemoryCacheEntryOptions { get; set; } = new();
}
