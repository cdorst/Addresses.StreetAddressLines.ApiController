// Copyright © Christopher Dorst. All rights reserved.
// Licensed under the GNU General Public License, Version 3.0. See the LICENSE document in the repository root for license information.

using Addresses.StreetAddressLines.DatabaseContext;
using DevOps.Code.DataAccess.Interfaces.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Addresses.StreetAddressLines.ApiController
{
    /// <summary>ASP.NET Core web API controller for StreetAddressLine entities</summary>
    [ApiController]
    [Route("api/[controller]")]
    public class StreetAddressLinesController : ControllerBase
    {
        /// <summary>Represents the application events logger</summary>
        private readonly ILogger<StreetAddressLinesController> _logger;

        /// <summary>Represents repository of StreetAddressLine entity data</summary>
        private readonly IRepository<StreetAddressLineDbContext, StreetAddressLine, int> _repository;

        /// <summary>Constructs an API controller for StreetAddressLine entities using the given repository service</summary>
        public StreetAddressLinesController(ILogger<StreetAddressLinesController> logger, IRepository<StreetAddressLineDbContext, StreetAddressLine, int> repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>Handles HTTP GET requests to access StreetAddressLine resources at the given ID</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<StreetAddressLine>> Get(int id)
        {
            if (id < 1) return NotFound();
            var resource = await _repository.FindAsync(id);
            if (resource == null) return NotFound();
            return resource;
        }

        /// <summary>Handles HTTP HEAD requests to access StreetAddressLine resources at the given ID</summary>
        [HttpHead("{id}")]
        public ActionResult<StreetAddressLine> Head(int id) => null;

        /// <summary>Handles HTTP POST requests to save StreetAddressLine resources</summary>
        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<ActionResult<StreetAddressLine>> Post(StreetAddressLine resource)
        {
            var saved = await _repository.AddAsync(resource);
            return CreatedAtAction(nameof(Get), new { id = saved.GetKey() }, saved);
        }
    }
}
