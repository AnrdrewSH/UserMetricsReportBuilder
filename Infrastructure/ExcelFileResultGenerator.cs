﻿using Application;
using Application.Engine;
using Application.Generator;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure
{
    public class ExcelFileResultGenerator : IExcelFileResultGenerator
    {
        private readonly IFilterEngine _filterEngine;
        private readonly IExcelGenerator _excelGenerator;

        public ExcelFileResultGenerator(
            IFilterEngine filterEngine,
            IExcelGenerator excelGenerator)
        {
            _filterEngine = filterEngine;
            _excelGenerator = excelGenerator;
        }

        public FileResult CreateFile(int year, SegmentType segmentType, string contentType)
        {
            Dictionary<string, int> aggreagatedMetrics = _filterEngine.GetMetricsByFilter(year, segmentType);
            List<ExcelEntity> excelEntities = aggreagatedMetrics
                .Select(item => new ExcelEntity(item.Key, item.Value))
                .OrderByDescending(item => item.Counter)
                .ToList();
            byte[] reportExcel = _excelGenerator.Generate(excelEntities);

            var fileContentResult = new FileContentResult(reportExcel, contentType)
            {
                FileDownloadName = $"{DateTime.Now:d} Report" + ".xls"
            };

            return fileContentResult;
        }
    }
}
