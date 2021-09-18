using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Itoil.DTO
{
    public class Metric
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
        /// Тип диаграммы/графика
        /// </summary>
        public string Type { get; set; }

        public int Value { get; set; }

        public int Min { get; set; }

        public int Max { get; set; }

        public string PollPeriod { get; set; }

        public bool IsOk { get; set; }

        /// <summary>
        /// Дата получения последнего (текущего) значения
        /// </summary>
        public DateTime ValueDate { get; set; }
    }
}