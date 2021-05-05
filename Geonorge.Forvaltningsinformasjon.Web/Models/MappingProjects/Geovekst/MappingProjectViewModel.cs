using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities.Enums;
using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Web.Models.MappingProjects.Geovekst
{
    public class MappingProjectViewModel
    {
        public List<ProjectListItem> MappingProjects { get; } = new List<ProjectListItem>();

        public MappingProjectViewModel(List<IMappingProject> mappingProjects)
        {
            foreach (IMappingProject project in mappingProjects)
            {
                MappingProjects.Add(new ProjectListItem(project));
            }
        }
    }

    public struct ProjectListItem
    {
        public string Name { get; }
        public string OfficeName { get; }
        public int Year { get; }
        public string MunicipalityNames { get; }
        public string MunicipalityToolTip { get; }
        public string DeliveryTypes { get; }

        public ProjectListItem(IMappingProject project)
        {
            Name = project.Name;
            OfficeName = project.OfficeName;
            Year = project.Year;

            // municipalities
            List<string> municipalityNames = new List<string>();

            project.Municipalities.ForEach(m => municipalityNames.Add(m.Name));

            if (municipalityNames.Count > 3)
            {
                MunicipalityToolTip = string.Join(", \n", municipalityNames);

                MunicipalityNames = string.Join(", <br>", municipalityNames.GetRange(0, 3));
                MunicipalityNames += " ...";
            }
            else
            {
                MunicipalityToolTip = "";
                MunicipalityNames = string.Join(", <br>", municipalityNames);
            }

            // delivery types
            List<string> deliveryTypes = new List<string>();

            foreach(MappingProjectDeliveryType deliveryType in project.DeliveryTypes)
            {
                switch (deliveryType)
                {
                    case MappingProjectDeliveryType.OrthoPhoto:
                        deliveryTypes.Add("Ortofoto");
                        break;
                    case MappingProjectDeliveryType.Laser:
                        deliveryTypes.Add("Laser");
                        break;
                    case MappingProjectDeliveryType.Fkb:
                        deliveryTypes.Add("FKB");
                        break;
                    default:
                        break;
                }
            }

            deliveryTypes.Sort();

            DeliveryTypes = string.Join(", ", deliveryTypes);
        }
    }
}
