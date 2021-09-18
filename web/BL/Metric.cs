using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Itoil.BL
{
    public abstract class Metric
    {
        /// <summary>
        /// ИД метрики
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дата получения последнего (текущего) значения
        /// </summary>
        public DateTime ValueDate { get; set; }

        /// <summary>
        /// Тип диаграммы/графика
        /// </summary>
        public MetricTypes Type { get; protected set; }

        public virtual IQueryable<MetricHistory> Histories { get; set; }

        public virtual DTO.Metric ToDto()
        {
            return new DTO.Metric
            {
                Id = Id,
                Name = Name,
                Type = Type.ToString(),
                ValueDate = ValueDate
            };
        }
    }
}