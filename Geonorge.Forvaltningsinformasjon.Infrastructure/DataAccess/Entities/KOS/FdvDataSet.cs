using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.KOS
{
    internal class FdvDataSet : IDataSet
    {
        #region IDataSet
        public int Id { get; set; }
        [NotMapped]
        public int? Active { get; set; } = 1;
        public string Name
        {
            get
            {
                return DataSet.Name;
            }
        }
        public string UpdateTypeName
        {
            get
            {
                return UpdateType.Name;
            }
        }

        public string UpdateTypeDescription
        {
            get
            {
                return UpdateType.Description;
            }
        }

        [NotMapped]
        public DateTime? UpdateDate { get; set; }
        [NotMapped]
        public bool IsEmpty { get; set; }
        #endregion

        public DataSetUpdateType UpdateType { get; set; }
        public int? UpdateTypeId { get; set; }

        public int? DataSetId { get; set; }
        public DataSet DataSet { get; set; }

        public int? ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
