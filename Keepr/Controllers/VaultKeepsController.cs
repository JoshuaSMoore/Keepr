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
        public class VaultKeepsController : ControllerBase
        {
            private readonly VaultKeepsService _vaultKeepsService;

            public VaultKeepsController(VaultKeepsService vaultKeepsService)
            {
                _vaultKeepsService = vaultKeepsService;
            }
            [Authorize]
            [HttpPost]

            public async Task<ActionResult<VaultKeep>> Post([FromBody] VaultKeep newVaultKeep)
            {
                try
                {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                newVaultKeep.CreatorId = userInfo.Id;
                VaultKeep createdVaultKeep = _vaultKeepsService.Post(newVaultKeep);
                return Ok(createdVaultKeep);
                }
                catch (System.Exception e)
                {
                return BadRequest(e.Message);
                }
            }
            [HttpGet("{vaultKeepId}")]
            public ActionResult<VaultKeep> GetById(int vaultKeepId)
            {
                try
                {
                    return Ok(_vaultKeepsService.GetById(vaultKeepId));
                }
                catch (System.Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            [Authorize]
            [HttpDelete("{vaultKeepId}")]
            public async Task<ActionResult<VaultKeep>> RemoveVaultKeep(int vaultKeepId)
            {
                try
                {
                    Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                    _vaultKeepsService.RemoveVaultKeep(vaultKeepId, userInfo.Id);
                    return Ok("VaultKeep was Deleted");
                }
                catch (System.Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
        }
    }