using AAA.Core;
using AAA.DataAccess;
using AAA.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace AAA.Facade.Controllers
{
    public class ReportController : ApiController
    {
        public List<GradeReport> Get()
        {
            SchoolProgressReport schoolReport = ProgressManager.GetSchoolProgressReport();

            return schoolReport.GetGradeReport();
        }
    }
}