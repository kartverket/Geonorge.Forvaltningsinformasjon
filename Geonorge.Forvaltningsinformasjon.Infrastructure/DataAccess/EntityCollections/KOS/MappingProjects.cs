using Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.KOS;
using Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.Custom;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities.Enums;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.EntityCollections.KOS
{
    internal class MappingProjects : IMappingProjects
    {
        private KosContext _dbContext;

        public MappingProjects(KosContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<IMappingProject> Get()
        {
            return _dbContext.Set<DataAccess.Entities.KOS.MappingProject>()
                .Include(p => p.MappingProjectMunicipalityLinks).ThenInclude(mpm => mpm.Municipality)
                .Include(p => p.Office)
                .Include(p => p.Deliveries).ThenInclude(d => d.Type)
                .Include(p => p.ProjectActivities)
                .AsEnumerable()
                .Select(mp => Create(mp))
                .Where(p => p.State != MappingProjectState.None)
                .ToList();
        }

        public List<IMappingProject> Get(
            string municipalityNumber, 
            int officeId,
            MappingProjectState state,
            RelevantMappingProjectDeliveryType deliveryType,
            int year)
        {
            IEnumerable<IMappingProject> projects = _dbContext.Set<DataAccess.Entities.KOS.MappingProject>()
                .Include(p => p.MappingProjectMunicipalityLinks).ThenInclude(mpm => mpm.Municipality)
                .Include(p => p.Office)
                .Include(p => p.Deliveries).ThenInclude(d => d.Type)
                .Include(p => p.ProjectActivities)
                .AsEnumerable()
                .Select(mp => Create(mp))
                .Where(p => p.State != MappingProjectState.None);

            projects = projects.Where(d => d.Deliveries.Any());

            if (municipalityNumber != null)
            {
                projects = projects.Where(p => p.Municipalities.FirstOrDefault(m => m.Number == municipalityNumber) != default);
            }

            if (officeId != default)
            {
                projects = projects.Where(p => p.Office.Id == officeId);
            }

            if (state != default)
            {
                projects = projects.Where(p => p.State == state);
            }

            if (deliveryType != default)
            {
                projects = projects.Where(p => p.Deliveries.FirstOrDefault(d => d.Type == deliveryType) != default);
            }

            if (year != default)
            {
                projects = projects.Where(p => p.Year == year);
            }

            return projects.ToList();
        }

        public IMappingProject GetDetails(int id)
        {
            return _dbContext.Set<DataAccess.Entities.KOS.MappingProject>()
                .Where(p => p.Id == id)
                .Include(p => p.MappingProjectMunicipalityLinks).ThenInclude(mpm => mpm.Municipality)
                .Include(p => p.Office)
                .Include(p => p.Deliveries).ThenInclude(d => d.Type)
                .Include(p => p.ProjectActivities)
                .AsEnumerable()
                .Select(mp => Create(mp))
                .First();
        }

        private IMappingProject Create(DataAccess.Entities.KOS.MappingProject mappingProjectKos)
        {
            DataAccess.Entities.Custom.MappingProject mappingProject = new DataAccess.Entities.Custom.MappingProject
            {
                Id = mappingProjectKos.Id,
                Active = mappingProjectKos.Active,
                Name = mappingProjectKos.Name,
                Year = mappingProjectKos.Year,
                Office = mappingProjectKos.Office,
            };

            var numberOfProjectDeliveries = mappingProjectKos.Deliveries.Count;

            var numberOfProjectDeliveriesWithReleaseDate = mappingProjectKos.Deliveries.Where(r => !string.IsNullOrEmpty(r.ReleaseDate)).ToList().Count;

            // determine project status
            if (mappingProjectKos.ProjectActivities.
                FirstOrDefault(a => a.Activity == MappingProjectActivity.ActivityType.COMPLETED && !string.IsNullOrEmpty(a.Date)) != default)
            {
                mappingProject.State = MappingProjectState.Closed;
            }
            else if (numberOfProjectDeliveries > 0 && numberOfProjectDeliveries == numberOfProjectDeliveriesWithReleaseDate)
            {
                mappingProject.State = MappingProjectState.Delivered;
            }
            else if (mappingProjectKos.ProjectActivities.
                FirstOrDefault(a => a.Activity == MappingProjectActivity.ActivityType.STARTED && !string.IsNullOrEmpty(a.Date)) != default)
            {
                mappingProject.State = MappingProjectState.Ongoing;
            }
            else
            {
                mappingProject.State = MappingProjectState.None;
            }
            mappingProjectKos.MappingProjectMunicipalityLinks.GroupBy(mpm => mpm.Municipality).ToList().ForEach(mpm => mappingProject.Municipalities.Add(mpm.First().Municipality));

            // filter deliveries by type
            int[] types = { 
                1,  // orthophoto
                2,  // laser
                8   // FKB
            };

            foreach(DataAccess.Entities.KOS.MappingProjectDelivery deliveryKos in mappingProjectKos.Deliveries)
            {
                if (types.Contains(deliveryKos.TypeId))
                {
                    DataAccess.Entities.Custom.MappingProjectDelivery delivery = new DataAccess.Entities.Custom.MappingProjectDelivery
                    {
                        Name = deliveryKos.Name,
                        Deadline = deliveryKos.Deadline,
                        ChangedDeadline = deliveryKos.ChangedDeadline,
                        FinalDeadline = deliveryKos.FinalDeadline,
                        ReleaseDate = deliveryKos.ReleaseDate
                    };

                    switch (deliveryKos.TypeId)
                    {
                        case 1:
                            delivery.Type = RelevantMappingProjectDeliveryType.OrthoPhoto;
                            break;
                        case 2:
                            delivery.Type = RelevantMappingProjectDeliveryType.Laser;
                            break;
                        case 8:
                            delivery.Type = RelevantMappingProjectDeliveryType.Fkb;
                            break;
                        default:
                            break;
                    }
                    mappingProject.Deliveries.Add(delivery);
                }
            }

            return mappingProject;
        }
    }
}
