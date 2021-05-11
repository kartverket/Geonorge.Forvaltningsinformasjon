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
            List<IMappingProject> projects = _dbContext.Set<DataAccess.Entities.KOS.MappingProject>()
                .Include(p => p.MappingProjectMunicipalityLinks).ThenInclude(mpm => mpm.Municipality)
                .Include(p => p.Office)
                .Include(p => p.Deliveries)
                .Include(p => p.ProjectActivities)
                .AsEnumerable()
                .Select(mp => Create(mp))
                .Where(p => p.State != MappingProjectState.None)
                .ToList();

            return projects;
        }

        private IMappingProject Create(DataAccess.Entities.KOS.MappingProject mappingProjectKos)
        {
            DataAccess.Entities.Custom.MappingProject mappingProject = new DataAccess.Entities.Custom.MappingProject
            {
                Id = mappingProjectKos.Id,
                Active = mappingProjectKos.Active,
                Name = mappingProjectKos.Name,
                Year = mappingProjectKos.Year,
                OfficeName = mappingProjectKos.Office.Name,
            };

            // determine project status
            if (mappingProjectKos.ProjectActivities.
                FirstOrDefault(a => a.Activity == MappingProjectActivity.ActivityType.COMPLETED && a.Date != null) != default)
            {
                mappingProject.State = MappingProjectState.Closed;
            }
            else if (mappingProjectKos.ProjectActivities.
                FirstOrDefault(a => a.Activity == MappingProjectActivity.ActivityType.STARTED && a.Date != null) != default)
            {
                mappingProject.State = MappingProjectState.Ongoing;
            }
            else
            {
                mappingProject.State = MappingProjectState.None;
            }
            // @TODO: remove filtering for duplicates if they're fixed in the db
            mappingProjectKos.MappingProjectMunicipalityLinks.GroupBy(mpm => mpm.Municipality).ToList().ForEach(mpm => mappingProject.Municipalities.Add(mpm.First().Municipality));

            foreach(MappingProjectDelivery delivery in mappingProjectKos.Deliveries)
            {
                MappingProjectDeliveryType type;

                switch (delivery.TypeId)
                {
                    case 1:
                        type = MappingProjectDeliveryType.OrthoPhoto;
                        break;
                    case 2:
                        type = MappingProjectDeliveryType.Laser;
                        break;
                    case 8:
                        type = MappingProjectDeliveryType.Fkb;
                        break;
                    default:
                        type = MappingProjectDeliveryType.Irrelevant;
                        break;
                }

                if (mappingProject.DeliveryTypes.Find(t => t == type) == default)
                {
                    mappingProject.DeliveryTypes.Add(type);
                }
            }

            return mappingProject;
        }
    }
}
