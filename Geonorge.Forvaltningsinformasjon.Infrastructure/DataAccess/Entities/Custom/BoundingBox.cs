using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.Custom
{
    internal class BoundingBox : IBoundingBox
    {
        public int MinX { get; set; }
        public int MinY { get; set; }
        public int MaxX { get; set; }
        public int MaxY { get; set; }
        [NotMapped]
        public virtual string CoordinateSystem { get; set; }
    }
}
