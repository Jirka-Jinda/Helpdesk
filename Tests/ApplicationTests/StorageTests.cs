using Helpdesk.Models.Cache.ScopeCache;

namespace Tests.ApplicationTests;

public class StorageTests
{
    private IScopeStorage _scopeStorage;

    public StorageTests()
    {
        _scopeStorage = new ScopeCache(new());
    }

    [Fact]
    public void Store_and_retrieve_keyed_scoped_data()
    {
        // Arrange
        string[] data = { "data1", "data2", "data3" };
        string[] keys = { "key1", "key2", "key3" };
        
        // Act
        for (int i = 0; i < data.Length; i++)
            Assert.True(_scopeStorage.PutKeyed(keys[i], data[i]));

        // Assert
        for (int i = 0; i < data.Length; i++)
        {
            Assert.True(_scopeStorage.GetKeyed(keys[i], out string? result));
            Assert.Equal(data[i], result);
        }
    }

    public void 
}
