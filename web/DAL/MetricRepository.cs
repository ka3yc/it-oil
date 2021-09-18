using Itoil.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Itoil.DAL
{
    public class MetricRepository
    {
        static int DayCount = 1;

        static List<Metric> FakeMetricData;
        static Random Rnd = new Random();

        public IEnumerable<Metric> GetListMetrics()
        {
            DayCount++;


            FakeMetricData.ForEach(m =>
            {
                RandomizeMetricValues(m);
            });

            return FakeMetricData;
        }

        public IEnumerable<Well> GetWells()
        {
            var wells = new List<Well>
            {
                new Well
                {
                    Id = 1,
                    OwnerName="ООО Газпромнефть добыча",
                    Field = "Северо-Пуровское",
                    Correlation = "1",
                    WellPad = "101",
                    Name = "Ствол 1",
                    Latitude = 66.9056,
                    Longitude = 65.3958
                },
                new Well
                {
                    Id = 2,
                    OwnerName="ООО Газпромнефть добыча",
                    Field = "Северо-Пуровское",
                    Correlation = "1",
                    WellPad = "101",
                    Name = "Ствол 2",
                    Latitude = 67.3056,
                    Longitude = 65.8958
                },
                new Well
                {
                    Id = 3,
                    OwnerName="ООО Газпромнефть добыча",
                    Field = "Северо-Пуровское",
                    Correlation = "1",
                    WellPad = "102",
                    Name = "Ствол 1",
                    Latitude = 65.5056,
                    Longitude = 65.1958
                },
                new Well
                {
                    Id = 4,
                    OwnerName="ООО Газпромнефть добыча",
                    Field = "Северо-Пуровское",
                    Correlation = "1",
                    WellPad = "102",
                    Name = "Ствол 2",
                    Latitude = 65.9056,
                    Longitude = 64.7958
                },
                new Well
                {
                    Id = 5,
                    OwnerName="ООО Газпромнефть добыча",
                    Field = "Северо-Пуровское",
                    Correlation = "1",
                    WellPad = "103",
                    Name = "Ствол 1",
                    Latitude = 67.5056,
                    Longitude = 65.7958
                },
                new Well
                {
                    Id = 6,
                    OwnerName="ООО Газпромнефть добыча",
                    Field = "Северо-Пуровское",
                    Correlation = "1",
                    WellPad = "103",
                    Name = "Ствол 2",
                    Latitude = 67.3056,
                    Longitude = 64.7958
                },
                new Well
                {
                    Id = 7,
                    OwnerName="ООО Газпромнефть добыча",
                    Field = "Тазовское",
                    Correlation = "1",
                    WellPad = "101",
                    Name = "Ствол 1",
                    Latitude = 63.3056,
                    Longitude = 62.7958
                },
                new Well
                {
                    Id = 8,
                    OwnerName="ООО Газпромнефть добыча",
                    Field = "Тазовское",
                    Correlation = "1",
                    WellPad = "102",
                    Name = "Ствол 1",
                    Latitude = 63.8056,
                    Longitude = 61.7958
                },
            };
            
            wells.ForEach(w => RandomizeWellData(w));

            return wells;
        }

        static MetricRepository()
        {
            FakeMetricData = new List<Metric>
            {
                new MetricGauge
                {
                    Id = 1,
                    Name = "Состояние ИБП",
                    Min = 0,
                    Max = 100,
                    FailThreshold = 30
                },
                new MetricGauge
                {
                    Id = 2,
                    Name = "Локальная сеть (Гбит/с)",
                    Min = 0,
                    Max = 10,
                    FailThreshold = 3
                },
                new MetricGauge
                {
                    Id = 3,
                    Name = "Скорость интернет (Мбит/с)",
                    Min = 0,
                    Max = 100,
                    FailThreshold = 10
                },
                new MetricPoll
                {
                    Id = 4,
                    Name = "Почтовый сервер",
                    Period = 5 * 60
                },
                new MetricPoll
                {
                    Id = 5,
                    Name = "Web сервер",
                    Period = 60
                },
                new MetricLicense
                {
                    Id = 6,
                    Name = "Лицензия Petrel",
                    Max = 720,
                    Value = 400
                },
                new MetricLicense
                {
                    Id = 7,
                    Name = "Лицензия Геонафт",
                    Max = 720,
                    Value = 700
                }
            };
        }

        void RandomizeMetricValues(Metric metric)
        {
            metric.ValueDate = DateTime.Now.AddSeconds(-Rnd.Next(1, 120));

            if (metric is MetricGauge)
            {
                var mGauge = metric as MetricGauge;
                mGauge.Value = Rnd.Next(mGauge.Min, mGauge.Max);
            }
            else if (metric is MetricPoll)
            {
                var mPoll = metric as MetricPoll;
                mPoll.ValueDate = DateTime.Now.AddSeconds(-Rnd.Next(1, 2 * mPoll.Period));
            }
            else if (metric is MetricLicense)
            {
                var lMetric = metric as MetricLicense;
                lMetric.Value--;
                if (lMetric.Value < 0)
                    lMetric.Value = 0;
            }
        }

        void RandomizeWellData(Well well)
        {
            var rndVal = Rnd.Next(0, 100);
            well.LastStatusUpdate = DateTime.Now.AddSeconds(-Rnd.Next(0, 120));
            well.SignalLevel = Math.Min(5, Rnd.Next(0, 12));
            well.IsInternetOk = rndVal >= 3;
        }
    }
}