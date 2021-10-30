using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Keepr.Models;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Keepr.Services;

namespace Keepr.Controllers

{
  [ApiController]
    [Route("api/[controller]")]
        public class KeepsController : ControllerBase
        {
        private readonly KeepsService _keepsService;
        public KeepsController(KeepsService keepsService)
        {
        _keepsService = keepsService;
        }
        [HttpGet]
        public ActionResult<List<Keep>> GetAll()
        {
        try
        {
        return Ok(_keepsService.GetAll());
        }
        catch (System.Exception e)
        {
        return BadRequest(e.Message);
        }
        }
        [HttpGet("{keepId}")]
        public ActionResult<Keep> GetById(int keepId)
        {
        try
        {
        return Ok(_keepsService.GetById(keepId));
        }
        catch (System.Exception e)
        {
        return BadRequest(e.Message);
        }
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Keep>> Post([FromBody] Keep keepData)
        {
        try
        {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        keepData.CreatorId = userInfo.Id;
        Keep createdKeep = _keepsService.Post(keepData);
        createdKeep.Creator = userInfo;
        return createdKeep;
        }
        catch (System.Exception e)
        {
        return BadRequest(e.Message);
        }
        }
        [Authorize]
        [HttpDelete("{keepId}")]
        public async Task<ActionResult<string>> RemoveKeep(int keepId)
        {
        try
        {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _keepsService.RemoveKeep(keepId, userInfo.Id);
        return Ok("keep was delorted");
        }
        catch (System.Exception e)
        {
        return BadRequest(e.Message);
        }
        }
        [Authorize]
        [HttpPut("{keepId}")]
        public async Task<ActionResult<Keep>> Edit([FromBody] Keep keepData, int keepId)
        {
            try
        {
            Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
            keepData.CreatorId = userInfo.Id;
            keepData.Id = keepId;
            return Ok(_keepsService.Edit(keepData));
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
        }
        }
}