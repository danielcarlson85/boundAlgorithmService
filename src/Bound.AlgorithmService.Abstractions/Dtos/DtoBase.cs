using System;

namespace AlgorithmService.Abstractions.Dtos
{
    /// <summary>
    /// Base class for all dto objects.
    /// </summary>
    public abstract class DtoBase
    {
        public int? Id { get; set; }
    }
}
