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

        public IMappingProject Get(int id)
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
            mappingProjectKos.MappingProjectMunicipalityLinks.GroupBy(mpm => mpm.Municipality).ToList().ForEach(mpm => mappingProject.Municipalities.Add(mpm.First().Municipality));

            // filter deliveries by type
            int[] types = { 1, 2, 8 };

            foreach(DataAccess.Entities.KOS.MappingProjectDelivery deliveryKos in mappingProjectKos.Deliveries)
            {
                if (types.Contains(deliveryKos.TypeId))
                {
                    DataAccess.Entities.Custom.MappingProjectDelivery delivery = new DataAccess.Entities.Custom.MappingProjectDelivery
                    {
                        Name = deliveryKos.Name,
                        TypeName = deliveryKos.Type.Name,
                        Deadline = deliveryKos.Deadline,
                        ChangedDeadline = deliveryKos.ChangedDeadline,
                        FinalDeadline = deliveryKos.FinalDeadline,
                        ReleaseDate = deliveryKos.ReleaseDate
                    };
                    mappingProject.Deliveries.Add(delivery);
                }
            }

            return mappingProject;
        }
    }
}
