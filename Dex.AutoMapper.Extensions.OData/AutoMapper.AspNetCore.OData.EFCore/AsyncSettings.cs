using System.Threading;

namespace AutoMapper.AspNet.OData;

public class AsyncSettings
{
    /// <summary>
    /// <summary>
    /// Gets or sets a cancellation token for async requests.
    /// </summary>
    public CancellationToken CancellationToken { get; set; }
}