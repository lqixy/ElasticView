using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticView.AppService.Contracts.Output
{
    public partial class QueryIndiceStatsDto
    {
        public Shards Shards { get; set; }
        public All All { get; set; }
        public Indices Indices { get; set; }
    }

    public partial class All
    {
        public Primaries Primaries { get; set; }
        public Primaries Total { get; set; }
    }

    public partial class Primaries
    {
        public Docs Docs { get; set; }
        public Store Store { get; set; }
        public Indexing Indexing { get; set; }
        public Get Get { get; set; }
        public Dictionary<string, long> Search { get; set; }
        public Merges Merges { get; set; }
        public Refresh Refresh { get; set; }
        public Flush Flush { get; set; }
        public Warmer Warmer { get; set; }
        public QueryCache QueryCache { get; set; }
        public Fielddata Fielddata { get; set; }
        public Completion Completion { get; set; }
        public Segments Segments { get; set; }
        public Translog Translog { get; set; }
        public RequestCache RequestCache { get; set; }
        public Recovery Recovery { get; set; }
    }

    public partial class Completion
    {
        public long SizeInBytes { get; set; }
    }

    public partial class Docs
    {
        public long Count { get; set; }
        public long Deleted { get; set; }
    }

    public partial class Fielddata
    {
        public long MemorySizeInBytes { get; set; }
        public long Evictions { get; set; }
    }

    public partial class Flush
    {
        public long Total { get; set; }
        public long TotalTimeInMillis { get; set; }
    }

    public partial class Get
    {
        public long Total { get; set; }
        public long TimeInMillis { get; set; }
        public long ExistsTotal { get; set; }
        public long ExistsTimeInMillis { get; set; }
        public long MissingTotal { get; set; }
        public long MissingTimeInMillis { get; set; }
        public long Current { get; set; }
    }

    public partial class Indexing
    {
        public long IndexTotal { get; set; }
        public long IndexTimeInMillis { get; set; }
        public long IndexCurrent { get; set; }
        public long IndexFailed { get; set; }
        public long DeleteTotal { get; set; }
        public long DeleteTimeInMillis { get; set; }
        public long DeleteCurrent { get; set; }
        public long NoopUpdateTotal { get; set; }
        public bool IsThrottled { get; set; }
        public long ThrottleTimeInMillis { get; set; }
    }

    public partial class Merges
    {
        public long Current { get; set; }
        public long CurrentDocs { get; set; }
        public long CurrentSizeInBytes { get; set; }
        public long Total { get; set; }
        public long TotalTimeInMillis { get; set; }
        public long TotalDocs { get; set; }
        public long TotalSizeInBytes { get; set; }
        public long TotalStoppedTimeInMillis { get; set; }
        public long TotalThrottledTimeInMillis { get; set; }
        public long TotalAutoThrottleInBytes { get; set; }
    }

    public partial class QueryCache
    {
        public long MemorySizeInBytes { get; set; }
        public long TotalCount { get; set; }
        public long HitCount { get; set; }
        public long MissCount { get; set; }
        public long CacheSize { get; set; }
        public long CacheCount { get; set; }
        public long Evictions { get; set; }
    }

    public partial class Recovery
    {
        public long CurrentAsSource { get; set; }
        public long CurrentAsTarget { get; set; }
        public long ThrottleTimeInMillis { get; set; }
    }

    public partial class Refresh
    {
        public long Total { get; set; }
        public long TotalTimeInMillis { get; set; }
        public long Listeners { get; set; }
    }

    public partial class RequestCache
    {
        public long MemorySizeInBytes { get; set; }
        public long Evictions { get; set; }
        public long HitCount { get; set; }
        public long MissCount { get; set; }
    }

    public partial class Segments
    {
        public long Count { get; set; }
        public long MemoryInBytes { get; set; }
        public long TermsMemoryInBytes { get; set; }
        public long StoredFieldsMemoryInBytes { get; set; }
        public long TermVectorsMemoryInBytes { get; set; }
        public long NormsMemoryInBytes { get; set; }
        public long PointsMemoryInBytes { get; set; }
        public long DocValuesMemoryInBytes { get; set; }
        public long IndexWriterMemoryInBytes { get; set; }
        public long VersionMapMemoryInBytes { get; set; }
        public long FixedBitSetMemoryInBytes { get; set; }
        public long MaxUnsafeAutoIdTimestamp { get; set; }
        public FileSizes FileSizes { get; set; }
    }

    public partial class FileSizes
    {
    }

    public partial class Store
    {
        public long SizeInBytes { get; set; }
        public long ThrottleTimeInMillis { get; set; }
    }

    public partial class Translog
    {
        public long Operations { get; set; }
        public long SizeInBytes { get; set; }
    }

    public partial class Warmer
    {
        public long Current { get; set; }
        public long Total { get; set; }
        public long TotalTimeInMillis { get; set; }
    }

    public partial class Indices
    {
        public All Articles { get; set; }
    }

    public partial class Shards
    {
        public long Total { get; set; }
        public long Successful { get; set; }
        public long Failed { get; set; }
    }
}
