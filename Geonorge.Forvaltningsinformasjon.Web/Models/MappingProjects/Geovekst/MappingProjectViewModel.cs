using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Geonorge.Forvaltningsinformasjon.Web.Models.MappingProjects.Geovekst
{
    public class MappingProjectViewModel
    {
        public List<ProjectListItem> MappingProjects { get; } = new List<ProjectListItem>();

        public List<IMunicipality> Municipalities { get; }
        public List<IOffice> Offices;
        public List<KeyValuePair<MappingProjectState, string>> States { get; } = new List<KeyValuePair<MappingProjectState, string>>();
        public List<int> Years { get; } = new List<int>();

        public string SelectedMunicipality { get; }
        public int SelectedOffice { get; }
        public MappingProjectState SelectedState { get; }
        public int SelectedYear { get; }

        public MappingProjectViewModel(
            List<IMappingProject> mappingProjects, 
            List<IMunicipality> municipalities,
            List<IOffice> offices,
            string selectedMunicipality,
            int selectedOffice,
            MappingProjectState selectedState,
            int selectedYear)
        {
            foreach (IMappingProject project in mappingProjects)
            {
                MappingProjects.Add(new ProjectListItem(project));
            }

            Municipalities = municipalities;
            Municipalities.Sort((l, r) => l.Name.CompareTo(r.Name));
            SelectedMunicipality = selectedMunicipality;

            Offices = offices;
            Offices.Sort((l, r) => l.Name.CompareTo(r.Name));
            SelectedOffice = selectedOffice;

            foreach(var state in Enum.GetValues(typeof(MappingProjectState)).Cast<MappingProjectState>())
            {
                States.Add(new KeyValuePair<MappingProjectState, string>(state, ProjectListItem.GetStateName(state, false)));
            }
            SelectedState = selectedState;

            const int startYear = 2010;

            Years = Enumerable.Range(startYear, DateTime.Now.Year - startYear + 1).Reverse().ToList();
            SelectedYear = selectedYear;
        }
    }

    public struct ProjectListItem
    {
        public static string GetStateName(MappingProjectState state, bool defaultIsEmpty = true)
        {
            string name = null;

            switch (state)
            {
                case MappingProjectState.Ongoing:
                    name = "Pågående";
                    break;
                case MappingProjectState.Closed:
                    name = "Avsluttet";
                    break;
                default:
                    name = defaultIsEmpty ? "" : "Alle";
                    break;
            }
            return name;
        }

        public int Id { get; }
        public string Name { get; }
        public string OfficeName { get; }
        public int Year { get; }
        public string MunicipalityNames { get; }
        public string MunicipalityToolTip { get; }
        public string DeliveryTypes { get; }
        public string State { get; }

        public List<IMappingProjectDelivery> Deliveries { get; }

        public ProjectListItem(IMappingProject project, bool includeDetails = false)
        {
            Id = project.Id;
            Name = project.Name;
            OfficeName = project.Office.Name;
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
            Deliveries = new List<IMappingProjectDelivery>();

            foreach (IMappingProjectDelivery delivery in project.Deliveries)
            {
                if (includeDetails)
                {
                    Deliveries.Add(delivery);
                }
                else if (!deliveryTypes.Contains(delivery.TypeName))
                {
                    deliveryTypes.Add(delivery.TypeName);
                }
            }

            deliveryTypes.Sort();

            DeliveryTypes = string.Join(", ", deliveryTypes);

            // state
            State = GetStateName(project.State);
        }
    }
}
