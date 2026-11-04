using SupercarsTiming.Server;

namespace SupercarsTiming.Natsoft.Tests;

public sealed class StandaloneHostTests
{
    [Fact]
    public void A_trailing_separator_does_not_start_another_instance_for_the_same_data_folder()
    {
        var path = Path.Combine(Path.GetTempPath(), $"standalone-{Guid.NewGuid():N}");
        using var first = StandaloneHost.SingleInstance(path, out var firstCreated);
        using var alias = StandaloneHost.SingleInstance(path + Path.DirectorySeparatorChar, out var aliasCreated);
        try
        {
            Assert.True(firstCreated);
            Assert.False(aliasCreated);
        }
        finally
        {
            if (aliasCreated) alias.ReleaseMutex();
            if (firstCreated) first.ReleaseMutex();
        }
    }
}
