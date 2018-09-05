using Geonorge.Forvaltningsinformasjon.Core.Models;

namespace Geonorge.Forvaltningsinformasjon.Core.Services
{
    public interface ISentralFkbService
    {
        SentralFkbSummary GetCountrySummary();
    }
}