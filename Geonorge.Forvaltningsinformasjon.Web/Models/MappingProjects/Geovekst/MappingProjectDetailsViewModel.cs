using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geonorge.Forvaltningsinformasjon.Web.Models.MappingProjects.Geovekst
{
    public class MappingProjectDetailsViewModel
    {
        public ProjectListItem Project { get; }
        public MappingProjectDetailsViewModel(IMappingProject project)
        {
            Project = new ProjectListItem(project, true);
        }
    }
}
