using Itoil.DAL;
using Itoil.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Itoil.BL
{
    public class MetricService : IDisposable
    {
        const string ErrorGetData = "Ошибка получения данных";
        const string ErrorReportingValue = "Ошибка сохранения значения метрики";

        NLog.Logger Logger;
        MetricRepository Repository;

        public MetricService()
        {
            Logger = NLog.LogManager.GetCurrentClassLogger();
             
            Repository = new MetricRepository();
        }

        /// <summary>
        /// Получить список метрик с текущими значениями
        /// </summary>
        /// <returns></returns>
        internal BaseResult<List<DTO.Metric>> GetMetrics()
        {
            try
            {
                var metrics = Repository.GetListMetrics().Select(m =>
                {
                    return m.ToDto();
                }).ToList();

                return BaseResult<List<DTO.Metric>>.Success(metrics);
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                return BaseResult<List<DTO.Metric>>.Error(ErrorGetData);
            }
        }

        /// <summary>
        /// Получить список сгруппированных скважин, со статусами по каждой
        /// </summary>
        /// <returns></returns>
        internal BaseResult<List<WellGroup>> GetWellData()
        {
            var rnd = new Random();

            try
            {
                var wells = Repository.GetWells();

                var result = 
                    wells
                    .GroupBy(w => w.OwnerName)
                    .Select(grpOwner =>new WellGroup
                    {
                        Name = grpOwner.Key,
                        Children = grpOwner
                            .GroupBy(w => w.Field)
                            .Select(grpField => new WellGroup
                            {
                                Name = grpField.Key,
                                Children = grpField
                                    .GroupBy(w => w.Correlation)
                                    .Select(grpCorr => new WellGroup
                                    {
                                        Name = grpCorr.Key,
                                        Children = grpCorr
                                        .GroupBy(w => w.WellPad)
                                        .Select(grpPad => new WellGroup
                                            {
                                                Name = grpPad.Key,
                                                Wells = grpPad
                                                    .Select(w => w.ToDto())
                                                    .ToList()
                                            }).ToList()
                                    }).ToList()
                            }).ToList()
                    }).ToList();

                return BaseResult<List<DTO.WellGroup>>.Success(result);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return BaseResult<List<DTO.WellGroup>>.Error(ErrorGetData);
            }
        }

        /// <summary>
        /// Обновить значение метрики (не реализовано)
        /// </summary>
        /// <param name="metricId">Ид метрики</param>
        /// <param name="value">новое значение</param>
        /// <returns></returns>
        internal BaseResult ReportMetricValue(int metricId, string value)
        {
            try
            {
                //var metric = Repository.GetMetric(metricId);
                //metric.SetValue(value);
                //Repository.SaveMetric(metric);

                return BaseResult.Success();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return BaseResult.Error(ErrorReportingValue);
            }
        }

        /// <summary>
        /// Освобождение ресурсов
        /// </summary>
        public void Dispose()
        {
            
        }
    }
}