using NTSTraining.Services.Combobox;
using Microsoft.AspNetCore.Mvc;
using NTS.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolManage.Api.Controllers;

namespace NTSTraining.Api.Controllers
{
    [Route("api/combobox")]
    [ApiController]
    public class ComboboxController : BaseApiController
    {
        private readonly IComboboxService comboboxService;

        public ComboboxController(IComboboxService comboboxService)
        {
            this.comboboxService = comboboxService;
        }
    }
}
